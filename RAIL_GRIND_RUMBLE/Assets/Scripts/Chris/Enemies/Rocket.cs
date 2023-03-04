using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 target;
    Transform explosion;
    float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        explosion = this.gameObject.transform.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.tag == "Player")
        {
            StartCoroutine(Explosion());
        }
    }
    
    public void TargetSet(Vector3 targetPos)
    {
        target = targetPos;
    }

    IEnumerator Explosion()
    {
        explosion.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
