using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBestTime;
    [SerializeField] TextMeshProUGUI textLastTime;
    [SerializeField] TextMeshProUGUI textCurrentTime;

    public float bestTime = 0;
    public float lastTime = 0;
    public float currentTime = 0;
    bool started = false;

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            currentTime += Time.deltaTime;
        }

        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lapDone();
        }
    }

    void lapDone()
    {
        if (started)
        {
            lastTime = currentTime;
            if (bestTime > lastTime || bestTime == 0)
            {
                bestTime = lastTime;
            }
        }

        started = true;
        currentTime = 0;
    }

    void UpdateUI()
    {
        textBestTime.text = "Best: " + Mathf.Round(bestTime * 100) / 100;
        textLastTime.text = "Last: " + Mathf.Round(lastTime * 100) / 100;
        textCurrentTime.text = "Current: " + Mathf.Round(currentTime * 100) / 100;
    }
}
