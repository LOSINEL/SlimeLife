using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcScriptManager : MonoBehaviour
{
    public static NpcScriptManager instance;
    public GameObject npcCanvas;
    string npcScript;
    bool npcScriptTimeEnd = false;
    int npcScriptCheckNum = 0;
    int npcScriptType = 0;
    int npcScriptNum = 0;
    float npcScriptTime = 0.05f;
    Text npcScriptText;
    Text npcScriptName;
    public float NpcScriptTime { get { return npcScriptTime; } set { npcScriptTime = value; } }
    public string NpcScript { set { npcScript = value; } }
    public int NpcScriptType { get { return npcScriptType; } }
    public int NpcScriptNum { get { return npcScriptNum; } }
    void Awake()
    {
        instance = this;
        npcScriptName = npcCanvas.GetComponentsInChildren<Text>()[0];
        npcScriptText = npcCanvas.GetComponentsInChildren<Text>()[1];
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
        npcCanvas.SetActive(false);
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
    public void SetNpcCanvas()
    {
        npcCanvas.SetActive(true);
        npcCanvas.GetComponentsInChildren<Image>()[1].sprite = Hand.instance.MinDistanceObject.GetComponent<NpcTouch>().npc.npcImage;
        npcScriptName.text = Hand.instance.MinDistanceObject.GetComponent<NpcTouch>().npc.npcName;
        npcScriptText.text = "";
        npcScript = Hand.instance.MinDistanceObject.GetComponent<NpcTouch>().npc.npcScript[NpcScriptType].script[NpcScriptNum];
        Player.instance.ActiveAll(false);
    }
    IEnumerator NpcScriptText()
    {
        yield return new WaitForSeconds(npcScriptTime);
        if (npcScriptCheckNum < npcScript.Length)
        {
            npcScriptText.text += npcScript[npcScriptCheckNum];
            npcScriptCheckNum += 1;
        }
        else
        {
            yield return null;
        }
        npcScriptTimeEnd = false;
    }
}