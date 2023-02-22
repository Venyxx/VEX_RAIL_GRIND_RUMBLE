using UnityEngine;
using UnityEngine.SceneManagement;

public class ShaderToggle : MonoBehaviour
{
    public Material[] regMats { get; set; }
    public Material[] toonMats { get; set; }
    private GameObject[] outlines;
    private MaterialSwitcher[] _materialSwitchers;

    private bool toonShaderOn = true;
    void Start()
    {
        regMats = Resources.LoadAll<Material>("Materials/Regular Mats");
        toonMats = Resources.LoadAll<Material>("Materials/Toon Mats");
        outlines = GameObject.FindGameObjectsWithTag("Outline");
        _materialSwitchers = FindObjectsOfType<MaterialSwitcher>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var matSwitcher in _materialSwitchers)
            {
                matSwitcher.SwapMats(toonShaderOn);
            }
            
            if (toonShaderOn)
            {
                
                foreach (var outline in outlines)
                {
                    outline.GetComponent<MeshRenderer>().enabled = false;
                }

                toonShaderOn = false;
            }
            else
            {

                foreach (var outline in outlines)
                {
                    outline.GetComponent<MeshRenderer>().enabled = true;
                }

                toonShaderOn = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }
}
