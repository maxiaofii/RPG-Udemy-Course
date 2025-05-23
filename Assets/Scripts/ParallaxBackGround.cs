using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    void Start()
    {
         cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    void Update()
    {
        //无限长背景实现原理，很有学习意义
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x*parallaxEffect;
        transform.position= new Vector3(xPosition + distanceToMove, transform.position.y );
        if(distanceMoved> xPosition + length)
        {
            xPosition += length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
