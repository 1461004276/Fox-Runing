using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Enemt_eagle : Enemy_base
{
    public Transform top, bot;
    //数值属性
    private float topY, botY;
    public float speed;
    private bool isTop = true;
    public int ideaTime;
    [SerializeField]private int ideaCount = 0;
    protected override void Start()
    {
        base.Start();
        //获得上下边界点
        topY = top.position.y;
        botY = bot.position.y;
        Destroy(top.gameObject);
        Destroy(bot.gameObject);
    }

    private void FixedUpdate()
    {
        moveMent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //敌人-老鹰上下飞行
    void moveMent()
    {
        if (isTop)
        {
            if (gameObject.transform.position.y < topY )
            {
                rb.velocity = new Vector2(rb.velocity.y,speed);
            }else if(gameObject.transform.position.y >= topY && ideaCount < ideaTime)
            {
                rb.velocity = new Vector2(0, 0);
                ideaCount++;
            }
            else if(gameObject.transform.position.y >= topY && ideaCount >= ideaTime)
            {
                isTop = false;
                ideaCount = 0;
            }
        }
        else
        {
            if (gameObject.transform.position.y > botY )
            {
                rb.velocity = new Vector2(rb.velocity.y,-speed);
            }else if(gameObject.transform.position.y <= botY && ideaCount < ideaTime)
            {
                rb.velocity = new Vector2(0, 0);
                ideaCount++;
            }
            else if(gameObject.transform.position.y <= botY && ideaCount >= ideaTime)
            {
                isTop = true;
                ideaCount = 0;
            }
        }


    }

}
