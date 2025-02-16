using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealtUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealtImages;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnHealthySprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealtTransform;

    private void Awake()
    {
        _playerHealtTransform = new RectTransform[_playerHealtImages.Length];

        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            _playerHealtTransform[i] = _playerHealtImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            if (_playerHealtImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealtImages[i], _playerHealtTransform[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealtImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealtImages[i], _playerHealtTransform[i]);
        }
    }
    
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnHealthySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
