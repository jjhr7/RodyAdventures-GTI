using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerStats : MonoBehaviour
    {
        //PlayerStats -> centro de control de las estadisticas del personaje
        //Aca se controla todo lo que tiene que ver con la vida, por ejemplo
        //cuando golpean al personaje y hay que bajarle la vida, hacer una aniamcion de danyo
        // cuando el persoanje muere hacer animacion ...

        public int healthLevel = 10;
        public int maxHealth;
        bool extraHealthActive;
        bool contadorMonedasActive;
        bool FLAGcontadorMonedasActive;

        public int extraHealth;
        public int extraMaxHealth;
        public int currentHealth;

        public int contadorMonedas;

        public HealthBar healthBar;
        public ExtraHealthBar extraHealthBar;

        MoneyBar moneyBar;
        public GameObject BarraMonedas;

        private double timer;
        private double timerMonedas;
        public double tiempoContadorMonedasActivo;


        public double extraHealthSpeed;

        AnimatorHandler animatorHandler; //para pasarle las animaciones al centro de control

        private void Awake()
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
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        // Start is called before the first frame update
        void Start()
        {
            maxHealth = SetMaxHealthFromHelathLevel(); // set la vida maxima que tendra el personaje dependido del healthLevel
            currentHealth = maxHealth; //indico al iniciar que la vida esta al maximo
            extraHealth = 0;
            healthBar.SetMaxHealth(maxHealth); //le indico a la barra de saluda su valor
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

    private int SetMaxHealthFromHelathLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage) //funcion que te reduce la vida respecto al danyo que recibes
        {
            if (extraHealth == 0)
            {
                currentHealth = currentHealth - damage;  // vida actual - el daño que te hacen

                healthBar.SetCurrentHealth(currentHealth); // actualizar la salud
                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                }
            }
            else
            {
                extraHealth -= damage;
                if (extraHealth < 0)
                {
                    currentHealth += extraHealth;
                    extraHealth = 0;
                }
                healthBar.SetCurrentHealth(currentHealth);
                extraHealthBar.setCurrentHealth(extraHealth);
            }
            animatorHandler.PlayTargetAnimation("Damage_01", true); //activar animacion de danyo
            if (currentHealth <= 0)
            {
                animatorHandler.PlayTargetAnimation("Dead_01", true);
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
                healthBar.SetCurrentHealth(currentHealth);
                extraHealthBar.setCurrentHealth(extraHealth);
                if (extraHealth > extraMaxHealth)
                {
                    extraHealth = extraMaxHealth;
                }
            }
            else
            {
                extraHealthActive = false;
                healthBar.SetCurrentHealth(currentHealth);
            }

        }
    }

