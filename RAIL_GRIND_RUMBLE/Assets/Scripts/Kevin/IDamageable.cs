using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(float Damage);

    Transform GetTransform();
}
