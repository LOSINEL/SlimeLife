using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject optionWindow, grayWindow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            TryOptionCanvas();
    }
    public void TryOptionCanvas()
    {
        grayWindow.SetActive(!optionWindow.activeSelf);
        optionWindow.SetActive(!optionWindow.activeSelf);
        Time.timeScale = (optionWindow.activeSelf) ? 0f : 1f;
        Player.instance.ActiveAll(!optionWindow.activeSelf);
    }
}