using UnityEngine;
using RPG.Resources;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IReycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
         
        public bool HandleRaycast(PlayerController callingcontroller)
        {
            if (!callingcontroller.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                callingcontroller.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}