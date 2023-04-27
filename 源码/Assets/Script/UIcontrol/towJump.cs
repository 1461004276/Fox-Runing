using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towJump : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            player.GetComponent<PlayerMove>().isTwoJump = true;
            Destroy(gameObject);
        }
    }
}
