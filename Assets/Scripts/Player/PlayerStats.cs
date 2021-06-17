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

    //stamina
    public float maxStamina;
    public float currentStamina;
    public int staminaLevel = 10;
    public float staminaRegenerationAmount = 10f;
    public float staminaDrainSprint = 10f;
    public float staminaRegenTimer = 0;

    public int contadorMonedas;

    PlayerManager playerManager;

    public HealthBar healthBar; //barra salud
    public StaminaBar staminaBar; //barra stamina
    public ExtraHealthBar extraHealthBar; //barra extra salud

    MoneyBar moneyBar; //barra monedas
    public GameObject BarraMonedas;

    private double timer;
    private double timerMonedas;
    public double tiempoContadorMonedasActivo;


    public double extraHealthSpeed;

    AnimatorHandler animatorHandler; //para pasarle las animaciones al centro de control


    private double timerGuende;
    private double timerFuego;
    private double timerInf;
    public double infectionSpeed;


    public bool FLAGGuende;
    public bool FLAGFuego;
    public int extraFireDamage;
    int vidaGuende;

    public GameObject fire;
    public RodySoundsManager rodySoundsManager;
    
    GameObject fireClone;
    PlayerLocomotion playerLocomotion;
    float normalSpeed;
    float rollSpeed;

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

        staminaBar = FindObjectOfType<StaminaBar>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        FLAGGuende = false;
        FLAGFuego = false;
        maxHealth = SetMaxHealthFromHelathLevel(); // set la vida maxima que tendra el personaje dependido del healthLevel
        currentHealth = maxHealth; //indico al iniciar que la vida esta al maximo
        extraHealth = 0;
        extraHealthActive = false;
        healthBar.SetMaxHealth(maxHealth); //le indico a la barra de saluda su valor
        //stamina
        maxStamina = SetMaxStaminaFromStaminaLevel(); //recoger stamina amxima
        staminaBar.SetMaxStamina(Mathf.RoundToInt(maxStamina)); //inicializar valor maximo stamina
        currentStamina = maxStamina; //inicializar al principio el stamina maximo
        //monedas
        moneyBar = FindObjectOfType<MoneyBar>();
        BarraMonedas.SetActive(false);
        contadorMonedas = 0;
        playerManager = GetComponent<PlayerManager>();

        playerLocomotion = GetComponent<PlayerLocomotion>();
        normalSpeed = playerLocomotion.movementSpeed;
        rollSpeed = playerLocomotion.sprintSpeed;



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

        if (FLAGcontadorMonedasActive)
        {
            timerMonedas = 0;
            contadorMonedasActive = true;
            FLAGcontadorMonedasActive = false;
        }

        if (FLAGGuende)
        {
            timerGuende += Time.deltaTime;
            if (timerGuende < 20)
            {
                staminaRegenTimer = 0;
                timerInf += Time.deltaTime;
                if (timerInf >= infectionSpeed)
                {
                    if (extraHealth == 0)
                    {
                        currentHealth = currentHealth - vidaGuende;  // vida actual - el danyo que te hacen

                        healthBar.SetCurrentHealth(currentHealth); // actualizar la salud
                        if (currentHealth <= 0)
                        {
                            currentHealth = 0;
                        }
                    }
                    else
                    {
                        extraHealth -= vidaGuende;
                        if (extraHealth < 0)
                        {
                            currentHealth += extraHealth;
                            extraHealth = 0;
                        }
                        healthBar.SetCurrentHealth(currentHealth);
                        extraHealthBar.setCurrentHealth(extraHealth);
                    };

                    if (currentHealth <= 0)
                    {
                        rodySoundsManager.prepararSonido(0);
                        animatorHandler.PlayTargetAnimation("Dead_01", true);
                        
                    }
                    timerInf = 0;
                }
            }
            else
            {
                vidaGuende = 0;
                timerGuende = 0;
                playerLocomotion.movementSpeed = normalSpeed;
                playerLocomotion.sprintSpeed = rollSpeed;
                FLAGGuende = false;
            }
        }

        if (FLAGFuego)
        {
            if (fireClone!=null)
            {
                fireClone.transform.position = transform.position;
            }
            else
            {
                fireClone = GameObject.Find("ParticleFire(Clone)");
            }
            timerFuego += Time.deltaTime;
            if (timerFuego > 60)
            {
                Destroy(fireClone);
                extraFireDamage = 0;
                FLAGFuego = false;
            }
        }
    }

    private int SetMaxHealthFromHelathLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    public void TakeDamage(int damage) //funcion que te reduce la vida respecto al danyo que recibes
    {
        if (extraHealth == 0)
        {
            currentHealth = currentHealth - damage;  // vida actual - el danyo que te hacen

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
        rodySoundsManager.prepararSonido(1);
        animatorHandler.PlayTargetAnimation("Damage_01", true); //activar animacion de danyo
        if (currentHealth <= 0)
        {
            rodySoundsManager.prepararSonido(0);
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

    public void TakeGuende(int vida, int speedRody)
    {
        vidaGuende = vida;
        playerLocomotion.sprintSpeed = speedRody * 2;
        playerLocomotion.movementSpeed = speedRody;
        FLAGGuende = true;
    }
    public void TakeFire(int valor)
    {
        if (FLAGFuego)
        {

            timerFuego = 0;
        }
        else
        {

            timerFuego = 0;
            Instantiate<GameObject>(fire, transform);
            fireClone = GameObject.Find("ParticleFire(Clone)");
            extraFireDamage = valor;
            FLAGFuego = true;
        }
    }
    public void TakeStaminaDamage(int damage)
    {
        currentStamina = currentStamina - damage;
        staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina)); //set stamina a la barra
    }

    public void RegenerateStamina()
    {
        if (playerManager.isSprinting) //si esta en modo bola
        {
            staminaRegenTimer = 0; //se reincia el contador de regeneracion
            if (currentStamina > 0) //si tiene mas de 0 de stamina
            {
                currentStamina -= staminaDrainSprint * Time.deltaTime; //resto stamina cada delataTime
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina)); //inserto la stamina
            }
            else //si tiene menos de 0
            {
                currentStamina = 0; // 0 
            }

        }
        else if (!playerManager.isSprinting) //sino esta en sprint
        {
            staminaRegenTimer += Time.deltaTime; //empezar contador de regeneracion
            if (currentStamina < maxStamina && staminaRegenTimer > 1f) //si su vida es menor y el contador es gucci
            {
                currentStamina += staminaRegenerationAmount * Time.deltaTime; //subir poco a poco el current
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina)); //insertar nueva vida
            }
        }

    }
}

