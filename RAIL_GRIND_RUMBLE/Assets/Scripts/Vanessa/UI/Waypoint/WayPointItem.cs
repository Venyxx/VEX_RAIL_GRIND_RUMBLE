
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]


//item data
public struct WayPointItem
{
    public Sprite icon;
    public float heightOffset;
    [HideInInspector] public Image image;
    [HideInInspector] public Text message;
    [HideInInspector] public GameObject effect;
    [HideInInspector] public GameObject waypointUI;
    [HideInInspector] public GameObject target;
    [HideInInspector] public Transform transform;
}
