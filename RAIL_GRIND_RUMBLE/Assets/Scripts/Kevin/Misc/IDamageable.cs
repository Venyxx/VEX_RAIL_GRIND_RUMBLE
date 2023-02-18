using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(float damage);
    void GainHealth(float GainHealth);
   

    Transform GetTransform();
}
