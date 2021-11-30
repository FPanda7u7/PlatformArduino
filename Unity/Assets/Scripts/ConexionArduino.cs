using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class ConexionArduino : MonoBehaviour
{
    string[] ports;
    bool isConnected = false;
    SerialPort port;
    public Dropdown lista;
    string portname;
    public Text aviso;

    private void Awake()
    {
        lista.options.Clear();
        ports = SerialPort.GetPortNames();

        foreach (string port in ports)
        {
            lista.options.Add(new Dropdown.OptionData() { text = port });
        }

        DropdownItemSelected(lista);
        lista.onValueChanged.AddListener(delegate { DropdownItemSelected(lista); });
    }

    void DropdownItemSelected(Dropdown lista)
    {
        int indice = lista.value;
        portname = lista.options[indice].text;
    }

    public void conectar()
    {
        if (!isConnected)
        {
            connect_to_Arduino();
        }
    }

    public void desconectar()
    {
        if (isConnected)
        {
            disconnect_from_Arduino();
        }
    }

    void connect_to_Arduino()
    {
        isConnected = true;
        port = new SerialPort(portname, 9600, Parity.None, 8, StopBits.One);
        port.Open();
    }

    void disconnect_from_Arduino()
    {
        isConnected = false;
        port.Close();
    }

    public void Jugar()
    {
        if (isConnected)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            aviso.enabled = true;
        }
        
    }

    void Start()
    {
        
        
    }
    void Update()
    {
        if (isConnected)
        {
            PuertoString.puertoArduino = portname;
        }
    }
}
