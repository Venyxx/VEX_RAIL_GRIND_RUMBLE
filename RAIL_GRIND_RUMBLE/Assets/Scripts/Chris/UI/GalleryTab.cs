using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryTab : MonoBehaviour
{
    public GameObject[] newspapers;
    public static bool[] hasBeenFound = new bool[9];
    [SerializeField] string[] newspaperTitles;
    [SerializeField] string[] newspaperText;
    [SerializeField] string[] spanishTitles;
    [SerializeField] string[] spanishText;
    [SerializeField] GameObject readPaperScreen;
    public bool paperIsOpen;
    // Start is called before the first frame update
    void Start()
    {
        readPaperScreen.SetActive(false);
        paperIsOpen = false;

        for (int i = 0; i < newspapers.Length; i++)
        {
            if (hasBeenFound[i] == true)
            {
                newspapers[i].gameObject.SetActive(true);
                newspapers[i].transform.Find("Title").GetComponent<TextMeshProUGUI>().text = newspaperTitles[i];
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
        newspapers[paperToAdd].transform.Find("Title").GetComponent<TextMeshProUGUI>().text = newspaperTitles[paperToAdd];
    }

    public void OpenPaper(int paperToOpen)
    {
        paperIsOpen = true;
        paperToOpen = paperToOpen - 1;
        readPaperScreen.SetActive(true);
        readPaperScreen.transform.Find("PaperTitle").GetComponent<TextMeshProUGUI>().text = newspaperTitles[paperToOpen];
        readPaperScreen.transform.Find("PaperText").GetComponent<TextMeshProUGUI>().text = newspaperText[paperToOpen];
    }

    public void ClosePaper()
    {
        readPaperScreen.SetActive(false);
        paperIsOpen = false;
    }
}
