using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent OnActivate;

    public UnityEvent OnDeactivate;

    bool CheckInput;

    bool Active;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CheckInput = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CheckInput = false;
        }
    }

    public void SetStatus(bool active)
    {
        Active = active;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {

        }
    }
}
