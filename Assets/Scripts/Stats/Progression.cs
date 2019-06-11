using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Pregression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int Level)
        {
            foreach (ProgressionCharacterClass item in characterClasses)
            {
                if (item.characterClass == characterClass)
                {
                    return item.health[Level - 1];
                }
            }
            return 40;
        }

        [Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health = null;
        }
    }
}