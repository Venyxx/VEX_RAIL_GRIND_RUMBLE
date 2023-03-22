using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Plug : MonoBehaviour
{
    public static int plugCount = 0;
    [SerializeField] Material blue;
    [SerializeField] Material gray;
    Renderer ren;
    bool electric;
    bool shockCycleRunning;
    GrappleHook grappleHook;
    GrappleDetection grappleDetector;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<Renderer>();
        shockCycleRunning = false;
        plugCount++;
        grappleHook = GameObject.Find("playerPrefab").GetComponent<GrappleHook>();
        grappleDetector = GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>();
        playerHealth = GameObject.Find("playerPrefab").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shockCycleRunning)
        {
            StartCoroutine(ShockCycle());
        }
    }

    IEnumerator ShockCycle()
    {
        shockCycleRunning = true;
        electric = true;
        ren.material = blue;
        yield return new WaitForSeconds(7f);
        electric = false;
        ren.material = gray;
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
                grappleDetector.aimPoints.Remove(this.transform);
                grappleDetector.aimPointCount--;
                plugCount--;
                Debug.Log("Plug Count: "+plugCount);
                Destroy(gameObject);
            }
        }
    }
}
