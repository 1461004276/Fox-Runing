using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    [SerializeField]private int gameOverCount = 0;
    [SerializeField]private bool isOver = false;
    private void FixedUpdate()
    {
        if(isOver) gameOverCount++;
        if(gameOverCount >= 120) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isOver = true;
        }
    }
}
