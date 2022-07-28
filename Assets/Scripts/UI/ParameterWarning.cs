using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterWarning : MonoBehaviour
{
    const float alphaSpeed = 0.5f;
    [SerializeField] GameObject hpWarningWindow;
    [SerializeField] GameObject spWarningWindow;
    [SerializeField] Slider nowHpWarning;
    [SerializeField] Slider nowSpWarning;
    float nowHpWarningValue = 0.25f;
    float nowSpWarningValue = 0.25f;
    bool hpAlphaUp = false;
    bool spAlphaUp = false;
    private void Start()
    {
        nowHpWarning.onValueChanged.AddListener(delegate { HpWarningValueChangeCheck(); });
        nowSpWarning.onValueChanged.AddListener(delegate { SpWarningValueChangeCheck(); });
    }
    private void Update()
    {
        if (nowHpWarningValue > 0)
        {
            DisplayHpWarning();
            BlinkHpWarningWindow((Player.instance.NowHp / Player.instance.MaxHp) / nowHpWarningValue);
        }
        if (nowSpWarningValue > 0)
        {
            DisplaySpWarning();
            BlinkSpWarningWindow((Player.instance.NowSp / Player.instance.MaxSp) / nowSpWarningValue);
        }
    }
    public void BlinkHpWarningWindow(float _value)
    {
        if (hpWarningWindow.activeSelf)
        {
            if (hpWarningWindow.GetComponent<Image>().color.a > 0.5f)
            {
                hpAlphaUp = false;
            }
            if (hpWarningWindow.GetComponent<Image>().color.a < 0.1f)
            {
                hpAlphaUp = true;
            }
            if (hpAlphaUp)
            {
                hpWarningWindow.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime * alphaSpeed * (2 - _value));
            }
            else
            {
                hpWarningWindow.GetComponent<Image>().color += new Color(0, 0, 0, -1 * Time.deltaTime * alphaSpeed * (2 - _value));
            }
        }
    }
    public void BlinkSpWarningWindow(float _value)
    {
        if (spWarningWindow.activeSelf)
        {
            if (spWarningWindow.GetComponent<Image>().color.a > 0.5f)
            {
                spAlphaUp = false;
            }
            if (spWarningWindow.GetComponent<Image>().color.a < 0.1f)
            {
                spAlphaUp = true;
            }
            if (spAlphaUp)
            {
                spWarningWindow.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime * alphaSpeed * (2 - _value));
            }
            else
            {
                spWarningWindow.GetComponent<Image>().color += new Color(0, 0, 0, -1 * Time.deltaTime * alphaSpeed * (2 - _value));
            }
        }
    }
    public void HpWarningValueChangeCheck()
    {
        nowHpWarningValue = nowHpWarning.value;
        nowHpWarning.GetComponentsInChildren<Text>()[1].text = ((int)(nowHpWarningValue * 100)).ToString() + "%";
    }
    public void SpWarningValueChangeCheck()
    {
        nowSpWarningValue = nowSpWarning.value;
        nowSpWarning.GetComponentsInChildren<Text>()[1].text = ((int)(nowSpWarningValue * 100)).ToString() + "%";
    }
    void DisplayHpWarning()
    {
        if (Player.instance.NowHp <= Player.instance.MaxHp * nowHpWarningValue)
        {
            hpWarningWindow.SetActive(true);
        }
        else
        {
            hpWarningWindow.SetActive(false);
        }
    }

    void DisplaySpWarning()
    {
        if (Player.instance.NowSp <= Player.instance.MaxSp * nowSpWarningValue)
        {
            spWarningWindow.SetActive(true);
        }
        else
        {
            spWarningWindow.SetActive(false);
        }
    }
}