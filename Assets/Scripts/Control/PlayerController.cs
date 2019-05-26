using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Contral
{
    public class PlayerController : MonoBehaviour
    {
        private Ray MouseRay => Camera.main.ScreenPointToRay(Input.mousePosition);
        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(MouseRay);
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hitinfo;
            bool hasHit = Physics.Raycast(MouseRay, out hitinfo);

            if (hasHit)
            {
                mover.MoveTo(hitinfo.point);
            }
        }

    }
}