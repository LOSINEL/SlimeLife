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
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(minDistanceObject.GetComponent<ItemPickUp>().item.itemName + "∏¶ »πµÊ«ﬂ¥Ÿ");
            colObject.Remove(minDistanceObject);
            Destroy(minDistanceObject);
        }
    }
    void RefreshPushButton()
    {
        pushButton.SetActive(true);
        itemInfoText.GetComponent<Text>().text = minDistanceObject.GetComponent<ItemPickUp>().item.itemName + "\n" + minDistanceObject.GetComponent<ItemPickUp>().item.itemInfo;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Item"))
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
        if(other.tag.Equals("Item"))
        {
            colObject.Remove(other.gameObject);
        }
    }
}