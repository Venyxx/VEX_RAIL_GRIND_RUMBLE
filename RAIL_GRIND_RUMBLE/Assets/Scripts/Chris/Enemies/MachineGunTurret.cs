using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTurret : MonoBehaviour
{
    float speed = 1f;
    GameObject playerREF;
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletCount;
    bool shootRunning;
    Vector3 newDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
    }

    // Update is called once per frame
    void Update()
    {
            float singleStep = speed * Time.deltaTime;
            Vector3 playerDirection = playerREF.transform.position - transform.position;
            newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            if (!shootRunning)
            {
                StartCoroutine(Shoot());
            }
    }

    IEnumerator Shoot()
    {
        shootRunning = true;

        //Shoot amount of bullets specified under Bullet Count
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(newDirection));
            newBullet.GetComponent<Bullet>().Rigidbody.AddForce(newBullet.transform.forward * newBullet.GetComponent<Bullet>().MoveSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.3f);
        }

        //Cooldown
        yield return new WaitForSeconds(10f);

        shootRunning = false;
    }
}
