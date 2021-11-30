using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMoveV : MonoBehaviour
{
    public float speed = 0.7f;
    public Transform[] moveSpots;
    private int i = 1;
    public static bool moverse;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (moverse)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
        }    

        if (transform.position == moveSpots[i].transform.position)
        {
            moverse = false;
            if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
            {
                i++;
            }
            else
            {
                i = 0;
            }           
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform); 
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
