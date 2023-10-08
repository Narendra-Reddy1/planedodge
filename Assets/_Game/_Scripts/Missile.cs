using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public Transform target;
    //used to disable the graphic of the missile as I am mainting a particle blast effect for each missile.
    //It can be optimized by maintaining a pool for blast particles and re-positioning it to the missile's position when needed.
    public GameObject missileGraphic;
    public Collider2D _collider;
    public ParticleSystem blastEffect;

    public void ActivateMissile()
    {
        missileGraphic.SetActive(true);
        _collider.enabled = true;
    }
    public virtual void AutoBlast()
    {
        blastEffect.gameObject.SetActive(true);
        blastEffect?.Play();
        _collider.enabled = false;
        missileGraphic.SetActive(false);
    }
}
