using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Range(0, 100)]
        [SerializeField] float regenerationPercentage = 70;
        float healthPoints = -1f;

        bool isDead = false;

        private void Start()
        {
            GetComponent<BaseStats>().OnLevelUp += Health_OnLevelUp;
            if (healthPoints < 0)
            {
                healthPoints = healthPoints < 0 ? GetComponent<BaseStats>().GetStat(Stat.Health) : healthPoints;
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print($"{gameObject.name} took damage: damage");
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
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
            print($"healthPoints = {healthPoints}");
            print($"Max = {GetComponent<BaseStats>().GetStat(Stat.Health)} * {regenerationPercentage} / 100" );
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);

            print($"regenHealthPoints = {regenHealthPoints}");
            print($"healthPoints = {healthPoints}");
        }
        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}