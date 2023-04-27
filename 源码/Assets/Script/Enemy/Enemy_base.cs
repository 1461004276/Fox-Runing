using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_base : MonoBehaviour
{
    protected Animator anim;
    protected Collider2D coll;
    protected Rigidbody2D rb;
    
    
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    


    public void boom()
    {
        anim.SetTrigger("death");
    }
    
    public  void death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

}
