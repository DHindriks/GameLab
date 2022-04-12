using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent OnActivate;

    public UnityEvent OnDeactivate;

    [SerializeField] Sprite ActiveSp;
    [SerializeField] Sprite UnactiveSp;
    [SerializeField] bool AutoDisable;
    [SerializeField] float AutoDisableTime;
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

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("ENTER");
        if (other.tag == "Player")
        {
            CheckInput = true;
            if (!AutoDisable || AutoDisable && !Active)
            {
                SetStatus(!Active);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{

    //    Debug.Log("ENTER");
    //    if (other.tag == "Player")
    //    {
    //        CheckInput = true;
    //        SetStatus(!Active);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CheckInput = false;
        }
    }

    public void SetStatus(bool active)
    {
        if (active != Active)
        {
            Active = active;

            if(active)
            {
                GetComponent<SpriteRenderer>().sprite = ActiveSp;
                OnActivate.Invoke();
                if (AutoDisable)
                {
                    Invoke(nameof(Disablebtn) , AutoDisableTime);
                }
            }else
            {
                GetComponent<SpriteRenderer>().sprite = UnactiveSp;
                OnDeactivate.Invoke();
            }
        }
    }

    void Disablebtn()
    {
        SetStatus(false);
    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Interact"))
    //    {

    //    }
    //}
}
