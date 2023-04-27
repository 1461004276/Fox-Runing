using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform cmv;
    public bool lockY;
    private float startPointX,startPointY;
    public float RoateSpeed;
    void Start()
    {
        startPointX  = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX+ cmv.position.x * RoateSpeed, startPointY);
        }
        else
        {
            transform.position = new Vector2(startPointX+ cmv.position.x * RoateSpeed, startPointY+ cmv.position.y * RoateSpeed);
        }
    }
}
