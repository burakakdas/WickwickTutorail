using UnityEngine;

public class HealtManager : MonoBehaviour
{
    [SerializeField] private int _maxHealt = 3;

    private int _currentHealt;

    private void Start()
    {
        _currentHealt = _maxHealt;
    }

    public void Damage(int damage)
    {
        if (_currentHealt > 0)
        {
            _currentHealt -= damage;
            //damage anımations

            if(_currentHealt <= 0)
            {
                //player dead
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
