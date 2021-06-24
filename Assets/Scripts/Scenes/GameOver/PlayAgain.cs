using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
   public double tiempoPausa;

   public void Awake()
   {
      tiempoPausa = 0;
   }

   public void Update()
   {
      tiempoPausa += Time.deltaTime;
      
      if(tiempoPausa>10 )
         FunctionPlayAgain();
   }

   public void FunctionPlayAgain()
   {
      SceneManager.LoadScene(ScenesStaticClass.getSceneName().ToString());
   }
}
