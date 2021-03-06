using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneral : MonoBehaviour
{
    public int value = 0;
    public bool PreventRespawn = false;
    [SerializeField] Transform platformsHolder;
    List<GameObject> platforms;
    int numberOfPlatforms;
    private AudioSource coinSound;

    private void Start()
    {
        coinSound = GetComponent<AudioSource>();
        platforms = new List<GameObject>();
        platformsHolder = GameObject.Find("Platforms").transform;

        foreach (Transform child in platformsHolder)
        {
            platforms.Add(child.gameObject);
        }
    }

    public void Spawn()
    {
        int rand = Random.Range(0, platforms.Count); //Get a random number marking the index of the platform
        float offSet = Random.Range(0, platforms[rand].transform.localScale.x); //Creates the offset for placing the coin

        if (Random.Range(0,2) == 0)
        {
            offSet *= -1;
        }

        //Debug.Log(offSet);
        gameObject.transform.position = new Vector3(platforms[rand].transform.position.x + offSet, platforms[rand].transform.position.y + 2, platforms[rand].transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coinSound.Play();
            collision.GetComponent<UnityPlayerControls>().AddCoin();
            if (PreventRespawn)
            {
                Destroy(gameObject);
            }
            else
            {
                Spawn();
            }
        }
    }
}
