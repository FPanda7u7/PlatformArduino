using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class SerialCom : MonoBehaviour
{
    
    //Arduino
    public static bool isConnected = false;
    string puerto;
    SerialPort port;

    //Game
    public static string estadoB, nuid;
    public static int x, y, z;
    public static bool coin;

    private void Awake()
    {
        puerto = PuertoString.puertoArduino;  
    }
    void Start()
    {
        port = new SerialPort(puerto, 9600, Parity.None, 8, StopBits.One);
        port.Open();
        isConnected = true;       
    }

    
    void Update()
    {
        if (isConnected)
        {
            string dataString = port.ReadLine();
            char splitChar = '/';
            string[] dataRAW = dataString.Split(splitChar);

            if (dataRAW.Length >= 5)
            {
                x = int.Parse(dataRAW[0]);
                y = int.Parse(dataRAW[1]);
                z = int.Parse(dataRAW[2]);
                estadoB = dataRAW[3];
                nuid = dataRAW[4];
            }          

            if (coin)
            {
                port.WriteLine(Collected.led.ToString());
                coin = false;
            }

            if (nuid == "4234f822")
            {
                PlataformMoveV.moverse = true;
            }else if (nuid == "1c1463a3")
            {
                PlataformMoveH.moverseH = true;
            }
        }
    }
}
