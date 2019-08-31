using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health HealthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas Canvas = null;
        // Update is called once per frame
        void Update()
        {
            float health = HealthComponent.GetFraction();
            if (Mathf.Approximately(health, 0) || Mathf.Approximately(health, 1))
            {
                Canvas.enabled = false;
                return;
            }
            Canvas.enabled = true;
            foreground.localScale = new Vector3(health, 1,1);
        }
    }
}