using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CompassManager : MonoBehaviour
{
    public static CompassManager Instance;
    public RawImage CompassImage;
    public RectTransform CompassObjectivesParent;
    public GameObject CompassObjectivePrefab;
    private readonly List<CompassObjective> _currentObjectives = new List<CompassObjective>();
    private GameObject ariRig;
    private GameObject mainCam;

    private void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }*/

        mainCam = GameObject.Find("Main Camera");
        Debug.Log("Awaken Compass Manager");
    }

    private IEnumerator Start()
    {
        WaitForSeconds updateDelay = new WaitForSeconds(1);

        while (enabled)
        {
            //SortCompassObjectives();
            yield return updateDelay;
        }
    }

   /* private void SortCompassObjectives()
    {
        //if (PlayerController.Instance == null)
        //{ return; }

        CompassObjective[] orderedObjectives = 
            _currentObjectives.Where<CompassObjective>(o => o.WorldGameObject != null).OrderByDescending(o => 
            Vector3.Distance(ariRig.transform.position, o.WorldGameObject.position)).ToArray();

        for (int i = 0; i < orderedObjectives.Length; i++)
        {
            orderedObjectives[i].UpdateUiIndex(i);
        }
    }*/

    private void LateUpdate() => UpdateCompassHeading();

    private void UpdateCompassHeading()
    {
        //if (PlayerController.Instance == null
           // return; 

        if (SceneManager.GetActiveScene().name == "MainMenu") return;

        Vector2 compassUvPosition = Vector2.right * 
            (mainCam.transform.rotation.eulerAngles.y / 360);

        CompassImage.uvRect = new Rect(compassUvPosition, Vector2.one);
    }

    public void AddObjectiveForObject(GameObject compassObjectiveGameObject, 
        Color color, Sprite sprite) =>
            _currentObjectives.Add(Instantiate(CompassObjectivePrefab, 
            CompassObjectivesParent, false).GetComponent<CompassObjective>()
            .Configure(compassObjectiveGameObject, color, sprite));
}