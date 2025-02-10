using UnityEngine;

public class PlayerİnteractionController : MonoBehaviour
{
    [SerializeField] PlayerController _playerComtroller;

    private void Awake()
    {
        _playerComtroller = GetComponent<PlayerController>();
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
}
