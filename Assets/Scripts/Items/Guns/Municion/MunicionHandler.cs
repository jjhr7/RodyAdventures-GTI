using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionHandler : MonoBehaviour
{
    public int valor = 1;
    private bool targeteado;

    PlayerInventory playerInventory;
    private PlayerStats _playerStats;
    GameObject[] player;
    private GameObject myplayer;
    public int tipoArma = 1;
    public int MoveSpeed;

    public GameObject municion;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerInventory = myplayer.GetComponent<PlayerInventory>();
        _playerStats = myplayer.GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(myplayer.transform.position, transform.position) < 5)
        {
            targeteado = true;
        }

        if (targeteado)
        {
            //Calculamos la distancia con el jugador
            float dist = Vector3.Distance(myplayer.transform.position, transform.position);
            //Apunta al jugador
            this.transform.LookAt(myplayer.transform.position + new Vector3(0, 1, 0));
            //Lo movemos a la velocidad deseada
            this.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Destruímos si toca al jugador
        if (other.tag.Equals("Player"))
        {
            cargarMunicion();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destruímos si toca al jugador
        if (collision.collider.tag.Equals("Player"))
        {
            cargarMunicion();
            Destroy(gameObject);
        }
    }

    public void cargarMunicion()
    {
        switch (tipoArma)
        {
            case 1:
                if (playerInventory.PDBleft+valor<100)
                {
                    playerInventory.PDBleft = playerInventory.PDBleft + valor;
                   _playerStats.rodySoundsManager.prepararSonido(4);
                    playerInventory._gunSheet.updateBulletsInfo(playerInventory.PDBleft+"/"+100);  
                }
                
                break;
            
            case 2:
                
                if (playerInventory.CCBleft + valor < 30)
                {
                    playerInventory.CCBleft = playerInventory.CCBleft + valor;
                    _playerStats.rodySoundsManager.prepararSonido(4);
                    playerInventory._gunSheet.updateBulletsInfo(playerInventory.CCBleft + "/" + 30);
                }

                break;
            
            case 3:
                if (playerInventory.ABBleft + valor < 10)
                {
                    playerInventory.ABBleft = playerInventory.ABBleft + valor;
                    _playerStats.rodySoundsManager.prepararSonido(4);
                    playerInventory._gunSheet.updateBulletsInfo(playerInventory.ABBleft + "/" + 10);
                }

                break;
            default:
                Debug.Log("Caja de munición desconocida");
                break;
        }
    }
}
