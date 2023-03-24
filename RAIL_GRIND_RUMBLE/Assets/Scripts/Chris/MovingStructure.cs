using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStructure : MonoBehaviour
{
    Vector3 ogPosition;
    [SerializeField] Transform moveTo;
    public bool move;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ogPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        if (move == true && transform.position != moveTo.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTo.position, step);
        } else if (move == false && transform.position != ogPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, ogPosition, step);
        } else if (move == false && transform.position == ogPosition && this.gameObject.tag == "AimPoint")
        {
            this.gameObject.SetActive(false);
            GameObject.Find("playerPrefab").GetComponent<GrappleHook>().StopSwing();
            GrappleDetection grappleDetector = GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>();
            grappleDetector.aimPoints.Remove(this.transform);
            grappleDetector.aimPointCount--;
        }
    }

    /*void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Donovan")
        {
            StartCoroutine(MovePiece());
        }
    }*/

    /*public IEnumerator MovePiece()
    {
        move = true;
        yield return new WaitForSeconds(10f);
        move = false;
    }*/
}
