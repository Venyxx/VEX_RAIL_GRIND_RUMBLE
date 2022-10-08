using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pathing
{
   [SerializeField, HideInInspector]
   List<Vector3> points;
   
    public Pathing (Vector3 center)
    {
        points = new List <Vector3>
        {
            center+Vector3.left,
            center+(Vector3.left+Vector3.up)*.5f,
            center + (Vector3.right+Vector3.down)*.5f,
            center + Vector3.right
        };
    }

    public Vector3 this[int i]
    { get {return points[i];}}

    public int NumPoints 
    { get {return points.Count;}}


    public int NumSegments 
    {
        get 
        {
            return (points.Count -4) / 3 + 1;
        }
    }
    public void AddSegment (Vector3 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * .5f);
        points.Add(anchorPos);
    }

    public Vector3 [] GetPointsInSegment (int i)
    {
        return new Vector3[] {points[i*3], points[i*3+1], points[i*3+2], points[i*3+3]};

    }

    public void MovePoint (int i, Vector3 pos)
    {
        points[i] = pos;
    }
}
