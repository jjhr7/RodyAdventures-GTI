using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerStats : MonoBehaviour
    {

        public int healthLevel = 10;
        public int maxHealth;
        bool extraHealthActive;
        bool contadorMonedasActive;
        bool FLAGcontadorMonedasActive;

        public int extraHealth;
        public int extraMaxHealth;
        public int currentHealth;

        public int contadorMonedas;

        public HealthBar healthbar;
        public ExtraHealthBar extraHealthBar;

        MoneyBar moneyBar;
        public GameObject BarraMonedas;

        private double timer;
        private double timerMonedas;
        public double tiempoContadorMonedasActivo;


        public double extraHealthSpeed;

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            extraHealth = 0;
            healthbar.SetMaxHealth(maxHealth);
            moneyBar = FindObjectOfType<MoneyBar>();
            BarraMonedas.SetActive(false);
            contadorMonedas = 0;
            extraHealthActive = false;
        }

        private void Update()
        {
            if (extraHealthActive)
            {
                timer += Time.deltaTime;
                if (timer > extraHealthSpeed)
                {
                    extraHealth--;
                    extraHealthBar.setCurrentHealth(extraHealth);
                    timer = 0;
                }
            }

            if (contadorMonedasActive)
            {
                timerMonedas += Time.deltaTime;
                if (timerMonedas > tiempoContadorMonedasActivo)
                {
                    BarraMonedas.SetActive(false);
                    timerMonedas = 0;
                }
            }

            if (FLAGcontadorMonedasActive)
            {
                timerMonedas = 0;
                contadorMonedasActive = true;
                FLAGcontadorMonedasActive = false;
            }
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (extraHealth == 0)
            {
                currentHealth -= damage;
                if (currentHealth < 0)
                {
                    currentHealth = 0;
                }
                healthbar.setCurrentHealth(currentHealth);
            }
            else
            {
                extraHealth -= damage;
                if (extraHealth < 0)
                {
                    currentHealth += extraHealth;
                    extraHealth = 0;
                }
                healthbar.setCurrentHealth(currentHealth);
                extraHealthBar.setCurrentHealth(extraHealth);
            }
        }

        public void TakeMoney(int money)
        {
            contadorMonedas += money;
            if (moneyBar != null)
            {
                BarraMonedas.SetActive(true);
                moneyBar.setMoneyBar(contadorMonedas);
                FLAGcontadorMonedasActive = true;
            }
            else
            {
                Debug.Log("No hay contador de monedas");
            }
        }


        public void TakeHealth(int vida)
        {
            currentHealth += vida;
            if (currentHealth > maxHealth)
            {
                extraHealthActive = true;
                extraHealth += currentHealth - maxHealth;
                currentHealth = maxHealth;
                healthbar.setCurrentHealth(currentHealth);
                extraHealthBar.setCurrentHealth(extraHealth);
                if (extraHealth > extraMaxHealth)
                {
                    extraHealth = extraMaxHealth;
                }
            }
            else
            {
                extraHealthActive = false;
                healthbar.setCurrentHealth(currentHealth);
            }

        }

    }
}
