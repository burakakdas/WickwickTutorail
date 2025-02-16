using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultipler, _wheatDesignSO.ResetBoostDuraction);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage,
           _playerStateUI.GetRottenBoosterWheatImage, _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite,
           _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuraction);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        Destroy(gameObject);
    }
}
