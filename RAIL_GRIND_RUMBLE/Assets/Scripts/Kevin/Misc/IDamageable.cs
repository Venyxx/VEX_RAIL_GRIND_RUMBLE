using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(float damage);
    void GainHealth(float GainHealth);
    void IsDizzy (bool isDizzy);
   

    Transform GetTransform();
}
