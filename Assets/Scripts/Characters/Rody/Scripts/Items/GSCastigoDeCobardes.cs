using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Awake()
    {
        bulletsLeft = magazineSize;
        
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
            yield return new WaitForSeconds(cadencia);
            
        }
    }

    public void ReloadAMMO( int ammo)
    {
        bulletsLeft = bulletsLeft + ammo;
    }
}
