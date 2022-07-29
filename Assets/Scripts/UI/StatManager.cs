using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatManager : MonoBehaviour
{
    public GameObject statDetailWindow;
    [SerializeField] Text statStrText, statDexText, statVitText, statLukText, statPointText;
    [SerializeField] Text detailStatStrText, detailStatDexText, detailStatVitText, detailStatLukText;
    [SerializeField] Text detailHpText, detailSpText, detailHpRegenText, detailSpRegenText;
    [SerializeField] Text detailAtkSpdText, detailMoveSpeedText;
    [SerializeField] Text detailWeaponDamageText, detailToolDamageText;
    private void Update()
    {
        RefreshStatText();
        RefreshStatDetailText();
        RefreshHpSpDetailText();
        RefreshSpeedDetailText();
        RefreshDamageDetailText();
    }
    void RefreshStatText()
    {
        statStrText.text = "�ٷ� : " + (Player.instance.StatStr + Player.instance.BonusStatStr).ToString();
        statDexText.text = "��ø : " + (Player.instance.StatDex + Player.instance.BonusStatDex).ToString();
        statVitText.text = "ü�� : " + (Player.instance.StatVit + Player.instance.BonusStatVit).ToString();
        statLukText.text = "��� : " + (Player.instance.StatLuk + Player.instance.BonusStatLuk).ToString();
        statPointText.text = "���� ����Ʈ : " + Player.instance.StatPoint.ToString();
    }

    void RefreshStatDetailText()
    {
        detailStatStrText.text = "�ٷ� : " + Player.instance.StatStr.ToString() + "(+" + Player.instance.BonusStatStr.ToString() + ")";
        detailStatDexText.text = "��ø : " + Player.instance.StatDex.ToString() + "(+" + Player.instance.BonusStatDex.ToString() + ")";
        detailStatVitText.text = "ü�� : " + Player.instance.StatVit.ToString() + "(+" + Player.instance.BonusStatVit.ToString() + ")";
        detailStatLukText.text = "��� : " + Player.instance.StatLuk.ToString() + "(+" + Player.instance.BonusStatLuk.ToString() + ")";
    }

    void RefreshHpSpDetailText()
    {
        detailHpText.text = "����� : " + ((int)Player.instance.NowHp).ToString() + "/" + ((int)Player.instance.MaxHp).ToString() + "(+" /*+((int)Player.instance.BonusHp).ToString()*/+ ")";
        detailSpText.text = "���¹̳� : " + ((int)Player.instance.NowSp).ToString() + "/" + ((int)Player.instance.MaxSp).ToString() + "(+" /*+((int)Player.instance.BonusSp).ToString()*/+ ")";
        detailHpRegenText.text = "����� ȸ���� : " + ((int)Player.instance.HpRegen).ToString() + "(+" /*+((int)Player.instance.BonusHp).ToString()*/+ ")";
        detailSpRegenText.text = "���¹̳� ȸ���� : " + ((int)Player.instance.SpRegen).ToString() + "(+" /*+((int)Player.instance.BonusHp).ToString()*/+ ")";
    }

    void RefreshSpeedDetailText()
    {
        detailAtkSpdText.text = "���� �ӵ� : " + (Mathf.Round(Player.instance.AtkSpd * 100) / 100f).ToString();
        detailMoveSpeedText.text = "�̵� �ӵ� : " + (Mathf.Round(Player.instance.MvSpd * 100) / 100f).ToString();
    }

    void RefreshDamageDetailText()
    {
        detailWeaponDamageText.text = "���� ���ݷ� : " + Player.instance.WeaponDamage.ToString();
        detailToolDamageText.text = "���� ���ݷ� : " + Player.instance.ToolDamage.ToString();
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