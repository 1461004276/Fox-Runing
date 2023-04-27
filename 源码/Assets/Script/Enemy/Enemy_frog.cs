using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy_base
{
    //组件属性
    public Transform leftPoint, rightPoint;
    public LayerMask ground;
    //数值属性
    private bool faceLeft = true;
    [SerializeField]private int ideacount = 0;
    public int ideaTime = 180;
    public float speed,jumpForce;
    private float leftX, rightX;
    protected  override void Start()
    {
        base.Start();
        //最暴力方法：直接将子物体的父物体设置为null
        //transform.DetachChildren();
        //第二个方法：开始就获得左右两个点的值
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        //获取后销毁子物体
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }
    

    private void FixedUpdate()
    {
        switchAnim();
    }

    //角色移动
    void moveMent()
    {
        if (faceLeft)
        {
            //向左移动
            if (transform.position.x > leftX)
            {
                //向左移动
                if (coll.IsTouchingLayers(ground) )
                {
                    anim.SetBool("jump",true);
                    anim.SetBool("fall",false);
                    rb.velocity = new Vector2(-speed,jumpForce);
                }

            }

            //自己想的方法，将动画设置过度，通过一个变量进行控制
                //anim.SetFloat("runing",(float)speed);
                if (transform.position.x <= leftX && ideacount < ideaTime)
            {
                //停止移动
                rb.velocity = new Vector2(0, 0);
                //anim.SetFloat("runing",0);
                ideacount++;
            }
           if(transform.position.x <= leftX&&ideacount>=ideaTime)
            {
                //反转角色
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
                ideacount = 0;
            }
        }
        else
        {
            if (transform.position.x < rightX)
            {
                //向右移动
                if (coll.IsTouchingLayers(ground) )
                {
                    anim.SetBool("jump",true);
                    anim.SetBool("fall",false);
                    rb.velocity = new Vector2(speed,jumpForce);
                }

                //自己想的方法，将动画设置过度，通过一个变量进行控制
                //anim.SetFloat("runing",(float)speed);
            }else if (transform.position.x >= rightX && ideacount < ideaTime)
            {
                //停止移动
                rb.velocity = new Vector2(0, 0);
                //anim.SetFloat("runing",0);
                ideacount++;
            }
            else if (transform.position.x >= rightX&&ideacount>=ideaTime)
            {
                //反转角色
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
                ideacount = 0;
            }
        }
    }

    void switchAnim()
    {
        if (anim.GetBool("jump") && rb.velocity.y <= 0.1f)
        {
            anim.SetBool("jump",false);
            anim.SetBool("fall",true);
        }else if(anim.GetBool("fall")&&coll.IsTouchingLayers(ground))
        {
            anim.SetBool("fall",false);
        }
    }

}
