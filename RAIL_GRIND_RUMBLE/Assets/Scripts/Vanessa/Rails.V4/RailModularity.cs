using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailModularity : MonoBehaviour
{
    public GameObject PositiveRunner;
    public GameObject NegativeRunner;
    private GameObject currentRail;

    //these values get passed in from pointmodularity



    // Start is called before the first frame update
    void Start()
    {
        currentRail = gameObject;
        PositiveRunner = transform.Find("PositiveRunner").gameObject;
        NegativeRunner = transform.Find("NegativeRunner").gameObject;
    }


    // Update is called once per frame
    void Update()
    {
   
    }



}
