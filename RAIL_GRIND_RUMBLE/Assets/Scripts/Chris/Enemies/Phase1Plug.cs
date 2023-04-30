using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Phase1Plug : MonoBehaviour
{
    //[SerializeField] Material blue;
    //[SerializeField] Material gray;
    [SerializeField] GameObject plugParent;
    Rigidbody rb;
    BoxCollider bc;
    Renderer ren;
    bool electric;
    bool shockCycleRunning;
    GrappleHook grappleHook;
    GrappleDetection grappleDetector;
    PlayerHealth playerHealth;
    public bool unplugged { get; private set; }

    public UnityEvent shake;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<Renderer>();
        shockCycleRunning = false;
        grappleHook = GameObject.Find("playerPrefab").GetComponent<GrappleHook>();
        grappleDetector = GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>();
        playerHealth = GameObject.Find("playerPrefab").GetComponent<PlayerHealth>();
        rb = transform.parent.GetComponent<Rigidbody>();
        bc = transform.parent.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shockCycleRunning)
        {
            //StartCoroutine(ShockCycle());
        }
    }

    IEnumerator ShockCycle()
    {
        shockCycleRunning = true;
        electric = true;
        //ren.material = blue;
        yield return new WaitForSeconds(7f);
        electric = false;
        //ren.material = gray;
        yield return new WaitForSeconds(3f);
        shockCycleRunning = false;
    }

    public IEnumerator Grappled()
    {
        if (electric)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("FACK YOU");
            grappleHook.StopSwing();
            playerHealth.IsDizzy(true);
        } else {
            yield return new WaitForSeconds(0.5f);
            if (grappleHook.isGrappling)
            {
                grappleHook.StopSwing();
            }
            this.GetComponent<BoxCollider>().enabled = false;
            grappleDetector.aimPoints.Remove(this.transform);
            grappleDetector.aimPointCount--;
            bc.enabled = false;
            rb.isKinematic = false;
            InvokeRepeating("ShockwaveEvent", 3f, 4f);

            // GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>().aimPoints.Remove(this.transform);
            yield return new WaitForSeconds(2f);
            unplugged = true;
        }
    }


    private void ShakeEvent()
    {
        shake.Invoke();
    }
}
