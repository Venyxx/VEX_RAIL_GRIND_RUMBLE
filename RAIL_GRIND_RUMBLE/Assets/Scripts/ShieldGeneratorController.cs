using UnityEngine;
using Random = System.Random;

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
    private AudioClip[] hitSounds;
    private AudioSource audioSource;
    public GameObject VFX;

    void Start()
    {
        _startingHealth = 3;
        _currentHealth = _startingHealth;
        _invincible = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        hitSounds = Resources.LoadAll<AudioClip>("Sounds/MetalDamageSounds");
        audioSource = GetComponent<AudioSource>();
        if (VFX != null)
        {
            VFX.SetActive(false);
        }
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
      
        if (!_invincible && _currentHealth > 0)
        {
            _currentHealth -= 1;
            _invincible = true;
            Invoke(nameof(InvincibilityCooldown), invincibilityTime);
            Random rand = new Random();
            audioSource.PlayOneShot(hitSounds[rand.Next(0, hitSounds.Length)]);
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

    private void Update()
    {
        if(_invincible)
        {
            VFX.SetActive(true);
        }

        if (!_invincible)
        {
            VFX.SetActive(false);
        }
    }

}
