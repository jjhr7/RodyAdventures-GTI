using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WeaponHolderSlot : MonoBehaviour
    {
        //WeaponHolderSlot > control si el arma esta en las manos

        public Transform parentOverride; //transform de toda la vida
        public bool isLeftHandSlot; //bool arma izquierda
        public bool isRightHandSlot; // lo de arriba pero derecha

        public GameObject currentWeaponModel; //aca almacenamos el arma actual

        public void UnloadWeapon()
        {
            if(currentWeaponModel != null) //si tenemos arma en manos
            {
                currentWeaponModel.SetActive(false); //descativar gameObject
            }
        }

        public void UnloadWeaponAndDestroy() //desarmar y destruir
        {
            if (currentWeaponModel != null) // si tenemos arma
            {
                Destroy(currentWeaponModel); // destruir gameObject
            }
        }

        public void LoadWeapomodel(WeaponItem weaponItem) //carga el arma en nustras manos
        {
            //unload  weapon and destroy
            UnloadWeaponAndDestroy(); //destruye el arma

            if (weaponItem == null) //si el arma nueva es igual a null
            {
                //Unload weapon
                UnloadWeapon(); //desarmar objeto
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject; //instanciar como gameObject
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;

        }
        
        public void LoadFireWeapomodel(FireWeponItem fireWeponItem) //carga el arma en nustras manos
        {
            //unload  weapon and destroy
            UnloadWeaponAndDestroy(); //destruye el arma

            if (fireWeponItem == null) //si el arma nueva es igual a null
            {
                //Unload weapon
                UnloadWeapon(); //desarmar objeto
                return;
            }

            GameObject model = Instantiate(fireWeponItem.modelPrefab) as GameObject; //instanciar como gameObject
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;

        }


    }


