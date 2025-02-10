using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _forceIncrease;
    [SerializeField] private float _resetBoostDuraction;

    public void Collect()
    {
        _playerController.SetJumpForce(_forceIncrease, _resetBoostDuraction);
        Destroy(gameObject);
    }
}
