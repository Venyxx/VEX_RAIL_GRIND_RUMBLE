using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSliceProgressionManager : MonoBehaviour
{
    [SerializeField] private GameObject section1Barricades;
    [SerializeField] private GameObject section2Barricade;
    [SerializeField] private GameObject section3Barricade;
    [SerializeField] private GameObject section4Barricade;
    private GameObject[] section1Enemies;
    private GameObject[] section2Enemies;
    private GameObject[] section3Enemies;
    private GameObject[] section4Enemies;
    
    
    // Start is called before the first frame update
    void Start()
    {
        section1Enemies = GameObject.FindGameObjectsWithTag("Section1Enemy");
        section2Enemies = GameObject.FindGameObjectsWithTag("Section2Enemy");
        section3Enemies = GameObject.FindGameObjectsWithTag("Section3Enemy");
        section4Enemies = GameObject.FindGameObjectsWithTag("Section4Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        SectionCheck(section1Enemies, section1Barricades);
        
        if(section1Barricades == null)
            SectionCheck(section2Enemies, section2Barricade);
        
        if(section2Barricade == null)
            SectionCheck(section3Enemies, section3Barricade);
        
        if(section3Barricade == null)
            SectionCheck(section4Enemies, section4Barricade);
    }

    void SectionCheck(GameObject[] sectionEnemies, GameObject sectionBarricade)
    {
        foreach (var enemy in sectionEnemies)
        {
            if (enemy != null) return;
        }
        Destroy(sectionBarricade);
    }
}
