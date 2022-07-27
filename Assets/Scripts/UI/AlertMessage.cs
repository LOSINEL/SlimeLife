using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertMessage : MonoBehaviour
{
    [SerializeField] float destroyTime = 2.5f;
    [SerializeField] float moveSpeed = 30f;
    Color imageColor;
    Color textColor;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    private void Update()
    {
        imageColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = imageColor - new Color(0, 0, 0, Time.deltaTime / destroyTime);

        textColor = gameObject.GetComponentInChildren<Text>().color;
        gameObject.GetComponentInChildren<Text>().color = textColor - new Color(0, 0, 0, Time.deltaTime / destroyTime);

        gameObject.GetComponent<RectTransform>().position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}