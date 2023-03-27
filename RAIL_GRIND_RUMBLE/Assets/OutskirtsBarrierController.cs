using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutskirtsBarrierController : MonoBehaviour
{
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private int quantity = 5;
    [SerializeField] private bool quantityBased = false; 
    

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
        if (!quantityBased && AllEnemiesDefeated())
        {
            DestroyBarriers();
        }

        if (quantityBased && CountDefeatedEnemies() >= quantity)
        {
            DestroyBarriers();
        }
    }

    private int CountDefeatedEnemies()
    {
        int defeatedEnemyCount = 0;
        foreach (var enemy in enemies)
        {
            if (!enemy.activeInHierarchy)
            {
                defeatedEnemyCount++;
            }
        }

        return defeatedEnemyCount;
    }

    private bool AllEnemiesDefeated()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                
                return false;
            }
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
