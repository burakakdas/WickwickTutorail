using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesignSO;

    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecreaseMultipler, _wheatDesignSO.ResetBoostDuraction);
        Destroy(gameObject);
    }
}
