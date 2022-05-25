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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (counter > timeFrame && collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            bool used = false;
            foreach (Transform child in player.transform)
            {
                AbilityGhostPepper comp = child.GetComponent<AbilityGhostPepper>();
                if (comp && comp.used)
                {
                    Destroy(child.gameObject);
                    Destroy(gameObject);
                    used = true;
                    return;
                }
            }
            if (!used)
            {
                collision.gameObject.GetComponent<UnityPlayerControls>().KillPlayer();
            }
        }
    }
}
