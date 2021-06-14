using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemBuyPrice;
    [SerializeField] private int itemUpgradePrice;

    public string ItemName { get { return itemName; } }
    public abstract string ItemColoredName { get; }
    public string ItemDescription { get { return itemDescription; } }
    public int ItemBuyPrice { get { return itemBuyPrice; } }
    public int ItemUpgradePrice { get { return itemUpgradePrice; } }

    public abstract string GetItemTooltipInfoText();
}
