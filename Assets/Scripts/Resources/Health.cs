using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using GameDevTV.Utils;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Range(0, 100)]
        [SerializeField] float regenerationPercentage = 70;
        LazyValue<float> healthPoints;

        bool isDead = false;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().OnLevelUp += Health_OnLevelUp;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().OnLevelUp -= Health_OnLevelUp;
        }
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print($"{gameObject.name} took damage: damage");
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void Health_OnLevelUp()
        {

            print("Health: Level up");
            print($"healthPoints = {healthPoints.value}");
            print($"Max = {GetComponent<BaseStats>().GetStat(Stat.Health)} * {regenerationPercentage} / 100");
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);

            print($"regenHealthPoints = {regenHealthPoints}");
            print($"healthPoints = {healthPoints.value}");
        }
        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value <= 0)
            {
                Die();
            }
        }
    }
}