using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Utils;
namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpEffect = null;


        public event Action OnLevelUp;
        LazyValue<int> currentLevel;
        Experience experience;

        private void Awake() 
        {    
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }

        private void Start()
        {
            currentLevel.ForceInit();
        }
 
        private void OnEnable() {
          if (experience != null)
            {
                experience.OnExperienceGained += Experience_OnExperienceGained;
            }   
        }

        private void OnDisable() 
        {
           if (experience != null)
            {
                experience.OnExperienceGained -= Experience_OnExperienceGained;
            }   
        }

        private void Experience_OnExperienceGained()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                print("Basestate: Level up");
                currentLevel.value = newLevel;
                OnLevelUp();
                LevelUpEffect();
            }
        }
        
        private void LevelUpEffect()
        {
            if (levelUpEffect == null) return;
            Instantiate(levelUpEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel()) + GetAdditiveModifier(stat);
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }
        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
    }
}