using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : PowerUp
{
    private PlayerController player;
    public float time;
    protected override void Start()
    {
        base.Start();
        
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }

    protected override void Update()
    {
        base.Update();
        
        if (powerUpState == PowerUpState.IsCollected)
        {
            if (Input.GetButtonDown("Special ability"))
            {
                //StartCoroutine(player.GetInvulnerableForSeconds(time));
                
                PowerUpHasExpired ();
            }
        }
    }
}
