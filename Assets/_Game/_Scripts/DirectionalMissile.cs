using UnityEngine;



public class DirectionalMissile : Missile
{
    private Vector3 direction;
    private void OnEnable()
    {
        missileGraphic.SetActive(true);
        target = GameObject.FindGameObjectWithTag(Konstants.PLAYER_TAG).transform;
        Invoke(nameof(AutoBlast), lifetime);
        direction = transform.position - target.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

    }
    private void Update()
    {
        transform.position -= direction.normalized * speed * Time.deltaTime;
    }
   
}
