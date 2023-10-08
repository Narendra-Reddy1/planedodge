using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Konstants.MISSILE_TAG))
        {
            AudioManager.instance.PlaySFX(AudioID.MissileBlastSFX);
            collision.GetComponent<Missile>().AutoBlast();
        }
    }

}
