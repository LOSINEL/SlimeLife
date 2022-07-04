using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    GameObject minDistanceObject;
    public Dictionary<GameObject, float> colObject = new Dictionary<GameObject, float>();
    float[] distance_arr;
    float minDistance;
    float npcScriptTime = 0.25f;
    string minDistanceObjectName = "";
    public GameObject pushButton;
    public GameObject itemInfoText;
    public GameObject npcCanvas;
    string[] npcScript;
    bool npcScriptTimeEnd = false;
    int npcScriptCheckNum = 0;
    int npcScriptType = 0;
    int npcScriptNum = 0;

    public float NpcScriptTime { get { return NpcScriptTime; } set { npcScriptTime = value; } }

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        distance_arr = colObject.Values.ToArray();
        if (colObject.Count == 0)
        {
            pushButton.SetActive(false);
            minDistanceObjectName = "";
            return;
        }
        if (colObject.Count == 1)
        {
            minDistance = distance_arr[0];
            minDistanceObject = colObject.FirstOrDefault(x => x.Value == minDistance).Key;
            if (minDistanceObject.name != minDistanceObjectName)
            {
                RefreshPushButton();
            }
            else
            {
                minDistanceObjectName = minDistanceObject.name;
            }
        }
        else if (colObject.Count >= 2)
        {
            minDistance = distance_arr[0];
            for (int i = 0; i < colObject.Count - 1; i++)
            {
                if (!(minDistance < distance_arr[i + 1]))
                {
                    minDistance = distance_arr[i + 1];
                }
            }
            minDistanceObject = colObject.FirstOrDefault(x => x.Value == minDistance).Key;
            if (minDistanceObject.name != minDistanceObjectName)
            {
                RefreshPushButton();
            }
            else
            {
                minDistanceObjectName = minDistanceObject.name;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && !npcCanvas.activeSelf)
        {
            if (minDistanceObject.tag.Equals("Item"))
            {
                Debug.Log(minDistanceObject.GetComponent<ItemPickUp>().item.itemName + "∏¶ »πµÊ«ﬂ¥Ÿ");
                colObject.Remove(minDistanceObject);
                Destroy(minDistanceObject);
            }
            if (minDistanceObject.tag.Equals("Npc"))
            {
                npcCanvas.SetActive(true);
                npcCanvas.GetComponentsInChildren<Image>()[1].sprite = minDistanceObject.GetComponent<NpcTouch>().npc.npcImage;
                npcCanvas.GetComponentsInChildren<Text>()[0].text = minDistanceObject.GetComponent<NpcTouch>().npc.npcName;
                npcCanvas.GetComponentsInChildren<Text>()[1].text = "";
                npcScript = minDistanceObject.GetComponent<NpcTouch>().npc.npcScript[npcScriptType].script[npcScriptNum].Split(' ');
                Player.instance.ActiveAll(false);
            }
        }
        if(npcCanvas.activeSelf)
        {
            if (!npcScriptTimeEnd)
            {
                if (npcScriptCheckNum >= npcScript.Length)
                {
                    if (Input.GetKeyUp(KeyCode.Q))
                    {
                        npcScriptCheckNum = 0;
                        npcScriptType = 0;
                        npcScriptNum = 0;
                        npcCanvas.SetActive(false);
                        Player.instance.ActiveAll(true);
                    }
                    if (Input.GetKeyUp(KeyCode.E) && npcScriptNum < minDistanceObject.GetComponent<NpcTouch>().npc.npcScript[npcScriptType].script.Length - 1)
                    {
                        npcCanvas.GetComponentsInChildren<Text>()[1].text = "";
                        npcScript = minDistanceObject.GetComponent<NpcTouch>().npc.npcScript[npcScriptType].script[++npcScriptNum].Split(' ');
                        npcScriptCheckNum = 0;
                    }
                    return;
                }
                else
                {
                    npcCanvas.GetComponentsInChildren<Text>()[1].text += npcScript[npcScriptCheckNum++] + " ";
                }
                npcScriptTimeEnd = true;
                StartCoroutine(NpcScriptText());
            }
        }
    }
    IEnumerator NpcScriptText()
    {
        yield return new WaitForSeconds(npcScriptTime);
        npcScriptTimeEnd = false;
    }
    void RefreshPushButton()
    {
        pushButton.SetActive(true);
        if (minDistanceObject.tag.Equals("Item"))
        {
            itemInfoText.GetComponent<Text>().text = minDistanceObject.GetComponent<ItemPickUp>().item.itemName + "\n" + minDistanceObject.GetComponent<ItemPickUp>().item.itemInfo;
        }
        else
        {
            itemInfoText.GetComponent<Text>().text = minDistanceObject.GetComponent<NpcTouch>().npc.npcName + "\n" + minDistanceObject.GetComponent<NpcTouch>().npc.npcInfo;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Item")||other.tag.Equals("Npc"))
        {
            if (colObject.ContainsKey(other.gameObject))
            {
                colObject[other.gameObject] = Vector3.Distance(Player.instance.transform.position, other.gameObject.transform.position); return;
            }
            colObject.Add(other.gameObject, Vector3.Distance(Player.instance.transform.position, other.gameObject.transform.position));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Item") || other.tag.Equals("Npc"))
        {
            colObject.Remove(other.gameObject);
        }
    }
}