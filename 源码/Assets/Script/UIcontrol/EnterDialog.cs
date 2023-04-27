using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDialog : MonoBehaviour
{
    public GameObject dialog;



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && dialog != null)
        {
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && dialog != null)
        {
            dialog.SetActive(false);
        }
    }
}
