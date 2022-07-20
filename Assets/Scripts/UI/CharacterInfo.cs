using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public GameObject characterInfoBackground;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TryCharacterInfoWIndow();
        }
    }
    public void TryCharacterInfoWIndow()
    {
        characterInfoBackground.SetActive(!characterInfoBackground.activeSelf);
    }
}