using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{
    public int cherryNum;
    public int gemNum;
    public Text CherryNumText;
    public Text GemNumText;
    public AudioSource getAudio;
    
    void Start()
    {
        cherryNum = 0;
        gemNum = 0;
    }

    private void Update()
    {
        //更新Cherry数量的UI
        CherryNumText.text = cherryNum.ToString();
        //更新Gem数量的UI
        GemNumText.text = gemNum.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //如果碰到的是Cherry
        if (col.tag == "Cherry")
        {
            //销毁碰到的Cherry
            getAudio.Play();
            col.GetComponent<Animator>().Play("isGet");
        }
        if (col.tag == "Gem")
        {
            //销毁碰到的Gem
            getAudio.Play();
            col.GetComponent<Animator>().Play("isGet");

        }
        if (col.gameObject.tag == "deadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("restart",1.5f);
        }
    }
    
    //重载场景
    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void cherryCount()
    {
        //Cherry数量加一
        cherryNum++;
    }
    
    public void gemCount()
    {
        //Gem数量加一
        gemNum++;
    }
    
}
