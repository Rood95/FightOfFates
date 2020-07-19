﻿using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ArrowMp : MonoBehaviour
{


    public float speed = 0f;
    public Rigidbody2D rb;

    public Sprite normal;
    public Sprite update;

    private bool arrowUpdate = UpgradeController.GetArrowUpdate();


    private float[] attackDetails = new float[2];

    // Start is called before the first frame update
    void Start()
    {          
        if (arrowUpdate)
        {
            object arrowStyle;
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Constance.ARCHERFIREARROW, out arrowStyle))
            {
                arrowStyle = (string)arrowStyle;
                if (arrowStyle.Equals("fire"))
                {
                    GetComponent<SpriteRenderer>().sprite = update;
                    speed = 38f;
                }
            }
            
        }
        else
        {
            object arrowStyle;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Constance.ARCHERFIREARROW, out arrowStyle))
            {
                arrowStyle = (string)arrowStyle;
                if (arrowStyle.Equals("no"))
                {
                    GetComponent<SpriteRenderer>().sprite = normal;
                }
            }            
            
        }
        if (UpgradeController.GetRapidFire())
        {
            speed += 8;
        }

        //rb.velocity = transform.right * speed;
    }

    public void SetDirection(bool facingRight)
    {
        Debug.Log("Arrow direction right:" + facingRight);
        //rb.velocity = (facingRight ? transform.right : -transform.right) * speed;
        if (facingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = -transform.right * speed;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        Debug.Log("Arrow velocity:" + rb.velocity);
        Debug.Log("Arrow direcction" + rb.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        BasicEnemyController enemy = hitInfo.GetComponentInParent<BasicEnemyController>();


        if (enemy != null)
        {
            attackDetails[0] = 1f;
            attackDetails[1] = transform.position.x;
            enemy.Damage(attackDetails);
        }

        MPlayer adversary = hitInfo.GetComponentInParent<MPlayer>();

        if (adversary != null)
        {
            attackDetails[0] = 1f;
            attackDetails[1] = transform.position.x;
            adversary.DamageWithKnockback(attackDetails);
        }

        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

}
