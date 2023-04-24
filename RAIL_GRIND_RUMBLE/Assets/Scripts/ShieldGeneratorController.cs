using UnityEngine;

public class ShieldGeneratorController : MonoBehaviour, IDamageable
{
    [SerializeField] private Mesh destroyedMesh;
    [SerializeField] private Material destroyedMaterial1;
    [SerializeField] private Material destroyedMaterial2;
    [SerializeField] private Material destroyedMaterial3;
    [SerializeField] private float invincibilityTime = 0.5f;
    private int _startingHealth;
    private int _currentHealth;
    private bool _isDestroyed;
    private bool _invincible;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    
    void Start()
    {
        _startingHealth = 3;
        _currentHealth = _startingHealth;
        _invincible = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }
    
    public Transform GetTransform()
    {
        return transform;
    }

    public void IsDizzy(bool dizzy)
    {
        Debug.Log("Shield Generator cannot be made Dizzy");
    }

    public void GainHealth(float health)
    {
        Debug.Log("Shield Generator cannot gain health");
    }

    public void TakeDamage(float health)
    {
        if (!_invincible)
        {
            _currentHealth -= 1;
            _invincible = true;
            Invoke(nameof(InvincibilityCooldown), invincibilityTime);
        }

        switch (_currentHealth)
        {
            case 2:
                _meshRenderer.material = destroyedMaterial1;
                break;
            case 1:
                _meshRenderer.material = destroyedMaterial2;
                _meshFilter.mesh = destroyedMesh;
                break;
            default:
                _meshRenderer.material = destroyedMaterial3;
                _meshFilter.mesh = destroyedMesh;
                _isDestroyed = true;
                break;
        }
    }

    private void InvincibilityCooldown()
    {
        _invincible = false;
    }


    public bool IsDestroyed()
    {
        return _isDestroyed;
    }



}
