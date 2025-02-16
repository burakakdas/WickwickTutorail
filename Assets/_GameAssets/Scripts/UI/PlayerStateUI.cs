using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Playables;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;
    [SerializeField] private PlayableDirector _playableDirector;

    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveTransform;
    [SerializeField] private Sprite _playerWalkingPassiveTransform;
    [SerializeField] private Sprite _playerSlidingActiveTransform;
    [SerializeField] private Sprite _playerSlidingPassiveTransform;

    [Header("Settings")]
    [SerializeField] private float _moveDuraction;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;

    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => _holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => _rottenBoosterWheatImage;

    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    private void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }

    private void Start()
    {
        _playerController.OnlPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        _playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        SetStateUserInterfaces(_playerWalkingActiveTransform, _playerSlidingPassiveTransform, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerstate)
    {
        switch (playerstate)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(_playerWalkingActiveTransform, _playerSlidingPassiveTransform, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetStateUserInterfaces(_playerWalkingPassiveTransform, _playerSlidingActiveTransform, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite,
        RectTransform activeTransformm, RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransformm.DOAnchorPosX(-25f, _moveDuraction).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuraction).SetEase(_moveEase);
    }
    
    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage,
        Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite,
        Sprite passiveWheatSprite, float duration)

    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuraction).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuraction).SetEase(_moveEase);
    }

    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
        Image wheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite,
        Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage, activeSprite,
        passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }

    internal void PlayBoosterUIAnimations(RectTransform playerBoosterTransform, Microsoft.Unity.VisualStudio.Editor.Image playerBoosterImage, Image getGoldBoosterWheatImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float resetBoostDuraction)
    {
        throw new NotImplementedException();
    }
}
