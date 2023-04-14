using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    private string sceneName = ariHouse; //ari house by default
    [SerializeField] private LoadLocation loadLocation = LoadLocation.AriRoom; //ari room by default

    public static Vector3 locationVector { get; private set; } = ariRoomVector; //ari room by default

    public const string ariHouse = "Ari's House";
    public const string outskirts = "Outskirts";
    public const string innerRing = "InnerRingLevel";
    public const string servosHQ = "Servos HQ";
    
    //ari house locations
    public static readonly Vector3 ariRoomVector = new Vector3(48.4770012f, 37.3800011f, 955.93103f);
    public static readonly Vector3 ariDoorInsideVector = new Vector3(19.8299999f, 37.3800011f, 960.419983f);

    //outskirts locations
    public static readonly Vector3 ariDoorOutsideVector = new Vector3(46.1100006f,37.2099991f,956.700012f);
    public static readonly Vector3 outskirtsBusStop1Vector = new Vector3(4.78999996f, 37.2099991f, 969.090027f);
    
    //inner ring locations
    public static readonly Vector3 innerRingBusStop1Vector = new Vector3(-226.610001f, -6.48000002f, 755.119995f);
    public static readonly Vector3 innerRingDefaultSpawnVector = new Vector3(-8.75f, -6.48000002f, 896.5f);


    public static readonly Vector3 servosHQDefaultSpawnVector = new Vector3(124.440002f,3.63000011f,29.5100002f);
    private void Awake()
    {
        if (!ProgressionManager.Get().firstLoad) return;
        
        //Debug.Log("Spawning Ari at default location");
        switch (SceneManager.GetActiveScene().name)
        {
            case ariHouse:
                locationVector = ariRoomVector;
                //Debug.Log("AriRoom");
                break;
            case outskirts:
                locationVector = ariDoorOutsideVector;
                Debug.Log("Outskirts");
                break;
            case innerRing:
                locationVector = innerRingDefaultSpawnVector;
                Debug.Log("Inner Ring");
                break;
            case servosHQ:
                locationVector = servosHQDefaultSpawnVector;
                Debug.Log("Servos HQ");
                break;
        }

        ProgressionManager.Get().firstLoad = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject") || other.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        switch (loadLocation)
        {
            case LoadLocation.AriDoorInside:
                sceneName = ariHouse;
                locationVector = ariDoorInsideVector;
                break;
            case LoadLocation.AriDoorOutside:
                sceneName = outskirts;
                locationVector = ariDoorOutsideVector;
                break;
            case LoadLocation.OutskirtsBusStop1:
                sceneName = outskirts;
                locationVector = outskirtsBusStop1Vector;
                break;
            case LoadLocation.InnerRingDefault:
                sceneName = innerRing;
                locationVector = innerRingDefaultSpawnVector;
                break;
            case LoadLocation.InnerRingBusStop1:
                sceneName = innerRing;
                locationVector = innerRingBusStop1Vector;
                break;
            case LoadLocation.ServosLotEntrance:
                sceneName = servosHQ;
                //MISSING VECTOR
                locationVector = servosHQDefaultSpawnVector;
                break;
            default:
                //load ari's room in default case
                sceneName = ariHouse;
                locationVector = ariRoomVector;
                break;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(LoadLocation location)
    {
        this.loadLocation = loadLocation;
        LoadScene();
    }

    private void OnDisable()
    {
        //Debug.LogError("LOADNEWSCENE DISABLED");
    }
}


[Serializable]
public enum LoadLocation
{
    AriRoom,
    AriDoorInside,
    AriDoorOutside, 
    OutskirtsBusStop1,
    InnerRingDefault,
    InnerRingBusStop1,
    ServosLotEntrance
}
