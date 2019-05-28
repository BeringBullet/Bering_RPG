using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Contral
{
    public class PlayerController : MonoBehaviour
    {
        private Ray MouseRay => Camera.main.ScreenPointToRay(Input.mousePosition);
        Mover mover;
        Health health;
        Fighter fighter;
        private void Start()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead) return;
            if (InteractWithCombat) return;
            if (InteractWithMovement) return;
        }

        private bool InteractWithCombat
        {
            get
            {
                RaycastHit[] hits = Physics.RaycastAll(MouseRay);
                foreach (RaycastHit hit in hits)
                {
                    CombatTarget combatTarget = hit.transform.GetComponent<CombatTarget>();
                    if (combatTarget == null) continue;
                    if (!fighter.CanAttack(combatTarget.gameObject)) continue;

                    if (Input.GetMouseButton(0))
                    {
                        fighter.Attack(combatTarget.gameObject);
                    }
                    return true;
                }
                return false;
            }
        }

        private bool InteractWithMovement
        {
            get
            {
                RaycastHit hitinfo;
                if (Physics.Raycast(MouseRay, out hitinfo))
                {
                    if (Input.GetMouseButton(0))
                    {
                        mover.StartMoveAction(hitinfo.point, 1f);
                    }
                    return true;
                }
                return false;
            }
        }
    }
}