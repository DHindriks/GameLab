using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] GameObject PowerUpObj;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("e");
            GameObject equipped = Instantiate(PowerUpObj, collision.transform.GetChild(0));
            Destroy(gameObject);
        }
    }
}
