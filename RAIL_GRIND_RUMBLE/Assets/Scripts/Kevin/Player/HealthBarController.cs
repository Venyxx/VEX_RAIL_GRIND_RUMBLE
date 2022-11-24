using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    public PlayerHealth playerhealth;
    public Image fillImage;
    private Slider slider;
   
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        float fillValue = playerhealth.currentHealth / playerhealth.maxHealth;

        if(fillValue <= slider.maxValue /3)
        {
            fillImage.color = Color.red;
        }
        else if(fillValue > slider.maxValue / 3)
        {
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}
