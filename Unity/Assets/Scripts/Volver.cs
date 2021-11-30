using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class Volver : MonoBehaviour
{
    string puerto;
    SerialPort port;

    private void Awake()
    {
        puerto = PuertoString.puertoArduino;
    }
    void Start()
    {
        port = new SerialPort(puerto, 9600, Parity.None, 8, StopBits.One);
        port.Open();
    }

    void Update()
    {
        
    }

    public void volverMenu()
    {
        SceneManager.LoadScene(0);
        port.Close();
    }
}
