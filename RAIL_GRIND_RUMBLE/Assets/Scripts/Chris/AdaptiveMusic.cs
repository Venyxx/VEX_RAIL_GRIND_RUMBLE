using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveMusic : MonoBehaviour
{
    public AudioSource standardMusic;
    public AudioSource combatMusic;
    [SerializeField] float maxVolume;
    float lerpDuration = 3;
    // Start is called before the first frame update
    void Start()
    {
        standardMusic = GetComponent<AudioSource>();
        combatMusic = transform.Find("CombatMusic").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SwitchSongs(string switchTo)
    {
        float timeElapsed = 0;

        //Possible way to delay combat music from ending for a short time
        /*if (switchTo == "Standard")
        {
            yield return new WaitForSeconds(2f);
        }*/

        while (timeElapsed < lerpDuration)
        {
            if (switchTo == "Combat")
            {
                combatMusic.volume = Mathf.Lerp(0, maxVolume, timeElapsed / lerpDuration);
                standardMusic.volume = Mathf.Lerp(maxVolume, 0, timeElapsed / lerpDuration);
            } else if (switchTo == "Standard")
            {
                standardMusic.volume = Mathf.Lerp(0, maxVolume, timeElapsed / lerpDuration);
                combatMusic.volume = Mathf.Lerp(maxVolume, 0, timeElapsed / lerpDuration);
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        if (switchTo == "Combat")
        {
            combatMusic.volume = maxVolume;
            standardMusic.volume = 0;
        } else if (switchTo == "Standard")
        {
            standardMusic.volume = maxVolume;
            combatMusic.volume = 0;
        }
        
    }
}
