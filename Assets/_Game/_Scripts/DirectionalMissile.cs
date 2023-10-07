using UnityEngine;



public class DirectionalMissile : Missile
{

    private Vector3 direction;
    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag(Konstants.PLAYER_TAG).transform;
        Invoke(nameof(AutoBlast), lifetime);

        direction = transform.position - target.position;
    }
    private void Update()
    {

        transform.position -= direction.normalized * speed * Time.deltaTime;
    }

}
