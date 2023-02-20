using UnityEngine;

public class ArtTestRotator : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool spinning = true;
    private bool canIncreaseDecreaseSpeed = true;

    void Start()
    {
        if (gameObject.name == "light")
        {
            canIncreaseDecreaseSpeed = false;
        }
    }

    void Update()
    {

        if (spinning)
        {
            transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSpeed();
        }

        if (Input.GetKey(KeyCode.UpArrow) && canIncreaseDecreaseSpeed)
        {
            speed += 15f *Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) && canIncreaseDecreaseSpeed)
        {
            speed -= 15f * Time.deltaTime;
        }
    }

    private void ToggleSpeed()
    {
        spinning = !spinning;
    }
}
