using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public GameObject optionWindow, grayWindow;
    bool isOptionWindowOpen = false;
    void Awake()
    {
        instance = this;
    }
    public void SetOptionCanvas()
    {
        grayWindow.SetActive(!optionWindow.activeSelf);
        optionWindow.SetActive(!optionWindow.activeSelf);
        isOptionWindowOpen = optionWindow.activeSelf;
        Time.timeScale = (optionWindow.activeSelf) ? 0f : 1f;
        Player.instance.ActiveAll(!optionWindow.activeSelf);
    }

    public void NpcNextScript()
    {
        if (!NpcScriptManager.instance.ScriptEnd())
        {
            NpcScriptManager.instance.ShowAllNpcScript();
        }
        else
        {
            NpcScriptManager.instance.ShowNpcScript();
        }
    }

    public void NpcClostScript()
    {
        NpcScriptManager.instance.ExitNpcScript();
    }
}