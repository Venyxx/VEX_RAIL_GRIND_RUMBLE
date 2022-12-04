using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarSpawner : MonoBehaviour
{
    private GameObject car1;
    private GameObject car2;
    private GameObject car3;

    [SerializeField] private GameObject[] carSelection;
    private PathCreator pathCreator;

    private Transform SpawnPoint;
    public float IntervalBetweenSpawn;

    private float spawnBetweenTime;
    private bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        car1 = GameObject.Find("BlueMovingCar");
        car2 = GameObject.Find("RedMovingCar");
        car3 = GameObject.Find("YellowMovingCar");

        carSelection = new GameObject [] {car1, car2, car3};
        SpawnPoint = gameObject.transform;
        //pathCreator = gameObject.transform.parent.GetComponent<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnBetweenTime <= 0)
        {
            //make the car
            int rand = Random.Range(0, carSelection.Length);
            var MadeCar = Instantiate(carSelection[rand], SpawnPoint.position, Quaternion.identity);

            //set its parent to the asset
            MadeCar.transform.parent = gameObject.transform.root;

            spawnBetweenTime = IntervalBetweenSpawn;
        }
        else
        {
            spawnBetweenTime -= Time.deltaTime;

        }
    }

    void OnTriggerStay (Collider col)
    {
        if (col.gameObject.tag == "MovingCar")
        {
            canSpawn = false;
        } else 
            canSpawn = true;
    }

        void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "MovingCar")
        {
            canSpawn = true;
        }
    }
}
