using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherrCount : MonoBehaviour
{
    public void isGet()
    {
        FindObjectOfType<PlayerCollect>().cherryCount();
        Destroy(gameObject);
    }
}
