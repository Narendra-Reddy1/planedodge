using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public Transform target;
    public virtual void AutoBlast()
    {
        //show blast effect.
        gameObject.SetActive(false);
    }
}
