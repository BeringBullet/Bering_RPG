using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

        [Range(0f, 5f)]
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttachs = 1f;
        [SerializeField]

        Health target;
        float weaponDamage = 20f;
        float timeSinceLastAttack = 0;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        private bool IsInRange => Vector3.Distance(transform.position, target.transform.position) < weaponRange;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead) return;

            if (!IsInRange)
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttachs)
            {
                
                //This will trigtger the Hit() event.
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        // Annimation Event
        void Hit()
        {
            if (target != null)
            {
                target.TakeDamage(weaponDamage);
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }
    }
}