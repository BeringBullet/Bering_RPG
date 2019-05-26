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

        Mover mover;
        ActionScheduler actionScheduler;

        private bool IsInRange => Vector3.Distance(transform.position, target.position) < weaponRange;
        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            if (!IsInRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
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
    }
}