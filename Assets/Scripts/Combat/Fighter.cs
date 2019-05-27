using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Transform target;

        [Range(0f, 5f)]
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttachs = 1f;

        float timeSinceLastAttack = 0;

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        private bool IsInRange => Vector3.Distance(transform.position, target.position) < weaponRange;

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

            if (!IsInRange) 
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttachs)
            {
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }


        // Annimation Event
        void Hit()
        {

        }
    }
}