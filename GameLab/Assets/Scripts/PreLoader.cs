using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
    public GameObject clientPrefab;
    public GameObject loopBackclientPrefab;
    public GameObject serverPrefab;
    public string ipField;

    public void OnClickClient()
    {
        DontDestroyOnLoad(Instantiate(clientPrefab).gameObject);
        FindObjectOfType<BaseClient>().ipAddress = ipField;
        SceneManager.LoadScene("CharacterTest");
    }

    //public void OnClickServer()
    //{
    //    DontDestroyOnLoad(Instantiate(serverPrefab).gameObject);
    //    SceneManager.LoadScene("CharacterTest");
    //}

    public void OnClickClientServer()
    {
        DontDestroyOnLoad(Instantiate(serverPrefab).gameObject);
        DontDestroyOnLoad(Instantiate(loopBackclientPrefab).gameObject);
        //FindObjectOfType<BaseClient>().ipAddress = ipField;
        SceneManager.LoadScene("CharacterTest");
    }

    public void OnValueChangeAssignIP(string value)
    {
        //Debug.Log("The value is being change to " + value);
        ipField = value;
    }
}
