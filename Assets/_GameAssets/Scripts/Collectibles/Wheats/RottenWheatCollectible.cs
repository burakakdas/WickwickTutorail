using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesignSo;

    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSo.IncreaseDecreaseMultipler, _wheatDesignSo.ResetBoostDuraction);
        Destroy(gameObject);
    }
}
