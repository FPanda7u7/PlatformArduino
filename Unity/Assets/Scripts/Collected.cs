using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collected : MonoBehaviour
{
    public static int led;
    private bool toco = true;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (toco)
            {
                toco = false;
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                led += 25;
                if (led == 250)
                {
                    led = 255;
                }
                SerialCom.coin = true;
                Destroy(gameObject, 0.5f);
            }
            
        }
    }
}
