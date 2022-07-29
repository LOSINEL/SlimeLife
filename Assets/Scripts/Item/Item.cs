using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType_
    {
        Weapon, // 무기
        Tool, // 도구
        Consumable, // 소모품
        Ingredient, // 재료
        Shoes, // 신발
    }

    [SerializeField] string itemName, itemInfo;
    [SerializeField] ItemType_ itemType;
    [SerializeField] Sprite itemImage;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] int buyPrice, sellPrice, bundleSize;
    [SerializeField] bool canSell;
    [SerializeField] Tool.ToolType toolType;
    [SerializeField] int damage = 0;
    [SerializeField] float moveSpd = 0f;
    public string ItemName { get { return itemName; } }
    public string ItemInfo { get { return itemInfo; } }
    public ItemType_ ItemType { get { return itemType; } }
    public Sprite ItemImage { get { return itemImage; } }
    public GameObject ItemPrefab { get { return itemPrefab; } }
    public int BuyPrice { get { return buyPrice; } }
    public int SellPrice { get { return sellPrice; } }
    public int BundleSize { get { return bundleSize; } }
    public bool CanSell { get { return canSell; } }
    public Tool.ToolType ToolType { get { return toolType; } }
    public int Damage { get { return damage; } }
    public float MoveSpd { get { return moveSpd; } }
}