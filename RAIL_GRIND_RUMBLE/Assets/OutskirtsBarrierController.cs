using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutskirtsBarrierController : MonoBehaviour
{
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject[] barriers;

    private List<GameObject> enemies;
    void Start()
    {
        enemies = new List<GameObject>();
        foreach (Transform enemy in enemiesParent.transform)
        {
            enemies.Add(enemy.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AllEnemiesDefeated())
        {
            DestroyBarriers();
        }
    }

    private bool AllEnemiesDefeated()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy) return false;
        }

        return true;
    }

    private void DestroyBarriers()
    {
        foreach (var barrier in barriers)
        {
            Destroy(barrier);
        }
        Destroy(this.gameObject);
    }
}
