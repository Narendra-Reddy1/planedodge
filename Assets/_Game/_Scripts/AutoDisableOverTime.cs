using UnityEngine;

public class AutoDisableOverTime : MonoBehaviour
{
    [SerializeField] private float _time = 5f;
    private void OnEnable()
    {
        Invoke(nameof(_DisableObj), _time);
    }
    private void _DisableObj()
    {
        gameObject.SetActive(false);
    }
}
