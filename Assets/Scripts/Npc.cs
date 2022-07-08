using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Npc", menuName = "New Npc/npc")]
public class Npc : ScriptableObject
{
    public enum NpcType
    {
        Normal, // 일반 - 말만 하는 애
        Merchant, // 상인 - 뭐 사고 파는 애
        Anvil, // 모루 - 임시
        Alchemy, // 연금술 테이블 - 임시
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