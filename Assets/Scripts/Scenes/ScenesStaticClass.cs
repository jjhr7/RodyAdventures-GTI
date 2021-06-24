using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScenesStaticClass
{
    //pasar info entre scenas
    public static string sceneName { get; set; }



    public static void deadInNivel1()
    {
        sceneName = "nivel1";
    }

    public static void deadInArenaNyapos()
    {
        sceneName = "arena_nyapos";
    }

    public static string getSceneName()
    {
        return sceneName;
    }
}
