using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Experimental.VFX;

public class LinesVFX : MonoBehaviour
{
    // Start is called before the first frame update
     private VisualEffect visualEffect;
    private float radius;
    private float AlphaLow;
    private float AlphaHigh;
    private Gradient gradient;
    public Gradient[] colorSwaps = new Gradient [3];
    private GameObject playerREF;

    private ThirdPersonMovement TPmovementREF;
     
    void Start()
    {
        visualEffect.SetFloat("Radius", radius);
        visualEffect.SetFloat("AlphaLow", AlphaLow);
        visualEffect.SetFloat("AlphaHigh", AlphaHigh);
        visualEffect.SetGradient("Color Gradient", gradient);

        
        Debug.Log("I AM RUNNING START");


        playerREF = GameObject.Find("playerPrefab");
        visualEffect = gameObject.GetComponent<VisualEffect>();
        TPmovementREF = playerREF.GetComponent<ThirdPersonMovement>();

    }

    void Awake ()
    {
        playerREF = GameObject.Find("playerPrefab");
        visualEffect = gameObject.GetComponent<VisualEffect>();
        TPmovementREF = playerREF.GetComponent<ThirdPersonMovement>();
    }
    // Update is called once per frame
    void Update()
    {
       
        if (!TPmovementREF.isWalking)
            RecalculateGradient();
    }

    private void RecalculateGradient ()
    {
        if (TPmovementREF.currentSpeed == TPmovementREF.maxSkateSpeed)
            gradient = colorSwaps[2];
        else 
            gradient = colorSwaps [0];

        visualEffect.SetGradient("Color Gradient", gradient);
    }
}
