using System.IO.Compression;
using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesingSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultipler;
    [SerializeField] private float _resetBoostDuraction;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _passiveWheatSprite;


    public float IncreaseDecreaseMultipler => _increaseDecreaseMultipler;
    public float ResetBoostDuraction => _resetBoostDuraction;
    public Sprite ActiveSprite => _activeSprite;
    public Sprite PassiveSprite => _passiveSprite;
    public Sprite ActiveWheatSprite => _activeWheatSprite;
    public Sprite PassiveWheatSprite => _passiveWheatSprite;
}
