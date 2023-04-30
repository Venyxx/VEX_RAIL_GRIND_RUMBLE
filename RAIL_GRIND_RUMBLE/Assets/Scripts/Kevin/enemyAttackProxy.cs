using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.ProBuilder.Shapes;

public class enemyAttackProxy : MonoBehaviour
{

    public AttackRadius ATKREF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyHitNow()
    {
        // ATKREF.AttackCoroutine = ATKREF.StartCoroutine(ATKREF.Attack());
        // Debug.Log("enemyattackingpls");
    }
}
