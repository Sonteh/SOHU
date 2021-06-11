using UnityEngine;

public class SpellData : MonoBehaviour
{
    [SerializeField] public float spellDamage;
    [SerializeField] public float healAmount;
    [SerializeField] private float spellOutOfRange;

    void Update()
    {
        Destroy(this.gameObject, spellOutOfRange);
    }
}
