using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartMap : MonoBehaviour
{
    private bool inContact = false;
    private UnityPlayerControls playerInput;
    [SerializeField] private string loadLevel;

    // Update is called once per frame
    void Update()
    {
        if (inContact && playerInput.useAction.ReadValue<float>() == 1)
        {
            SceneManager.LoadScene(loadLevel);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inContact = true;
        playerInput = collision.gameObject.GetComponent<UnityPlayerControls>();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inContact = false;
        playerInput = null;
    }
}
