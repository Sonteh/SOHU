using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CastSpell : NetworkBehaviour
{
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private float _fireballCooldown = 0.5f;
    private float _canUseFireball = -1.0f;

    [SerializeField] private GameObject _magicMissilePrefab;
    //[SerializeField] private float _fireballCooldown = 0.5f;
    //private float _canUseFireball = -1.0f;

    //private float previousInput;

    private static Controls controls;
    public static Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;
        Controls.Player.CastFireball.performed += ctx => CastFireball();
        Controls.Player.CastMagicMissile.performed += ctx => CastMagicMissile();
    }


    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    [Client]
    private void CastFireball()
    {
        if (!hasAuthority) { return;  }
        CmdCastFireball(transform.position + Vector3.forward, Quaternion.identity);
    }

    [Command]
    private void CmdCastFireball(Vector3 spellDirection, Quaternion quat)
    {
        //_canUseFireball = _fireballCooldown + Time.time;
        GameObject fireball = Instantiate(_fireballPrefab, spellDirection, quat);
        NetworkServer.Spawn(fireball, connectionToClient);
    
    }

    [Client]
    private void CastMagicMissile()
    {
        if (!hasAuthority) { return; }
        CmdCastMagicMissile(transform.position + Vector3.forward, Quaternion.identity);
    }

    [Command]
    private void CmdCastMagicMissile(Vector3 spellDirection, Quaternion quat)
    {
        //_canUseFireball = _fireballCooldown + Time.time;
        GameObject magicMissile = Instantiate(_magicMissilePrefab, spellDirection, quat);
        NetworkServer.Spawn(magicMissile, connectionToClient);
    }
}


