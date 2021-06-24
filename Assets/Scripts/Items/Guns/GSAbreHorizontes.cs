using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSAbreHorizontes : MonoBehaviour
{
    public ModoDisparo modo = ModoDisparo.Unico;
    public float cadencia = 1f;
    public GameObject bala;
    public Transform spawner;
    public int magazineSize;
    private bool rafaga = false;
    public ParticleSystem muzzleFlash;
    public int bulletsLeft;
    public int piranyasDualesDM; 
    public int piranyasDualesDMK;
    public AudioSource sonidoDisparo;

    

    PlayerStats PlayerStats;
    GameObject[] player;
    private GameObject myplayer;
    
    //UI Armas
    private GunSheet _gunSheet;
    
    //Reference
    private Camera fpsCam;
    public Transform attackPoint;

    public InputHandler inputHandler;

    public PlayerInventory playerInventory;
    public void Awake()
    {
        fpsCam = FindObjectOfType<Camera>();
        inputHandler = FindObjectOfType<InputHandler>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        
        if (playerInventory.primeraCargaAB)
        {
            bulletsLeft = magazineSize;
            playerInventory.ABBleft = magazineSize;
            playerInventory.primeraCargaAB = false;
        }
        else
        {
            bulletsLeft = playerInventory.ABBleft;
        }
        
        _gunSheet = FindObjectOfType<GunSheet>();

    }

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        PlayerStats = myplayer.GetComponent<PlayerStats>();
        piranyasDualesDM = bala.GetComponent<BSExplosive>().bulletDamage;
        piranyasDualesDMK = bala.GetComponent<BSExplosive>().bulletDamageFireKepot;
        
        if (_gunSheet == null)
        {
            _gunSheet = FindObjectOfType<GunSheet>();
        }
        _gunSheet.updateBulletsInfo(bulletsLeft+" / "+magazineSize);
    }

    private void OnEnable()
    {
        InputHandler.onStartFire += StartShooting;
        InputHandler.onStopFire += StopShooting;
        
    }

    private void OnDisable()
    {
        InputHandler.onStartFire -= StartShooting;
        InputHandler.onStopFire -= StopShooting;
    }
    
    void StartShooting()
    {
        if ( bulletsLeft > 0)
        {
            if (modo == ModoDisparo.Rafaga)
            {
                rafaga = true;
            }
            
            StartCoroutine(Disparar());
        }
            
    }
    
    void StopShooting()
    {
        rafaga = false;
    }
    
    IEnumerator Disparar()
    {
        Vector3 direccionBala = new Vector3();
        
        if (fpsCam != null && inputHandler != null && inputHandler.lockOnFlag)
        {
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
            RaycastHit hit;
            //check if ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75); //Just a point far away from the player
        
            //Calculate direction from attackPoint to targetPoint
            direccionBala = targetPoint - attackPoint.position;
        }
        
        while (rafaga && bulletsLeft>0)
        {
            sonidoDisparo.Play();
            //Debug.Log("Pum!!");
            muzzleFlash.Emit(1);
            if (fpsCam != null && inputHandler != null && inputHandler.lockOnFlag)
            {
                GameObject currentBullet = Instantiate(bala, spawner.position, spawner.rotation);
                currentBullet.transform.forward = direccionBala.normalized; 
            }
            else
            {
                Instantiate(bala, spawner.position, spawner.rotation);
            }

            bulletsLeft--;
            playerInventory.ABBleft = bulletsLeft;
            //Debug.Log(bulletsLeft+" / "+magazineSize);
            if (_gunSheet == null)
            {
                _gunSheet = FindObjectOfType<GunSheet>();
            }
            _gunSheet.updateBulletsInfo(bulletsLeft+" / "+magazineSize);
            yield return new WaitForSeconds(cadencia);
            
        }
    }

    public void ReloadAMMO( int ammo)
    {
        bulletsLeft = bulletsLeft + ammo;
    }

    private void Update()
    {
        if (PlayerStats.FLAGFuego)
        {
            bala.GetComponent<BSExplosive>().bulletDamage = piranyasDualesDMK;
        }
        else
        {
            bala.GetComponent<BSExplosive>().bulletDamage = piranyasDualesDM;
        }
    }
}
