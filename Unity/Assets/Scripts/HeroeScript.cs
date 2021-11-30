using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class HeroeScript : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public float doubleJump;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;


    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private SpriteRenderer spriteRenderer;

    private bool canDoubleJump;
    private bool isGrounded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (CheckGround.isGrounded)
        {
            isGrounded = true;
            canDoubleJump = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKey("w") || SerialCom.y > 600)
        {
            if (isGrounded)
            {
                canDoubleJump = true;
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpSpeed);
            }
            else
            {
                if (Input.GetKeyDown("w") || SerialCom.estadoB == "1")
                {
                    if (canDoubleJump)
                    {
                        Animator.SetBool("doublejump", true);
                        Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, doubleJump);
                        canDoubleJump = false;
                    }
                }
            }
        }

        if (!isGrounded)
        {
            Animator.SetBool("jump", true);
            Animator.SetBool("running", false);
        }
        if (isGrounded)
        {
            Animator.SetBool("jump", false);
            Animator.SetBool("doublejump", false);
            Animator.SetBool("falling", false);
        }
        if (Rigidbody2D.velocity.y < 0)
        {
            Animator.SetBool("falling", true);
        }

        if (Rigidbody2D.velocity.y > 0)
        {
            Animator.SetBool("falling", false);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d") || SerialCom.x > 600)
        {
            Rigidbody2D.velocity = new Vector2(Speed, Rigidbody2D.velocity.y);
            spriteRenderer.flipX = false;
            Animator.SetBool("running", true);
        }
        else if (Input.GetKey("a") || (SerialCom.x < 400 && SerialCom.isConnected))
        {
            Rigidbody2D.velocity = new Vector2(-Speed, Rigidbody2D.velocity.y);
            spriteRenderer.flipX = true;
            Animator.SetBool("running", true);
        }
        else
        {
            Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
            Animator.SetBool("running", false);
        }

        /*if (betterJump)
        {
            if (Rigidbody2D.velocity.y < 0)
            {
                Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }

            if (Rigidbody2D.velocity.y > 0 && !Input.GetKey("w"))
            {
                Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }*/
    }
}
