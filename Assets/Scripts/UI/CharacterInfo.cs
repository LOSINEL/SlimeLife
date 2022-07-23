using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public GameObject characterInfoBackground;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TryCharacterInfo();
    }
    public void TryCharacterInfo()
    {
        characterInfoBackground.SetActive(!characterInfoBackground.activeSelf);
    }
}