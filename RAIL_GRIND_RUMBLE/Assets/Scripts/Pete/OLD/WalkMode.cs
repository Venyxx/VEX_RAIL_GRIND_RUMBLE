using UnityEngine;

public class WalkMode : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private LayerMask whatIsGround;
    private InputHandler _playerActions;
    private Rigidbody _rb;
    private bool _grounded;
    private float _horizontalInput;
    private float _verticalInput;

    private const float PlayerHeight = 2;

    private Vector3 _moveDirection;
    private Transform _orientation;
    // Start is called before the first frame update
    void Start()
    {
        _playerActions = new InputHandler();
        _playerActions.Player.Enable();
        _rb = GetComponent<Rigidbody>();
        GameObject orientationRef = GameObject.Find("Orientation");
        _orientation = orientationRef.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<ThirdPersonMovement>().enabled = false;
        _grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, whatIsGround);
        Vector2 moveInput = _playerActions.Player.Move.ReadValue<Vector2>();
        _horizontalInput = moveInput.x/2;
        _verticalInput = moveInput.y/2;
        Debug.Log(_rb.velocity);
    }

    private void FixedUpdate()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput * Time.deltaTime;
        _rb.velocity = new Vector3(_moveDirection.normalized.x * walkSpeed * 10f, _rb.velocity.y, _moveDirection.normalized.z * walkSpeed * 10f);
    }
}
