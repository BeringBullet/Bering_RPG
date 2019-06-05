using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float maxHealthPoints = 100f;

        float healthPoints = 100f;

        public bool IsDead { get; private set; } = false;

        private void Start()
        {
            healthPoints = maxHealthPoints;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (IsDead) return;
            IsDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            CheckHealth();
        }
    }
}