using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(float Damage);
    void GainHealth(float GainHealth);


    Transform GetTransform();
}
