using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    namespace Assets.Scripts.Resources
    {
        class HealthDisplay : MonoBehaviour
        {
            Health health;

            private void Awake()
            {
                health = GameObject.FindWithTag("Player").GetComponent<Health>();
            }

            private void Update()
            {
                GetComponent<Text>().text = $"{health.Percentage:0.0}%";
            }
        }
    }
}
