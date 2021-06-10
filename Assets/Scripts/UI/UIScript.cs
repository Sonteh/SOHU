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
    [SerializeField] public Player player;

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

    }

    private void Update() 
    {
        if (IsFireballUsed)
        {
            FireballCooldownUpdate();
        }
        
        if (IsMagicMissleUsed)
        {
            MagicMissleCooldownUpdate();
        }

        if (IsRollingMeteorUsed)
        {
            RollingMeteorCooldownUpdate();
        }

        if (IsPortableZoneUsed)
        {
            PortableZoneCooldownUpdate();
        }

        if (IsTacticalRecallUsed)
        {
            TacticalRecallCooldownUpdate();
        }
    }

    private void FireballCooldownUpdate()
    {
        if (fireballImage.fillAmount == 1f)
        {
            fireballImage.fillAmount = 0f;
        }

        fireballImage.fillAmount += 1f / fireballCooldownTime * Time.deltaTime;

        if (fireballImage.fillAmount >= 1f)
        {
            IsFireballUsed = false;
        }
    }

    private void MagicMissleCooldownUpdate()
    {
        if (magicMissleImage.fillAmount == 1f)
        {
            magicMissleImage.fillAmount = 0f;
        }

        magicMissleImage.fillAmount += 1f / magicMissleCooldownTime * Time.deltaTime;

        if (magicMissleImage.fillAmount >= 1f)
        {
            IsMagicMissleUsed = false;
        }
    }

    private void RollingMeteorCooldownUpdate()
    {
        if (rollingMeteorImage.fillAmount == 1f)
        {
            rollingMeteorImage.fillAmount = 0f;
        }

        rollingMeteorImage.fillAmount += 1f / rollingMeteorCooldownTime * Time.deltaTime;

        if (rollingMeteorImage.fillAmount >= 1f)
        {
            IsRollingMeteorUsed = false;
        }
    }

    private void PortableZoneCooldownUpdate()
    {
        if (portableZoneImage.fillAmount == 1f)
        {
            portableZoneImage.fillAmount = 0f;
        }

        portableZoneImage.fillAmount += 1f / portableZoneCooldownTime * Time.deltaTime;

        if (portableZoneImage.fillAmount >= 1f)
        {
            IsPortableZoneUsed = false;
        }
    }

    private void TacticalRecallCooldownUpdate()
    {
        if (tacticalRecallImage.fillAmount == 1f)
        {
            tacticalRecallImage.fillAmount = 0f;
        }

        tacticalRecallImage.fillAmount += 1f / tacticalRecallCooldownTime * Time.deltaTime;

        if (tacticalRecallImage.fillAmount >= 1f)
        {
            IsTacticalRecallUsed = false;
        }
    }
}