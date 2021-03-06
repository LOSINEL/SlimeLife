using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcScriptManager : MonoBehaviour
{
    public static NpcScriptManager instance;
    public GameObject npcScriptBackground;
    public GameObject merchantShopButton;
    string npcScript;
    bool npcScriptTimeEnd = false;
    int npcScriptCheckNum = 0;
    int npcScriptType = 0;
    int npcScriptNum = 0;
    float npcScriptTime = 0.05f;
    Text npcScriptText;
    Text npcScriptName;
    public float NpcScriptTime { set { npcScriptTime = value; } }
    public int NpcScriptType { get { return npcScriptType; } }
    public int NpcScriptNum { get { return npcScriptNum; } }
    void Awake()
    {
        instance = this;
        npcScriptName = npcScriptBackground.GetComponentsInChildren<Text>()[0];
        npcScriptText = npcScriptBackground.GetComponentsInChildren<Text>()[1];
    }
    public void ShowNpcScript()
    {
        if (!npcScriptTimeEnd)
        {
            if (npcScriptCheckNum >= npcScript.Length)
            {
                if (npcScriptNum < Hand.instance.MinDistanceObject.GetComponent<NpcTouch>().npc.npcScript[npcScriptType].script.Length - 1)
                {
                    npcScriptText.text = "";
                    npcScript = Hand.instance.MinDistanceObject.GetComponent<NpcTouch>().npc.npcScript[npcScriptType].script[++npcScriptNum];
                    npcScriptCheckNum = 0;
                }
            }
            npcScriptTimeEnd = true;
            StartCoroutine(NpcScriptText());
        }
    }
    public void ExitNpcScript()
    {
        npcScriptCheckNum = 0;
        npcScriptType = 0;
        npcScriptNum = 0;
        npcScriptBackground.SetActive(false);
        Player.instance.ActiveAll(true);
    }
    public bool ScriptEnd()
    {
        if (npcScript.Length == npcScriptCheckNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShowAllNpcScript()
    {
        npcScriptText.text = npcScript;
        npcScriptCheckNum = npcScript.Length;
    }
    public void SetNpcCanvas(Npc _npc)
    {
        npcScriptBackground.SetActive(true);
        if (_npc.npcType == Npc.NpcType.Merchant)
        {
            merchantShopButton.SetActive(true);
        }
        else
        {
            merchantShopButton.SetActive(false);
        }
        npcScriptBackground.GetComponentsInChildren<Image>()[1].sprite = _npc.npcImage;
        npcScriptName.text = _npc.npcName;
        npcScriptText.text = "";
        npcScript = _npc.npcScript[NpcScriptType].script[NpcScriptNum];
        Player.instance.ActiveAll(false);
    }

    public void NextNpcScript()
    {
        if (!ScriptEnd())
        {
            ShowAllNpcScript();
        }
        else
        {
            ShowNpcScript();
        }
    }

    IEnumerator NpcScriptText()
    {
        if (npcScriptCheckNum < npcScript.Length)
        {
            npcScriptText.text += npcScript[npcScriptCheckNum];
            npcScriptCheckNum += 1;
            yield return new WaitForSeconds(npcScriptTime);
        }
        else
        {
            yield return null;
        }
        npcScriptTimeEnd = false;
    }
}