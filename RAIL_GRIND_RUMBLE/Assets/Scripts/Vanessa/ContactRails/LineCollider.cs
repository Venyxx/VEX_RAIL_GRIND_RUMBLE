using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
   public LineRenderer Line;
   


   
   
   public void GenerateMeshCollider ()
   {
    
    MeshCollider LinesCollider = GetComponent<MeshCollider>();

    if (LinesCollider == null)
    {
        LinesCollider = gameObject.AddComponent<MeshCollider>();
        //LinesCollider.tag = "Line";
    }

    Mesh mesh = new Mesh ();
    Line.BakeMesh(mesh);
    LinesCollider.sharedMesh = mesh;
   }


  void OnTriggerEnter (Collider collision)
   {
    //Debug.Log("hit");
    
    if (collision.tag == "RailStart")
    {
        Debug.Log("move towards railstart");
    }
    else if (collision.tag == "RailEnd")
    {
        Debug.Log("Move towards railend");
    }
   }

}
