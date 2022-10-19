using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(int Damage);

    Transform GetTransform();
}
