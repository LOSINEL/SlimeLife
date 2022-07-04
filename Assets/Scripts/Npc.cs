using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Npc", menuName = "New Npc/npc")]
public class Npc : ScriptableObject
{
    public enum NpcType
    {
        Normal, // �Ϲ� - ���� �ϴ� ��
        Merchant, // ���� - �� ��� �Ĵ� ��
        Anvil, // ��� - �ӽ�
        Alchemy, // ���ݼ� ���̺� - �ӽ�
    }

    public string npcName;
    public NpcType npcType;
    public Sprite npcImage;
    public GameObject npcPrefab;
    public string npcInfo;
    public NpcScript[] npcScript;
}

[System.Serializable]
public class NpcScript
{
    public string[] script;
}