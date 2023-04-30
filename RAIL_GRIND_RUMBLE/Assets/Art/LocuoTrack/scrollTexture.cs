using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollTexture : MonoBehaviour
{

    [SerializeField] private float ScrollX = 0.5f;
    [SerializeField] private float ScrollY = 0.5f;
    [SerializeField] private Material digitalMaterial;
    Color colour; 

    // Start is called before the first frame update
    void Start()
    {
        Color colour = digitalMaterial.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (OffsetX, OffsetY);

    }

    private void OnTriggerEnter(Collider Player)
    {
        //digitalMaterial.SetVector("_EmissionColor", new Vector4(0.8196f,0.783f,0) * -4.0f);
        colour *= 4.0f;
        digitalMaterial.SetColor("_EmissionColor", colour);
    }

    private void OnTriggerExit(Collider Player)
    {
        digitalMaterial.SetVector("_EmissionColor", new Vector4(0.8196f,0.783f,0) * 6.0f);
    }
}
