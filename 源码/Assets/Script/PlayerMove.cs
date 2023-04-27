using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    
    public float speed;
    public float jumpForce;//跳跃力
    public bool spaceAction = false;//判断是否跳跃键按起
    public bool crouchAction = false;//判断是否下蹲键按起
    public bool isGround = false;//判断是否在地面上
    public LayerMask ground;
    public Transform cellingCheck;
    public Transform groundCheck;
    
    public CircleCollider2D coll;
    public BoxCollider2D boxcoll;
    public AudioSource attckAudio,jumpAudio,hurtAudio;


    private int extraJump;
    public bool isTwoJump;
    [SerializeField]private int deadingCount;
    [FormerlySerializedAs("rd")] [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator anim;//获取动画组件
    // Start is called before the first frame update
    void Start()
    {
        //获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //修复fixedupdate对getbutton down的有时候无法触发
        //判断是否按下跳跃键
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        if (isGround && Input.GetButtonDown("Jump"))
        {
            spaceAction = true;
        }
        else if(extraJump> 0 && Input.GetButtonDown("Jump") && isTwoJump)
        {
            spaceAction = true;
        }
        switchAnim();
        
        //判断是否按下下蹲键
        
        
        if (Input.GetKey(KeyCode.S))
        {
            crouchAction = true;
        }
        if (!crouchAction && !boxcoll.IsTouchingLayers(ground))
        {
            anim.SetBool("crouch",crouchAction);
            boxcoll.offset = new Vector2(0.0376f, -0.0583f);
            boxcoll.size = new Vector2(0.89f, 0.89f);
            coll.offset = new Vector2(0, -0.6659f);
            coll.radius = 0.34f;
        }
        
    }
    void FixedUpdate()
    {
        if(!anim.GetBool("isHurt")) moveMent();
        deading();
        
    }
    //角色移动
    void moveMent()
    {
        //获取水平轴的输入
        float horizontalMove = Input.GetAxis("Horizontal");
        //获取跳跃轴的输入
        float faceDircetion = Input.GetAxisRaw("Horizontal");

        
        //角色水平移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove*Time.fixedDeltaTime * speed, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(faceDircetion));
        }
        //角色朝向
        if (faceDircetion != 0) transform.localScale = new Vector3(faceDircetion, 1, 1);
        //角色跳跃
        
        if(!isTwoJump) oneJump();
        if(isTwoJump) twoJump();
        

        //角色下蹲
        if (crouchAction )
        {
            anim.SetBool("crouch",crouchAction);
            boxcoll.offset = new Vector2(0,-0.8f);
            boxcoll.size = new Vector2(0.2f,0.2f);
            coll.offset = new Vector2(0,-0.8f);
            coll.radius = 0.2f;
            
            if(!Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground)) crouchAction = false;
        }
        //角色掉下虚空
       /* if (transform.position.y < -10)
        {
            deadingCount++;
            transform.position = new Vector3(-12f, 1f, 0);
        }*/
    }
    
    //旧的角色跳跃，手感很差
    /*
     void jump()
      {
        if (spaceAction)
        {
            //判断是否在地面上
            if (coll.IsTouchingLayers(ground))
            {
                jumpAudio.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
                anim.SetBool("jumping",true);
            }
            spaceAction = false;
        }
    }
     */
    //新的跳跃
    void oneJump()
    {
        if (isGround)
        {
            if(spaceAction)
            {
                jumpAudio.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
                anim.SetBool("jumping",true);
            }
            spaceAction = false;
        }
    }
    void twoJump()
    {
        if (isGround)
        {
            extraJump = 1;
        }
        //判断是否按下跳跃键并且实在空中
        if (spaceAction && extraJump > 0)
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
            anim.SetBool("jumping",true);
            extraJump--;
        }
        //判断是否按下跳跃键并落在地面上
        else if (spaceAction && extraJump == 0 && isGround)
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
            anim.SetBool("jumping",true);
            
        }
        spaceAction = false;
    }

    //切换动画
    void switchAnim()
    {
        anim.SetBool("idle",false);
        if(!coll.IsTouchingLayers(ground)&& rb.velocity.y<0.1f) anim.SetBool("falling",true);
        
        if (anim.GetBool("jumping"))
        {
            //判断是否在下落
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if (coll.IsTouchingLayers(ground))//判断是否在地面上
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
        
    }
    //角色攻击敌人
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemies")
        {
            Debug.Log("transform.position.x: " + transform.position.x);
            Debug.Log("coll.gameObject.transform.position.x: " + col.gameObject.transform.position.x);
            
            //获取敌人所有组件
            Enemy_base enemy = col.gameObject.GetComponent<Enemy_base>();
            if (anim.GetBool("falling"))
            {
                
                //敌人死亡
                attckAudio.Play();
                enemy.boom();
                //角色跳跃
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping",true);
            }
            else if(transform.position.x<=col.gameObject.transform.position.x)
            {
                hurtAudio.Play();
                rb.velocity = new Vector2(-10,rb.velocity.y);
                anim.SetBool("isHurt",true);
                //利用deadingCount来延迟角色复活
                deadingCount++;
            }
            else if(transform.position.x>col.gameObject.transform.position.x)
            { 
                hurtAudio.Play();
                rb.velocity = new Vector2(10,rb.velocity.y);
                anim.SetBool("isHurt",true);
                //利用deadingCount来延迟角色复活
                deadingCount++;
            }
        }
        
    }
    //角色死亡
    private void deading()
    {
        //角色死亡,延迟复活
        if (deadingCount > 0)
        {
            deadingCount++;
            if (deadingCount == 30)
            {
                anim.SetBool("isHurt",false);
                //gameObject.transform.position  = new Vector3(-32f,0,0);
                anim.SetFloat("running",0);
                rb.velocity = new Vector2(0, 0);
                deadingCount = 0;
            }
        }
    }
    
}
