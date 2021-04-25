using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class MoneyBar : MonoBehaviour
    {
        public Text moneyBar;

        public void setMoneyBar(int money)
        {
            moneyBar.text = money.ToString();
        }
    }
