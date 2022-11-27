using UnityEngine;

public class PlayerThrownObject : MonoBehaviour
{
    public bool target;
    Transform playerCurrentAim;
    GameObject playerREF;

    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.Find("GrappleDetector");
        playerCurrentAim = playerREF.gameObject.GetComponent<GrappleDetection>().currentAim;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == true && playerCurrentAim.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentAim.transform.position, 25f * Time.deltaTime);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
