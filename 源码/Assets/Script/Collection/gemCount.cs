using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemCount : MonoBehaviour
{
    public void isGet()
    {
        FindObjectOfType<PlayerCollect>().gemCount();
        Destroy(gameObject);
    }
}
