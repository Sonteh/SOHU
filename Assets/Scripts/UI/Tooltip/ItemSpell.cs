using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "New Spell", menuName = "Items/Spell")]
public class ItemSpell : Item
{
    [SerializeField] private ItemSpellType itemSpellType;
    [SerializeField] private string useText = "Something";
    
    public ItemSpellType ItemSpellType { get {return itemSpellType; } }
    public override string ItemColoredName
    {
        get
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(itemSpellType.TextColor);

            return $"<color=#{hexColor}>{ItemName}</color>";
        }
    }

    public override string GetItemTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(ItemSpellType.SpellTypeName).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("Buy Price: ").Append(ItemBuyPrice).Append(" Gold");

        return builder.ToString();
    }
}
