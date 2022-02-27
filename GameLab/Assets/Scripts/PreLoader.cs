using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
    public GameObject clientPrefab;
    public GameObject serverPrefab;
    public string ipField;

    public void OnClickClient()
    {
        DontDestroyOnLoad(Instantiate(clientPrefab).gameObject);
        //FindObjectOfType<BaseClient>().ipAddress = ipField;
        //SceneManager.LoadScene("Lobby");
    }
    public void OnClickServer()
    {
        DontDestroyOnLoad(Instantiate(serverPrefab).gameObject);
        //FindObjectOfType<BaseServer>().Init();
        //SceneManager.LoadScene("Lobby");
    }
    public void OnClickClientServer()
    {
        DontDestroyOnLoad(Instantiate(serverPrefab).gameObject);
        //FindObjectOfType<BaseServer>().Init();
        DontDestroyOnLoad(Instantiate(clientPrefab).gameObject);
        //FindObjectOfType<BaseClient>().ipAddress = ipField;
        //SceneManager.LoadScene("Lobby");
    }

    public void OnValueChangeAssignIP(string value)
    {
        //Debug.Log("The value is being change to " + value);
        ipField = value;
    }
}
