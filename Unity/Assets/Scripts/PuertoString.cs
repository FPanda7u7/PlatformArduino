using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertoString : MonoBehaviour
{
    public static PuertoString instance;
    public static string puertoArduino;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {        
    }

    void Update()
    {
    }
}
