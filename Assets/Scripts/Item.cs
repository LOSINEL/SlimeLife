using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon, // ����
        Tool, // ����
        Consumable, // �Ҹ�ǰ
        Ingredient, // ���
        Shoes, // �Ź�
    }

    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public string itemInfo;
    public int toolType = 0;
    public int damage = 0;
    public Tool.ToolType RequestToolItemType()
    {
        switch(itemImage.name.Split('_')[1])
        {
            case "Axe":
                return Tool.ToolType.Axe;
            case "PickAxe":
                return Tool.ToolType.PickAxe;
            case "Scythe":
                return Tool.ToolType.Scythe;
            case "Shovel":
                return Tool.ToolType.Shovel;
        }
        return Tool.ToolType.Hand;
    }
}