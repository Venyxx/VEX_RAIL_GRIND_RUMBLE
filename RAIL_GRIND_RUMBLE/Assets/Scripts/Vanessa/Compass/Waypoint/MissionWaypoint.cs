using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public Image img;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        img.transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
}
