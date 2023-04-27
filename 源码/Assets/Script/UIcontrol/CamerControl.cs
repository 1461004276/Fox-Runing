using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControl : MonoBehaviour
{

    public Transform player;
    

    void Update()
    {
        //对于镜头的简单跟随
        if(player.position.y<=1.5f&&player.position.y>=0f) transform.position = new Vector3(player.position.x,player.position.y,-10f);
        else if(player.position.y > 1.5) transform.position = new Vector3(player.position.x, 1.5f, -10f);
        else if(player.position.y < 0) transform.position = new Vector3(player.position.x, 0.1f, -10f);
    }
}
