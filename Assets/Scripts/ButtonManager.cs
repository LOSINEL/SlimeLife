using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public GameObject optionCanvas, grayCanvas;
    bool isOptionCanvasOpen = false;
    public bool IsOptionCanvasOpen { get { return isOptionCanvasOpen; } }
    void Awake()
    {
        instance = this;
    }
    public void SetOptionCanvas()
    {
        grayCanvas.SetActive(!optionCanvas.activeSelf);
        optionCanvas.SetActive(!optionCanvas.activeSelf);
        isOptionCanvasOpen = optionCanvas.activeSelf;
        Time.timeScale = (optionCanvas.activeSelf) ? 0f : 1f;
        Player.instance.ActiveAll(!optionCanvas.activeSelf);
    }

    public void CloseNpcCanvas()
    {
        Hand.instance.npcCanvas.SetActive(false);
        Player.instance.ActiveAll(true);
    }
}