using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    GameObject optionCanvas;
    GameObject grayCanvas;
    bool isOptionCanvasOpen = false;
    public bool IsOptionCanvasOpen { get { return isOptionCanvasOpen; } }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            optionCanvas = GameObject.Find("Option_Canvas");
            grayCanvas = GameObject.Find("Gray_Canvas");
            SetOptionCanvas();
        }
    }
    public void SetOptionCanvas()
    {
        grayCanvas.SetActive(!optionCanvas.activeSelf);
        optionCanvas.SetActive(!optionCanvas.activeSelf);
        isOptionCanvasOpen = optionCanvas.activeSelf;
        Time.timeScale = (optionCanvas.activeSelf) ? 0f : 1f;
        Player.instance.ActiveAll(!optionCanvas.activeSelf);
    }
}