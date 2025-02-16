using UnityEngine;

public class PlayerİnteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTransform;
    private PlayerController _playerComtroller;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerComtroller = GetComponent<PlayerController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerComtroller);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform);
            CameraShake.Instance.ShakeCamera(1f, 0.5f);
        }
    }
}
