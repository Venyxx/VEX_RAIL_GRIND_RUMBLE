using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    Transform target;
    GameObject playerREF;
    float speed = 1f;
    bool chargeRunning;
    bool shootRunning;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        target = this.gameObject.transform.Find("Target");
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {   
        //Turn turret toward player
        if (!chargeRunning)
        {
            float singleStep = speed * Time.deltaTime;
            Vector3 playerDirection = playerREF.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        //Charging
        if(!chargeRunning && !shootRunning)
        {
            StartCoroutine(Charging());
        }

        //Targeting
        if (target.gameObject.activeInHierarchy == true)
        {
            target.transform.position = new Vector3(playerREF.transform.position.x, playerREF.transform.position.y, playerREF.transform.position.z);
        } else {
            target.transform.position = target.transform.position;
        }
    }

    IEnumerator Charging()
    {
        chargeRunning = true;
        target.gameObject.SetActive(false);
        yield return new WaitForSeconds(20f);
        StartCoroutine(Shooting());
        chargeRunning = false;
    }

    IEnumerator Shooting()
    {
        shootRunning = true;
        target.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);

        var z = transform.rotation.eulerAngles.z;
        var y = transform.rotation.eulerAngles.y;
        var thisRot = Quaternion.Euler(new Vector3(90,y,z));
        var rot = Quaternion.Euler(new Vector3(-90, y, z));

        GameObject rocketShot = Instantiate(rocket, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), rot);
        Vector3 aimAt = new Vector3(target.position.x, target.position.y, target.position.z);
        rocketShot.GetComponent<Rocket>().TargetSet(aimAt);
        StartCoroutine(Charging());

        transform.rotation = thisRot;

        shootRunning = false;
    }
}
