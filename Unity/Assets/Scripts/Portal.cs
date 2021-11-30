using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private bool inDoor = false;
    public int nivel;

    void Start()
    {
        Collected.led = 0;
        SerialCom.coin = true;
    }

    void Update()
    {
        if (inDoor && SerialCom.z == 0)
        {
            SceneManager.LoadScene(nivel);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Collected.led == 255)
        {
            inDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inDoor = false;
    }
}
