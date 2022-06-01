using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterAssign : MonoBehaviour
{

    [SerializeField] GameObject CharPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<UnityPlayerControls>())
        {
            collision.GetComponent<UnityPlayerControls>().SetCharacter(CharPrefab);
        }
    }
}
