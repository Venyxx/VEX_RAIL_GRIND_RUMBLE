using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    private ShaderToggle _shaderToggle;
    private MeshRenderer _meshRenderer;

    private Material _currentMaterial; 

    void Start()
    {
        _shaderToggle = FindObjectOfType<ShaderToggle>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SwapMats(bool toonOn)
    {
        _currentMaterial = _meshRenderer.material;
        //Debug.Log($"SwapMats called on {gameObject.name}. ToonOn: {toonOn}");

        if (toonOn)
        {
            foreach (var mat in _shaderToggle.regMats)
            {
                //Debug.Log($"Current Mat Name: {_currentMaterial.name}\n, Checking against: {mat.name}");
                if (_currentMaterial.name.Replace(" (Instance)", "") == mat.name)
                {
                    //Debug.Log("Found a match. Breaking...");
                    _meshRenderer.material = mat;
                    break;
                }
            }
        }
        else
        {
            foreach (var mat in _shaderToggle.toonMats)
            {
                //Debug.Log($"Current Mat Name{_currentMaterial.name}\n, Checking against {mat.name}");
                if (_currentMaterial.name.Replace(" (Instance)", "") == mat.name)
                {
                    //Debug.Log("Found a match. Breaking...");
                    _meshRenderer.material = mat;
                    break;
                }
            }
        }
    }
}
