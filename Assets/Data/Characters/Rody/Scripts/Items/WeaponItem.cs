using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(menuName ="Items/Weapon Item")] //permite crear mas cs como este con click derecho/crete/item/weapaon item
    public class WeaponItem : Item //item porque adquiere sus propiedades
    {
        //WeaponItem -> Script cuya funcion es tener las propiedades de las armas


        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Heavy_Attack_1;
    }

