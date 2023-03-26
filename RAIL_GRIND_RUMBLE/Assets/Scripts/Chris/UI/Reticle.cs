using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    GrappleDetection grappleDetection;
    GameObject reticleREF;
    Image reticleImage;
    Transform target;
    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        reticleREF = GameObject.Find("Reticle");
        reticleREF.SetActive(false);
        cam = GameObject.Find("Main Camera");
        reticleImage = reticleREF.GetComponent<Image>();
        grappleDetection = GetComponent<GrappleDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grappleDetection.aimPoints.Count <= 0)
        {
            //reticleREF.SetActive(false);
            return;
        } else {
            target = grappleDetection.currentAim;
            //reticleREF.SetActive(true);
            reticleREF.transform.rotation = Quaternion.Euler(reticleREF.transform.eulerAngles.x, reticleREF.transform.eulerAngles.y, reticleREF.transform.eulerAngles.z + 5);
        } 


        float minX = reticleImage.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = reticleImage.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.width - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        if (Vector3.Dot((target.position - cam.transform.position), cam.transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            } else {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        reticleImage.transform.position = pos;
    }

    void FixedUpdate()
    {
        if (ProgressionManager.Get().grappleUnlocked == true)
        {
            ReticleUpdate();
        }
    }

    void ReticleUpdate()
    {
        if (grappleDetection.aimPoints.Count <= 0)
        {
             reticleREF.SetActive(false);
        } else if (grappleDetection.aimPoints.Count > 0 && reticleREF.activeInHierarchy == false) {
             target = grappleDetection.currentAim;
             reticleREF.SetActive(true);
        } 
    }

    public void ReticleToggle (bool isOn)
    {
        if (ProgressionManager.Get().grappleUnlocked == false) return;

        if (isOn == true)
        {
            reticleREF.SetActive(true);
        }
        else {
            reticleREF.SetActive(false);
        }
    }
}
