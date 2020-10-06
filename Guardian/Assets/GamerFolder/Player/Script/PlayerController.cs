﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

   public Transform flooCollider;
   public Transform skin;
   public int comboNum;
   public float comboTime;
   public float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(GetComponent<Character>().life <= 0)
        {
            rb.simulated = false;
            this.enabled = false;
        
        }

        dashTime = dashTime + Time.deltaTime;
        if(Input.GetButtonDown("Fire2")&& dashTime > 1)
        {
            dashTime = 0;
            skin.GetComponent<Animator>().Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 680,0));//adiciona força no dash
        }


        comboTime = comboTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire1")&& comboTime > 0.5f)
        {

            comboNum++;
            if (comboNum > 2) {

                comboNum = 1;
            }

            comboTime = 0;

            skin.GetComponent<Animator>().Play("PlayerAttack" + comboNum, -1);

        }
        if (comboTime >= 1) 
        {

            comboNum = 0;
        
        
        }


        if (Input.GetButtonDown("Jump") && flooCollider.GetComponent<FloorCollider>().canJump ==true) {

            skin.GetComponent<Animator>().Play("PlayerJump",-1);
            rb.velocity = Vector2.zero;
            flooCollider.GetComponent<FloorCollider>().canJump = false;
            rb.AddForce(new Vector2(0, 960));//adiciona força no pulo.
        
        
        
        }

        vel = new Vector2(Input.GetAxisRaw("Horizontal")*8.5f, rb.velocity.y);//controla a velocidade do player

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);

            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }

    }


    private void FixedUpdate()
    {

        if (dashTime > 0.5)
        {
            rb.velocity = vel;
        }
    }
}


