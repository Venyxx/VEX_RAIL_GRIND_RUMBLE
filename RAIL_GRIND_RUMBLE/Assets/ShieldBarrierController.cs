using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBarrierController : MonoBehaviour
{
    private GameObject[] barriers;
    private GameObject[] generators;

    private void Start()
    {
        Transform barrierParent = transform.GetChild(1);
        Transform generatorParent = transform.GetChild(0);
        barriers = new GameObject[barrierParent.childCount];
        generators = new GameObject[generatorParent.childCount];
        AddChildrenToArray(barrierParent, barriers);
        AddChildrenToArray(generatorParent, generators);
    }

    private void Update()
    {
        if (AllGeneratorsDestroyed())
        {
            foreach (GameObject barrier in barriers)
            {
                Destroy(barrier);
            }
        }
    }

    private static void AddChildrenToArray(Transform parent, GameObject[] array)
    {
        int i = 0;
        foreach (Transform child in parent)
        {
            array[i++] = child.gameObject;
        }
    }

    private bool AllGeneratorsDestroyed()
    {
        int destroyCount = 0;
        foreach (GameObject generator in generators)
        {
            ShieldGeneratorController shieldGenerator = generator.GetComponent<ShieldGeneratorController>();
            if (shieldGenerator.IsDestroyed())
            {
                destroyCount++;
            }
        }
        return destroyCount >= generators.Length;
    }
}
