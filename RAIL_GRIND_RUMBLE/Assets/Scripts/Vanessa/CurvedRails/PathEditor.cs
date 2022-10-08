using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
   PathCreator creator;
   Pathing pathing;

   
   void OnSceneGUI ()
   {
    Input();
    Draw();
   }

    void Input ()

    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add segment");
            pathing.AddSegment(mousePos);
        }
        }
    
   void Draw ()
   {
    
    for (int i = 0; i < pathing.NumSegments; i++)
    {
        Vector3[] points = pathing.GetPointsInSegment(i);
        Handles.color = Color.black;
        Handles.DrawLine(points[1], points[0]);
        Handles.DrawLine(points[2], points[3]);
        Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green,null,2);
    }


    Handles.color = Color.red;
    for (int i = 0; i < pathing.NumPoints; i ++)
    {
        Vector3 newPos = Handles.FreeMoveHandle(pathing[i], Quaternion.identity, .1f, Vector3.zero, Handles.CylinderHandleCap);
        if (pathing[i] != newPos)
        {
            Undo.RecordObject(creator, "Move point");
            pathing.MovePoint(i, newPos);
        }

    }
   }

   void OnEnable()
   {
    creator = (PathCreator)target;
    if (creator.pathing == null)
    {
        creator.CreatePath();
    }
    pathing = creator.pathing;
   }
}
