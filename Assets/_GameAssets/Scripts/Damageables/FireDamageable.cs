using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float _force = 10f;
    public void GiveDamage(Rigidbody playRigidbody, Transform playerVisualTransform)
    {
        HealtManager.Instance.Damage(1);
        playRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
