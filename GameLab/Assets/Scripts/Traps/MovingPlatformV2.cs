using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformV2 : BaseTrap
{
    [SerializeField] List<Transform> checkpoints;
    [SerializeField][Range(0, 20)] float speed;
    [SerializeField] bool movesWhenActive = false;
    float currentSpeed;
    int direction = 1;
    int counter = 0;

    public override void Update()
    {
        base.Update();
        MovePlatform();
    }

    void MovePlatform()
    {
        transform.position = Vector2.MoveTowards(transform.position, checkpoints[counter].position, currentSpeed * Time.deltaTime);
        if (transform.position == checkpoints[counter].position)
        {
            if (counter == 0 || counter == checkpoints.Count - 1)
            {
                direction *= -1;
                counter += direction;
            }
            else
            {
                counter += direction;
            }
        }

        if (counter < 0 || counter > checkpoints.Count - 1)
        {
            direction *= -1;
            counter += direction * 2;
        }
    }

    public override void Active()
    {
        base.Active();
        if (movesWhenActive)
            currentSpeed = speed;
        else
            currentSpeed = 0;
    }
    public override void Inactive()
    {
        if (movesWhenActive)
            currentSpeed = 0;
        else
            currentSpeed = speed;
    }
    public override void Primed()
    {

    }
}
