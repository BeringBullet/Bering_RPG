using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealthPoints = 100f;

        Animator animator;

        float healthPoints = 100f;

        public bool IsDead { get; private set; } = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            healthPoints = maxHealthPoints;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (IsDead) return;
            IsDead = true;
            animator.SetTrigger("die");
        }
    }
}