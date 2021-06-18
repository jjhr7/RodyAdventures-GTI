using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //clase para todos los objetos del juego (o para los que se metan aqui :-) )

    public class Item : ScriptableObject
    {
        [Header("Item Information")]
        public Sprite itemIcon;
        public string itemName;
    }


