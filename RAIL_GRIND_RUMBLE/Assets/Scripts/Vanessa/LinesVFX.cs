using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Experimental.VFX;

public class LinesVFX : MonoBehaviour
{
    // Start is called before the first frame update
     private VisualEffect visualEffect;
    private float radius = 2;
    private float AlphaLow = 0.4f;
    private float AlphaHigh = 1;
    private Gradient gradient;
    public Gradient[] colorSwaps = new Gradient [3];
    private GameObject playerREF;

    private ThirdPersonMovement TPmovementREF;
     
     private bool canLines;
    void Start()
    {
        gradient = colorSwaps[0]; 
        Recalculate();
        canLines = false;

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
        
        //relative speed management
        if(TPmovementREF.currentSpeed > 10)
        {
            
            canLines = true;
            AlphaHigh = 1;
            AlphaLow = 0.4f;
            Recalculate();
        } else
        {
            canLines = false;
            AlphaLow = 0;
            AlphaHigh = 0;
            Recalculate();
        }
            
        //color management
        if (!TPmovementREF.isWalking)
            RecalculateGradient();
    }

    private void RecalculateGradient ()
    {
        if (playerREF.GetComponent<CollisionFollow>().isGrinding || playerREF.GetComponent<WallRun>().isWallRunning)
        {
           
             gradient = colorSwaps[2];
        }  
        else 
            gradient = colorSwaps [0];

         Recalculate();
    }

    private void Recalculate ()
    {
        visualEffect.SetFloat("Radius", radius);
        visualEffect.SetFloat("AlphaLow", AlphaLow);
        visualEffect.SetFloat("AlphaHigh", AlphaHigh);
        visualEffect.SetGradient("Color Gradient", gradient);
        
    }
}
