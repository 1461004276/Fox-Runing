using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentHouseCol : MonoBehaviour
{
    //fox用来获取fox的位置
    public GameObject fox;
    //bc用来获取house的碰撞体
    private BoxCollider2D bc;

    void Start()
    {
        //获取到当前环境物业的碰撞体
        bc = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        //当fox的y坐标小于等于house的y坐标时，house的碰撞体变为触发器
        if  (fox.transform.position.y<=bc.transform.position.y)
        {
            bc.isTrigger = true;
            
        }
        //当fox的y坐标大于house的y坐标时，house的碰撞体变为非触发器
        if((fox.transform.position.y-bc.transform.position.y)>1f)
        {
            bc.isTrigger = false;
        }
        //当按下x键时，house的碰撞体变为触发器
        if (Input.GetKey(KeyCode.S))
        {
            bc.isTrigger = true;
        }
    }
    
}
