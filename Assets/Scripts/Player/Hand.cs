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
    string minDistanceObjectName = "";
    public GameObject pushButton;
    public GameObject itemInfoText;
    public Inventory inventory;
    public GameObject MinDistanceObject { get { return minDistanceObject; } }

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Item Ω¿µÊ
            if (minDistanceObject.tag.Equals("Item"))
            {
                inventory.GetComponent<Inventory>().AcquireItem(minDistanceObject.GetComponent<ItemPickUp>().item);
                colObject.Remove(minDistanceObject);
                Destroy(minDistanceObject);
            }
            // Npc ¥Î»≠
            if (!NpcScriptManager.instance.npcScriptBackground.activeSelf)
            {
                if (minDistanceObject.tag.Equals("Npc"))
                {
                    NpcScriptManager.instance.SetNpcCanvas();
                }
            }
            else
            {
                NpcScriptManager.instance.NpcNextScript();
            }
        }
        if (NpcScriptManager.instance.npcScriptBackground.activeSelf && !NpcScriptManager.instance.ScriptEnd())
        {
            NpcScriptManager.instance.ShowNpcScript();
        }
        if (Input.GetKeyDown(KeyCode.Q) && NpcScriptManager.instance.npcScriptBackground.activeSelf)
        {
            NpcScriptManager.instance.ExitNpcScript();
        }
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