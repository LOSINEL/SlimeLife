using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    public static AlertManager instance;
    public GameObject alertWindow;
    GameObject alertMessage;
    private void Awake()
    {
        instance = this;
    }
    public void CreateAlertMessage(string _message)
    {
        alertMessage = Instantiate(alertWindow, alertWindow.GetComponent<RectTransform>().position, Quaternion.identity);
        alertMessage.GetComponentInChildren<Text>().text = _message;
        alertMessage.transform.SetParent(gameObject.transform, false);
    }
}