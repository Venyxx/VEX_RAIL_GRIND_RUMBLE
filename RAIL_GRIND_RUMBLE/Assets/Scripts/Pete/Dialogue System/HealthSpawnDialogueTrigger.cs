
using UnityEngine;

public class HealthSpawnDialogueTrigger : DialogueTrigger
{
    [SerializeField] private bool shouldSpawnHealth;
    [SerializeField] private GameObject healthSpawnPickUp;
    [SerializeField] private float launchForce;
    private HealthPickupController healthPickup;
    public void SpawnHealth()
    {
        if (shouldSpawnHealth && healthPickup == null)
        {
            //GameObject model = this.gameObject.transform.parent.Find("Model").gameObject;
            GameObject pickup = Instantiate(healthSpawnPickUp, new Vector3(23.3190002f, 38.5600014f, 957.877014f), Quaternion.identity);
            healthPickup = pickup.GetComponent<HealthPickupController>();
            healthPickup.canBePickedUp = false;
            Invoke("EnablePickup", 1f);
        }
    }

    public void EnablePickup()
    {
        healthPickup.canBePickedUp = true;
    }
}