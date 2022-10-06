using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector]
    public Pathing pathing;

    public void CreatePath()
    {
        pathing = new Pathing(transform.position);
    }
}
