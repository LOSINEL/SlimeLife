using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    GameObject minDistanceObject;
    public Dictionary<GameObject, float> colObject = new Dictionary<GameObject, float>();
    float[] distance_arr;
    float minDistance;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        distance_arr = colObject.Values.ToArray();
        if (colObject.Count == 0) return;
        if (colObject.Count == 1)
        {
            minDistance = distance_arr[0];
            minDistanceObject = colObject.FirstOrDefault(x => x.Value == minDistance).Key;
            Debug.Log(minDistanceObject.name);
            return;
        }
        for (int i = 0; i < colObject.Count - 1; i++)
        {
            if (distance_arr[i] < distance_arr[i + 1])
                minDistance = distance_arr[i];
            else
                minDistance = distance_arr[i + 1];
        }
        minDistanceObject = colObject.FirstOrDefault(x => x.Value == minDistance).Key;
        Debug.Log(minDistanceObject.name);
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