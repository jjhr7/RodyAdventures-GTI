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
        piranyasDualesDM = bala.GetComponent<BSExplosive>().bulletDamage;
        piranyasDualesDMK = bala.GetComponent<BSExplosive>().bulletDamageFireKepot;
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
            sonidoDisparo.Play();
            //Debug.Log("Pum!!");
            muzzleFlash.Emit(1);
            Instantiate(bala, spawner.position, spawner.rotation);
            bulletsLeft --;
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
