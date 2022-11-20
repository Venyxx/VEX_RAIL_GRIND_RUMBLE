using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRailCollider : MonoBehaviour
{
    private ThirdPersonMovement ThirdPersonMovementREF;

    // Start is called before the first frame update
    void Start()
    {
        ThirdPersonMovementREF = gameObject.GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.name == "PositiveRunner")
        {
            gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, col.gameObject.transform.position, 0.1f);
            gameObject.transform.parent = col.transform;
            col.gameObject.GetComponent<PositiveRunner>().SpeedAdjustment(ThirdPersonMovementREF.moveSpeed);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.name == "PositiveRunner")
        {
            col.gameObject.GetComponent<PositiveRunner>().SpeedReAlignment();
        }
    }

    public void JumpOffRail(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        transform.parent = null;
    }
}
