﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Player
{
    public float speed;
    float updateSpeed = 20;
    private float moveInput;


    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator;


    //---------------------- DEATH ----------------
    private bool loop = false;
    private bool secondLoop = false;
    private float deathTime;


    void  FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (UpgradeController.GetSpeedUp())
        {
            speed = updateSpeed;
        }


        if (!knockback)
        {
            moveInput = joystick.Horizontal;
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveInput * speed));
        }
          
        if (facingRight == false && moveInput > 0)
        {
            base.Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            base.Flip();
        }

    }


    void Update()
    {

        if (!finishGame)
        {
            base.smothJump();
            // this.GetDamage();
            base.CheckKnockback();

            if (isGrounded == true)
            {
                extraJumps = extraJumpsValue;
                animator.SetBool("IsGrounded", true);
            }
            else
            {
                animator.SetBool("IsGrounded", false);
            }
        }
        else
        {
            if (victory)
            {
                Victory();
                if (deathTime + 1f < Time.time)
                {
                    VictoryLoop();
                }
            }
            else
            {
                Die();

                if (deathTime + 2f < Time.time)
                {
                    DeathDance();

                }
            }
        }
        
    }





    public void Die()
    {
        if (!loop)
        {

            loop = true;
            MusicSelector musicSelector = GameObject.Find("MusicSelector").GetComponent<MusicSelector>();
            musicSelector.startMemeMusic();
            animator.SetBool("IsDead", true);
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<CapsuleCollider2D>());
            deathTime = Time.time;
        }
    }


    public void DeathDance()
    {
        Instantiate(coffin, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }


    public void Victory()
    {
        if (!loop)
        {
            loop = true;
            MusicSelector musicSelector = GameObject.Find("MusicSelector").GetComponent<MusicSelector>();
            musicSelector.startQueen();
            animator.SetBool("IsWinning", true);
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<CapsuleCollider2D>());
            deathTime = Time.time;
        }
    }

    public void VictoryLoop()
    {
        if (!secondLoop)
        {
            secondLoop = true;
            animator.SetBool("IsWinning", false);
            animator.SetBool("IsWinningLoop", true);

        }

    }




}
