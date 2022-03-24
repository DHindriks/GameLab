using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent OnActivate;

    public UnityEvent OnDeactivate;

    bool CheckInput;

    bool Active;

    [ContextMenu("Deactivate")]
    void Deactivate()
    {
        SetStatus(false);
    }

    [ContextMenu("Activate")]
    void Activate()
    {
        SetStatus(true);
    }

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

        if(active)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            OnActivate.Invoke();
        }else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            OnDeactivate.Invoke();
        }
    }

    //private void Update()
    //{
    //    if(Input.GetButtonDown("Interact"))
    //    {

    //    }
    //}
}
