using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(float Damage);
    void GainHealth(float GainHealth);
    void IsDizzy (bool isDizzy);
   

    Transform GetTransform();
}
