using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryTab : MonoBehaviour
{
    public Image[] newspapers;
    public static bool[] hasBeenFound = new bool[9];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < newspapers.Length; i++)
        {
            if (hasBeenFound[i] == true)
            {
                newspapers[i].gameObject.SetActive(true);
            } else {
                newspapers[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewspaper(int paperToAdd)
    {
        hasBeenFound[paperToAdd] = true;
        newspapers[paperToAdd].gameObject.SetActive(true);
    }
}
