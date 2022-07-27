using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon, // 무기
        Tool, // 도구
        Consumable, // 소모품
        Ingredient, // 재료
        Shoes, // 신발
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