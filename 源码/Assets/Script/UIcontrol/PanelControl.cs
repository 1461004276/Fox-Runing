using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    public void take()
    {
        GameObject.Find("Canvas").transform.Find("title").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Play").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Quit").gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
