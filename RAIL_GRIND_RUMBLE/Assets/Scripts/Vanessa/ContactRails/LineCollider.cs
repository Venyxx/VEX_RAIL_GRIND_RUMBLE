using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    public GameObject start;

    public GameObject end;

    private Vector3 startPos;

    private Vector3 endPos;

    public LineRenderer line;

    private void addColliderToLine()
    {
        startPos = (start.x, start.y, start.z);
        endPos = (end.x, end.y, end.z);

        BoxCollider col =
            new GameObject("Collider").AddComponent<BoxCollider>();
        col.transform.parent = line.transform; // Collider is added as child object of line
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint; // setting position of collider object

        // Following lines calculate the angle between startPos and endPos
        float angle =
            (
            Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x)
            );
        if (
            (startPos.y < endPos.y && startPos.x > endPos.x) ||
            (endPos.y < startPos.y && endPos.x > startPos.x)
        )
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);
    }
}
