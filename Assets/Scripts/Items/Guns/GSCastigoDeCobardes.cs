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


    public int piranyasDualesDM;
    public int piranyasDualesDMK;


    PlayerStats PlayerStats;
    GameObject[] player;
    private GameObject myplayer;
    //UI Armas
    private GunSheet _gunSheet;

    public void Awake()
    {
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
        while (rafaga && bulletsLeft>0)
        {
            //Debug.Log("Pum!!");
            muzzleFlash.Emit(1);
            Instantiate(bala, spawner1.position, spawner1.rotation);
            Instantiate(bala, spawner2.position, spawner2.rotation);
            Instantiate(bala, spawner3.position, spawner3.rotation);
            Instantiate(bala, spawner4.position, spawner4.rotation);
            bulletsLeft --;
            Debug.Log(bulletsLeft+" / "+magazineSize);
            
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
