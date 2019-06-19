using RPG.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    namespace Assets.Scripts.Resources
    {
        class EnemyDisplay : MonoBehaviour
        {
            Fighter fighter;

            private void Awake()
            {
                fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            }

            private void Update()
            {
                if (fighter.GetTarget() == null)
                {
                    GetComponent<Text>().text = "N/A";
                    return;
                }
                Health health = fighter.GetTarget();
                GetComponent<Text>().text = $"{health.GetPercentage():0}/{health.GetMaxHealthPoints():0}";
            }
        }
    }
}
