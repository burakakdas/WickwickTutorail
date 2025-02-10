using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesingSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultipler;
    [SerializeField] private float _resetBoostDuraction;

    public float IncreaseDecreaseMultipler => _increaseDecreaseMultipler;
    public float ResetBoostDuraction => _resetBoostDuraction;
}
