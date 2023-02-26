using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaypointSys : MonoBehaviour
{
    public RectTransform prefab;
    private GameObject player;
    private TMP_Text distanceText;

    private RectTransform waypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("WaypointsCanvas").transform;

        waypoint = Instantiate(prefab, canvas);
        waypoint.name = gameObject.name + "UIWP";
        player = GameObject.Find("AriRig");
        distanceText = waypoint.GetComponentInChildren<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        waypoint.position = screenPos;
        
        distanceText.text = Vector3.Distance(player.transform.position, transform.position).ToString("0.0") + " m";
    }


}
