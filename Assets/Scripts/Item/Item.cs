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
    public int buyPrice;
    public int sellPrice;
    public int bundleSize;
    [SerializeField] Tool.ToolType toolType;
    [SerializeField] int damage = 0;
    [SerializeField] float moveSpd = 0f;
    public Tool.ToolType ToolType { get { return toolType; } }
    public int Damage { get { return damage; } }
    public float MoveSpd { get { return moveSpd; } }
}