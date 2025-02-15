using System;
using UnityEngine;

public class HealtManager : MonoBehaviour
{
    public static HealtManager Instance { get; private set;}

    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealtUI _playerHealtUI;

    [Header("Settings")]
    [SerializeField] private int _maxHealt = 3;

    private int _currentHealt;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentHealt = _maxHealt;
    }

    public void Damage(int damage)
    {
        if (_currentHealt > 0)
        {
            _currentHealt -= damage;
            _playerHealtUI.AnimateDamage();

            if(_currentHealt <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public void Heal(int heal)
    {
        if(_currentHealt < _maxHealt)
        {
            _currentHealt = Mathf.Min(_currentHealt + heal, _maxHealt);
        }
    }
}
