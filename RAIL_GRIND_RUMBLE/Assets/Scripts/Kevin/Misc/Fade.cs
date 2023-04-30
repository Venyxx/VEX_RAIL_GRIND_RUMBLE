using System;
using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{

    [SerializeField] private Color targetColor = new Color(255, 0, 0, 0);
    [SerializeField] private Material materialToChange;
    [SerializeField] private float lifeTimeSeconds = 5;
    private ParticleSystem effect;

    void Start()
    {

        materialToChange = gameObject.GetComponent<Renderer>().material;
        StartCoroutine(LerpFunction(targetColor, lifeTimeSeconds));
        effect = transform.GetChild(0).GetComponent<ParticleSystem>();
        StartCoroutine(WaitAndStopParticle());
    }
    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = materialToChange.color;
        while (time < duration)
        {
            materialToChange.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        materialToChange.color = endValue;
        Destroy(gameObject);
    }

    IEnumerator WaitAndStopParticle()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Stopping particle effect");
        effect.Stop();
    }
}