using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorRollingPin : MonoBehaviour
{
    [SerializeField] private float timeFrame = 0;
    private float counter = 0;

    private void Start()
    {
        Destroy(gameObject, 20);
    }

    private void Update()
    {
        counter += Time.deltaTime;
        Debug.Log(counter + " : " + timeFrame);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (counter > timeFrame && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
        }
    }
}
