using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _movementIncreaseSpeed;
    [SerializeField] private float _resetBoostDuraction;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_movementIncreaseSpeed, _resetBoostDuraction);
        Destroy(gameObject);
    }
}
