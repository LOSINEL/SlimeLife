using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatManager : MonoBehaviour
{
    public GameObject statDetailWindow;
    [SerializeField] Text statStrText;
    [SerializeField] Text statDexText;
    [SerializeField] Text statVitText;
    [SerializeField] Text statLukText;
    [SerializeField] Text statPointText;
    private void Update()
    {
        RefreshStatText();
    }
    void RefreshStatText()
    {
        statStrText.text = "STR : " + (Player.instance.StatStr + Player.instance.BonusStatStr).ToString();
        statDexText.text = "DEX : " + (Player.instance.StatDex + Player.instance.BonusStatDex).ToString();
        statVitText.text = "  VIT : " + (Player.instance.StatVit + Player.instance.BonusStatVit).ToString();
        statLukText.text = "LUK : " + (Player.instance.StatLuk + Player.instance.BOnusStatLuk).ToString();
        statPointText.text = "Stat Point : " + Player.instance.StatPoint.ToString();
    }
    public void ResetStat()
    {
        Player.instance.AddStat(Player.StatType.STR, -1 * Player.instance.StatStr);
        Player.instance.AddStat(Player.StatType.DEX, -1 * Player.instance.StatDex);
        Player.instance.AddStat(Player.StatType.VIT, -1 * Player.instance.StatVit);
        Player.instance.AddStat(Player.StatType.LUK, -1 * Player.instance.StatLuk);
        Player.instance.SetStatPoint(Player.instance.StatPointAll);
    }
    public void TryStatDetailWindow()
    {
        statDetailWindow.SetActive(!statDetailWindow.activeSelf);
    }
    public void AddStat1()
    {
        switch (EventSystem.current.currentSelectedGameObject.name.Split('_')[0])
        {
            case "Str":
                Player.instance.AddStat(Player.StatType.STR, 1);
                break;
            case "Dex":
                Player.instance.AddStat(Player.StatType.DEX, 1);
                break;
            case "Vit":
                Player.instance.AddStat(Player.StatType.VIT, 1);
                break;
            case "Luk":
                Player.instance.AddStat(Player.StatType.LUK, 1);
                break;
        }
    }
    public void AddStat10()
    {
        switch (EventSystem.current.currentSelectedGameObject.name.Split('_')[0])
        {
            case "Str":
                Player.instance.AddStat(Player.StatType.STR, 10);
                break;
            case "Dex":
                Player.instance.AddStat(Player.StatType.DEX, 10);
                break;
            case "Vit":
                Player.instance.AddStat(Player.StatType.VIT, 10);
                break;
            case "Luk":
                Player.instance.AddStat(Player.StatType.LUK, 10);
                break;
        }
    }
    public void AddStatAll()
    {
        switch (EventSystem.current.currentSelectedGameObject.name.Split('_')[0])
        {
            case "Str":
                Player.instance.AddStat(Player.StatType.STR, Player.instance.StatPoint);
                break;
            case "Dex":
                Player.instance.AddStat(Player.StatType.DEX, Player.instance.StatPoint);
                break;
            case "Vit":
                Player.instance.AddStat(Player.StatType.VIT, Player.instance.StatPoint);
                break;
            case "Luk":
                Player.instance.AddStat(Player.StatType.LUK, Player.instance.StatPoint);
                break;
        }
    }
}