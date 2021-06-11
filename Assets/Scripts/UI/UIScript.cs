using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class UIScript : NetworkBehaviour
{
    public Health health;
    public Image imageHealthBar;
    public float playerHealth;
    [SerializeField] public TMP_Text playerGold;
    [SerializeField] public Player player;
    [SerializeField] public NetworkPlayer networkPlayer;

    #region Spells Reference

    [Header("Fireball")]
    [SerializeField] public Fireball fireball;
    [SerializeField] public Image fireballImage;
    public float fireballCooldownTime;
    public bool IsFireballUsed;

    [Header("Magic Missle")]
    [SerializeField] public MagicMissle magicMissle;
    [SerializeField] public Image magicMissleImage;
    public float magicMissleCooldownTime;
    public bool IsMagicMissleUsed;

    [Header("Rolling Meteor")]
    [SerializeField] public RollingMeteor rollingMeteor;
    [SerializeField] public Image rollingMeteorImage;
    public float rollingMeteorCooldownTime;
    public bool IsRollingMeteorUsed;

    [Header("Portable Zone")]
    [SerializeField] public PortableZone portableZone;
    [SerializeField] public Image portableZoneImage;
    public float portableZoneCooldownTime;
    public bool IsPortableZoneUsed;

    [Header("Tactical Recall")]
    [SerializeField] public TacticalRecall tacticalRecall;
    [SerializeField] public Image tacticalRecallImage;
    public float tacticalRecallCooldownTime;
    public bool IsTacticalRecallUsed;

    #endregion

    public void PlayerHealth(float _value)
    {
         imageHealthBar.fillAmount = _value / 100;
    }

    private void Start() 
    {
        gameObject.SetActive(true);

        networkPlayer.uiScript = this;
    }

    private void Update() 
    {
        if (IsFireballUsed)
        {
            IsFireballUsed = SpellCooldownVisualization(fireballImage, fireballCooldownTime, IsFireballUsed);
        }
        
        if (IsMagicMissleUsed)
        {
            IsMagicMissleUsed = SpellCooldownVisualization(magicMissleImage, magicMissleCooldownTime, IsMagicMissleUsed);
        }

        if (IsRollingMeteorUsed)
        {
            IsRollingMeteorUsed = SpellCooldownVisualization(rollingMeteorImage, rollingMeteorCooldownTime, IsRollingMeteorUsed);
        }

        if (IsPortableZoneUsed)
        {
            IsPortableZoneUsed = SpellCooldownVisualization(portableZoneImage, portableZoneCooldownTime, IsPortableZoneUsed);
        }

        if (IsTacticalRecallUsed)
        {
            IsTacticalRecallUsed = SpellCooldownVisualization(tacticalRecallImage, tacticalRecallCooldownTime, IsTacticalRecallUsed);
        }
    }

    private bool SpellCooldownVisualization(Image spellImage, float spellCooldown, bool IsSpellUsed)
    {
        if (spellImage.fillAmount == 1f)
        {
            spellImage.fillAmount = 0f;
        }

        spellImage.fillAmount += 1f / spellCooldown * Time.deltaTime;

        if (spellImage.fillAmount >= 1f)
        {
            IsSpellUsed = false;
            return IsSpellUsed;
        }
        
        return IsSpellUsed;
    }
}