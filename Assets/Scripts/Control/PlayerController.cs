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
                    CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                    if (target == null) continue;

                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target);
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
                        mover.StartMoveAction(hitinfo.point);
                    }
                    return true;
                }
                return false;
            }
        }
    }
}