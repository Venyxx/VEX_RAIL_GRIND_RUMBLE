using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceTrigger : MonoBehaviour
{
    public bool danceTime;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>().moveInput.x == 0 && GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>().moveInput.y == 0)
            danceTime = true;
        else
            danceTime = false;
    }
    
    void OnTriggerEnter (Collider col)
    {
        Debug.Log("hello dance trigger" + col.gameObject);
        if (col.gameObject.tag == "Player" && danceTime)
        {
            playerAnim = GameObject.Find("AriRig").GetComponent<Animator>();
            playerAnim.SetBool("isDancing", true);
        }

        if (col.gameObject.tag == "Dance" && danceTime)
        {
            playerAnim = GameObject.Find("AriRig").GetComponent<Animator>();
            playerAnim.SetBool("isDancing", true);
        }
    }
    void OnTriggerExit (Collider col)
    {
        Debug.Log("goodbye dance trigger");
        if (col.gameObject.name == "playerPrefab")
        {
            playerAnim = GameObject.Find("AriRig").GetComponent<Animator>();
            danceTime = false;
            playerAnim.SetBool("isDancing", false);
        }
    }
}
