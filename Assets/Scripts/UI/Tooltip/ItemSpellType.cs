using UnityEngine;

[CreateAssetMenu]
public class ItemSpellType : ScriptableObject
{
    [SerializeField] private string spellTypeName; 
    [SerializeField] private Color textColor;

    public string SpellTypeName { get { return spellTypeName; } }
    public Color TextColor { get { return textColor; } }
}
