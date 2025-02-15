using UnityEngine;

public  interface IDamageable
{
    void GiveDamage(Rigidbody playRigidbody, Transform playerVisualTransform);
}
