using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startPoint;
    [SerializeField] private Transform[] movePoints;
    private int index;

    void Start()
    {
        transform.position = movePoints[startPoint].position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, movePoints[index].position) < 0.02f)
        {
            index++;
            if(index == movePoints.Length)
                {
                    index = 0; 
                }
        }

        transform.position = Vector2.MoveTowards(transform.position, movePoints[index].position, speed * Time.deltaTime);
    }
}
