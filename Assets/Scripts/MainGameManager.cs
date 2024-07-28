using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;

    //public GameObject loadingScreen;

    public static MainGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MainGameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<MainGameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";


                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
