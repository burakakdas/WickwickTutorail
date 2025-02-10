using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _movementDecreaseSpeed;
    [SerializeField] private float _resetBoostDuraction;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_movementDecreaseSpeed, _resetBoostDuraction);
        Destroy(gameObject);
    }
}
