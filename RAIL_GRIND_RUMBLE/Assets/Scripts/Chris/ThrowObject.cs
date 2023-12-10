using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEngine.Experimental.Rendering;

public class ThrowObject : MonoBehaviour
{
    //holding and throwing anim stuff - Raul ////////////////////////////////////////////////////////////////////////////////////
    
    [SerializeField] private RigBuilder ik;
    //private GameObject leftArmGrappleREF;
    [SerializeField] private GameObject rootRigREF;
    [SerializeField] GameObject playerWeapon;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public bool isHoldingObject;
    string objectHolding;
    [SerializeField] GameObject heldTrashcanFakeREF;
    [SerializeField] GameObject heldTrashcanThrowREF;
    [SerializeField] GameObject heldDroneFakeREF;
    [SerializeField] GameObject heldDroneThrowREF;
    [SerializeField] Transform throwPoint;
    Transform orientation;
    GrappleHook grappleHookScript;
    private bool _targeting = false;

    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    private ThirdPersonMovement thirdPersonMovement;
    private GrappleDetection grappleDetection;


    // Start is called before the first frame update
    void Start()
    {
       
        isHoldingObject = false;
        grappleHookScript = gameObject.GetComponent<GrappleHook>();
        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        grappleDetection = gameObject.transform.Find("GrappleDetector").GetComponent<GrappleDetection>();
        objectHolding = "";

        //Arm Rig
        rootRigREF = GameObject.Find("mixamorig:Hips");
        //leftArmGrappleREF = GameObject.Find("leftArmHold");
        //ik = rootRigREF.GetComponent<RigBuilder>();
        ik.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0) && isHoldingObject == true) 
        {
            ThrowObjectAction();
        }*/

        if(isHoldingObject == false)
        {
            ik.enabled = false;
        }


        
    }

    public void SpawnHeldObject(string obj)
    {
        //Left Arm Rig
        ik.enabled = true;
        playerWeapon.SetActive(false);
        Debug.Log("spawnedheldObject");


        isHoldingObject = true;
        objectHolding = obj;

        if (obj == "Trashcan")
        {
            heldTrashcanFakeREF.SetActive(true);
        } else if (obj == "Drone")
        {
            heldDroneFakeREF.SetActive(true);
        }
        
    }

    void ThrowObjectAction(GameObject target)
    {
        //arm rig
        ik.enabled = false;
        playerWeapon.SetActive(true);

        isHoldingObject = false;
        heldTrashcanFakeREF.SetActive(false);
        heldDroneFakeREF.SetActive(false);

        GameObject thrownObject = null;

        if (objectHolding == "Trashcan")
        {
            thrownObject = Instantiate(heldTrashcanThrowREF, throwPoint.transform.position, heldTrashcanThrowREF.transform.rotation);
        } else if (objectHolding == "Drone")
        {
            thrownObject = Instantiate(heldDroneThrowREF, throwPoint.transform.position, heldDroneThrowREF.transform.rotation);
        }
        
        objectHolding = "";

        if (target != null)
        {
            Debug.Log("Target is not null");
            PlayerThrownObject thrownObjectComponentRef = thrownObject.gameObject.GetComponent<PlayerThrownObject>();
            thrownObjectComponentRef.SetCurrentPlayerAim(target);
            thrownObjectComponentRef.isTargeting = true;
            
            return; 
        }
        Debug.Log("Target is null");

        thrownObject.gameObject.GetComponent<PlayerThrownObject>().isTargeting = false;

        Vector3 forceToAdd = orientation.transform.forward * throwForce + transform.up * throwUpwardForce;
        thrownObject.gameObject.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
      
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if ((!(context.started && isHoldingObject)) || thirdPersonMovement.dialogueBox.activeInHierarchy) return;
        
        if(FindObjectOfType<MechBossMovement>()){
            MechBossMovement hernandez = FindObjectOfType<MechBossMovement>(); 
            if (hernandez != null && objectHolding == "Drone" && Vector3.Distance(thirdPersonMovement.transform.position, hernandez.transform.position) < 80f) ;
            {
                ThrowObjectAction(hernandez.gameObject);
                return;
            }
        }

        EnemyMovement[] enemyMovements = FindObjectsOfType<EnemyMovement>();
        GameObject[] enemies = new GameObject[enemyMovements.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = enemyMovements[i].gameObject;
        }

        GameObject closestEnemy = null;
        if (enemyMovements.Length > 0)
        {
            GameObject potentialClosest = GetClosestEnemy(enemies).gameObject;
            if(potentialClosest != null /*&& Vector3.Distance(thirdPersonMovement.transform.position, potentialClosest.transform.position) < 30f*/){
                closestEnemy = potentialClosest;
            }
        }
        Debug.Log(closestEnemy);
        ThrowObjectAction(closestEnemy);
        gameObject.GetComponent<ThirdPersonMovement>().PlaySound(2);
    }
    
    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject o in enemies)
        {
            Transform t = o.transform;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
