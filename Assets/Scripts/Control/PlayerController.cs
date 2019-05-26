using RPG.Movement;
using UnityEngine;

namespace RPG.Contral
{
    public class PlayerController : MonoBehaviour
    {

        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool hasHit = Physics.Raycast(ray, out hitinfo);

            if (hasHit)
            {
                mover.MoveTo(hitinfo.point);
            }
        }
    }
}