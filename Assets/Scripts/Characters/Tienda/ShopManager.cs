using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{

    public int coins;
    public Text coinsUI;
    public GameObject contadorm;
    //para sincronizar las monedas con las de rody
    public PlayerStats playerStats;
    GameObject[] player;
    private GameObject myplayer;
    //
    public GameObject shop;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;


    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerStats = myplayer.GetComponent<PlayerStats>();
        coins = playerStats.contadorMonedas;

        contadorm.SetActive(true);
        coinsUI.text = coins.ToString();
        LoadPanels();
        CheckPurchaseable();
        shop.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        coins = playerStats.contadorMonedas;
        LoadPanels();
        CheckPurchaseable();
    }
    public void LoadPanels()
    {
        for(int i=0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemSO[i].description;
            shopPanels[i].imgItem.sprite = shopItemSO[i].item;
            shopPanels[i].costTxt.text = shopItemSO[i].basecost.ToString();
            CheckPurchaseable();
        }
    }
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (coins >= shopItemSO[i].basecost)
            {//si tengo suficiente dinero
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }
    public void PurchaseItem(int btnNo)
    {
        if(coins >= shopItemSO[btnNo].basecost)
        {
           
            coins = coins - shopItemSO[btnNo].basecost;
            coinsUI.text = coins.ToString();
            CheckPurchaseable();
        }
    }
}
