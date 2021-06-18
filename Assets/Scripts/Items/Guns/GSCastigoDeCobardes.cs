using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class GSCastigoDeCobardes : MonoBehaviour
{
    public ModoDisparo modo = ModoDisparo.Unico;
    public float cadencia = 1f;
    public GameObject bala;
    public Transform spawner1, spawner2, spawner3,spawner4;
    public int magazineSize;
    private bool rafaga = false;
    public ParticleSystem muzzleFlash;
    public int bulletsLeft;
    public AudioSource sonidoDisparo;


    public int piranyasDualesDM;
    public int piranyasDualesDMK;


    PlayerStats PlayerStats;
    GameObject[] player;
    private GameObject myplayer;
    //UI Armas
    private GunSheet _gunSheet;

    //Reference
    private Camera fpsCam;
    public Transform attackPoint1;
    public Transform attackPoint2;
    public Transform attackPoint3;
    public Transform attackPoint4;

    public InputHandler inputHandler;
    
    public void Awake()
    {
        fpsCam = FindObjectOfType<Camera>();
        inputHandler = FindObjectOfType<InputHandler>();
        
        bulletsLeft = magazineSize;
        _gunSheet = FindObjectOfType<GunSheet>();

    }

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        PlayerStats = myplayer.GetComponent<PlayerStats>();
        piranyasDualesDM = bala.GetComponent<BSPiranyasDuales>().bulletDamage;
        piranyasDualesDMK = bala.GetComponent<BSPiranyasDuales>().bulletDamageFireKepot;
        
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
        Vector3 direccionBala1 = new Vector3();
        Vector3 direccionBala2 = new Vector3();
        Vector3 direccionBala3 = new Vector3();
        Vector3 direccionBala4 = new Vector3();
        
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
            direccionBala1 = targetPoint - attackPoint1.position;
            direccionBala2 = targetPoint - attackPoint2.position;
            direccionBala3 = targetPoint - attackPoint3.position;
            direccionBala4 = targetPoint - attackPoint4.position;
        }
        
        while (rafaga && bulletsLeft>0)
        {
            sonidoDisparo.Play();
            muzzleFlash.Emit(1);
            
            if (fpsCam != null && inputHandler != null && inputHandler.lockOnFlag)
            {
                GameObject currentBullet1 = Instantiate(bala, spawner1.position, spawner1.rotation);
                GameObject currentBullet2 = Instantiate(bala, spawner2.position, spawner2.rotation);
                GameObject currentBullet3 = Instantiate(bala, spawner3.position, spawner3.rotation);
                GameObject currentBullet4 = Instantiate(bala, spawner4.position, spawner4.rotation);
                
                currentBullet1.transform.forward = direccionBala1.normalized; 
                currentBullet2.transform.forward = direccionBala2.normalized; 
                currentBullet3.transform.forward = direccionBala3.normalized; 
                currentBullet4.transform.forward = direccionBala4.normalized; 
            }
            else
            {
                Instantiate(bala, spawner1.position, spawner1.rotation);
                Instantiate(bala, spawner2.position, spawner2.rotation);
                Instantiate(bala, spawner3.position, spawner3.rotation);
                Instantiate(bala, spawner4.position, spawner4.rotation);
            }
            
            
            //bulletsLeft --;
            //Debug.Log(bulletsLeft+" / "+magazineSize);
            
            if (_gunSheet == null)
            {
                _gunSheet = FindObjectOfType<GunSheet>();
            }
            _gunSheet.updateBulletsInfo(bulletsLeft+" / "+magazineSize);
            yield return new WaitForSeconds(cadencia);
            
        }
    }


    private void Update()
    {
        if (PlayerStats.FLAGFuego)
        {
            bala.GetComponent<BSPiranyasDuales>().bulletDamage = piranyasDualesDMK;
        }
        else
        {
            bala.GetComponent<BSPiranyasDuales>().bulletDamage = piranyasDualesDM;
        }
    }


    public void ReloadAMMO( int ammo)
    {
        bulletsLeft = bulletsLeft + ammo;
    }
}
