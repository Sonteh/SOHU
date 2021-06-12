using UnityEngine;

public class SpellData : MonoBehaviour
{
    [SerializeField] public float spellDamage = 0;
    [SerializeField] public float healAmount = 0;
    [SerializeField] private float spellOutOfRange = 0;

    void Update()
    {
        Destroy(this.gameObject, spellOutOfRange);
    }
}
