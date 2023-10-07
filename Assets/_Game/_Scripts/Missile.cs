using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public Transform target;
    public GameObject missileGraphic;
    public ParticleSystem blastEffect;
    public virtual void AutoBlast()
    {
        blastEffect.gameObject.SetActive(true);
        blastEffect?.Play();
        missileGraphic.SetActive(false);
    }
}
