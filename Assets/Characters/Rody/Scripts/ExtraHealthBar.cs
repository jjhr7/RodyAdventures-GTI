using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ExtraHealthBar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider.GetComponent<Slider>();
        }

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void setCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }
    }
}


