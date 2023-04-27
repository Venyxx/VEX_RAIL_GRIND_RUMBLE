using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    //Movement
    //New Blend Tree//////////////////////////////////////////////////////////////////////////////////////////////
    private bool Gliding;
    bool brakeHeld = false;
    private bool onStairs;

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public playerSounds sfxRef;


    public float moveSpeed;
    [SerializeField] private float walkSpeed = 0.3f;
    [SerializeField] private float brakeSpeed = 0.1f;
    [SerializeField] private float decelerationSpeed = 5f;
    public float currentSpeed;
    private float baseMoveSpeed;
    private float speedLerp;
    public float groundDrag;
    Vector3 standingStill = new Vector3 (0,0,0);
    public Vector2 moveInput;
    public float maxSkateSpeed = 1;
    private float slopeLimit;

    public double speedPrint;

    private CollisionFollow playerCollisionFollowREF;
    private PlayerRailLeftCollider playerLeftColREF;
    private PlayerRailRightCollider playerRightColREF;

    //Jump
    //[SerializeField]private float jumpForceMax;
    //[SerializeField]private float jumpForceMin;
    [SerializeField]private float jumpForce;
    //[SerializeField]private float additionalJumpForce;
    public bool isJumping; // Kevin made this pub
    bool jumpDelayRunning = false;
    float jumpTimeCounter;
    private float jumpTimer;

    public float jumpCoolDown;
    public float airMultiplier;
    public bool canJump;
    
    //Grounded Check
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool Grounded;

    //Grapple Check
    public bool isGrappling;
    public float swingSpeed;
    GrappleHook grappleREF;
    PlayerAttack playerAttackREF;

    private Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 skateDirection;
    private GameObject ariWalkingShoes;

    public GameObject dialogueBox { get; private set; }
    public DialogueTemplate nearestDialogueTemplate = null;
    public DialogueManager dialogueManager { get; private set; }

    //Stop Overlapping Actions Stuff - Raul
    public AnimationiManager animManager;



    // animation IDs
    private float _animationBlend;
    private Animator _animator;
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDWalking;
    private int _animIDGrinding;
    private int _animIDMotionSpeed;
    private int _animIDWallRunLeft;
    private int _animIDWallRunRight;
    private int _animIDBrake;
    public float SpeedChangeRate = 10.0f;
    private float targetSpeed;
    public bool analogMovement;

    


    //Acceleration Timer
    public bool isBraking;
    private float maxTime = 1.5f;
    private float currentTime;
    private bool canAccelerate;

    public InputHandler playerActions { get; private set; }
    private bool moveKeyUp;

    private bool jump;
    private bool jumpCancel;

    public bool isWalking { get; private set; } = true;


    public Rigidbody rigidBody;

    //Coins
    private int coinCount;
    GameObject coinCounterREF;
    TextMeshProUGUI coinCountText;

    //speed hud
    private TextMeshProUGUI speedUIText;

    //Camera Switching
    GameObject currentCam;

    private GameObject[] skateWheels;
    private GameObject[] skateShoes;
    private GameObject playerWeapon;
    private GameObject sprayCan;
    private GameObject ariRig;
    [SerializeField] private Material skatesOnMaterial;
    [SerializeField] private Material skatesOffMaterial;

    //COLLIDER STUFF (so ari gets shorter when skates are turned off)
    private CapsuleCollider ariCollider;
    private static float skateCenterY = 0.8497467f;
    private static float skateHeight = 1.92239f;
    private static float walkCenterY = 0.8990228f;
    private static float walkHeight = 1.823838f; 

    //Sound Effects
    [SerializeField] private AudioClip[] playerSounds;
    private AudioSource audioSource;

    //Player Stun
    public PlayerHealth healthRef;
    public bool isStunned = false;
    public bool CombatPause = false;

    //skates and shoes
    private ETCCustomizationVendor ETCCustREF;
    private Manager ManagerREF;
    
    
    [Tooltip("Leave this checked in the inspector unless you are manually moving ari in the scene and need her to spawn where you moved her. " + 
             "Leave it unchecked if there are no gameObjects with 'LoadNewScene.cs' attached.")]
    public bool loadInDefaultLocation = false;

    // Start is called before the first frame update
    void Start()
    {

        ManagerREF = GameObject.Find("Manager").GetComponent<Manager>();
        ETCCustREF = GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>();
        ariWalkingShoes = GameObject.Find("walkShoes");
        ariWalkingShoes.SetActive(false);

        dialogueManager = FindObjectOfType<DialogueManager>();
        coinCount = ProgressionManager.Get().coinCount;
        moveSpeed = 0;
        baseMoveSpeed= 2;
        speedLerp = 2.22f;
        //max skate speed will be a better changable var later
        //maxSkateSpeed = 15;
        playerCollisionFollowREF = gameObject.GetComponent<CollisionFollow>();
        ariRig = transform.Find("AriRig").gameObject;
        playerLeftColREF = ariRig.gameObject.transform.Find("LeftCollider").gameObject.GetComponent<PlayerRailLeftCollider>();
        playerRightColREF = ariRig.gameObject.transform.Find("RightCollider").gameObject.GetComponent<PlayerRailRightCollider>();
        grappleREF = gameObject.GetComponent<GrappleHook>();
        playerAttackREF = gameObject.GetComponent<PlayerAttack>();
        healthRef = gameObject.GetComponent<PlayerHealth>();

        playerActions = new InputHandler();
        playerActions.Player.Enable();
        rigidBody.freezeRotation = true;
        isGrappling = false;
        canJump = true;

        
        //animation setting
        _animator = ariRig.gameObject.GetComponent<Animator>();
         AssignAnimationIDs();

        currentTime = 0;

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        

        //Coin Counter
        coinCounterREF = GameObject.Find("CoinCounter");
        coinCountText = coinCounterREF.GetComponent<TextMeshProUGUI>();
        coinCountText.text = $"{coinCount}";

        //Change/remove this line later based on level-to-level gameplay
        //coinCount = 0;

        //set speed UI element
        speedUIText = GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();

        skateShoes = GameObject.FindGameObjectsWithTag("SkateBody");
        skateWheels = GameObject.FindGameObjectsWithTag("SkateWheel");
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
        sprayCan = GameObject.FindGameObjectWithTag("PlayerCan");
        ariCollider = GetComponent<CapsuleCollider>();
        WalkToggleHelper();

        //Audio
        audioSource = GetComponent<AudioSource>();
        
        GameObject dialogueParent = GameObject.Find("Dialogue");
        dialogueBox = dialogueParent.transform.Find("DialogueBox").gameObject;
        //Debug.Log($"DialogueBox is null? {dialogueBox == null}");
        healthRef = GetComponent<PlayerHealth>();

        //atkScript = GetComponent<PlayerAttack>(); //added by pete to fix raul's nullref since he was assigning this in the inspector

        

        if (SceneManager.GetActiveScene().name == "InnerRingLevel")
        {
            Debug.Log("LOADED INTO INNER RING ");
            MainQuest2 mq2 = ProgressionManager.Get().mainQuest2;
            Debug.Log("MAINQUEST2 IS NULL? " + (mq2 == null));
            Debug.Log("MAINQUEST 2 IS NOT ACTIVE?" + (!mq2.isActive));
            if (mq2 == null || !mq2.isActive)
            {
                loadInDefaultLocation = false;
                Debug.Log("SET LOAD IN DEFAULT LOCATION TO FALSE");
                /*transform.localPosition = LoadNewScene.innerRingDefaultSpawnVector;
                loadInDefaultLocation = false;
                Debug.Log("Location Vector: " + LoadNewScene.locationVector);
                Debug.Log("Inner Ring Vector: " + LoadNewScene.innerRingDefaultSpawnVector);
                Debug.Log("My position: " + transform.localPosition);*/
            }
        }
        
        if (loadInDefaultLocation == true)
        {
            transform.localPosition = LoadNewScene.locationVector;
            Debug.Log("Location Vector: " + LoadNewScene.locationVector);
            Debug.Log("My position: " + transform.localPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
         /*if(isBraking == true)
        {

            float deceleration = 30f;
            if (currentSpeed > 0 || rigidBody.velocity.magnitude > 0)
            {
                currentSpeed -= currentSpeed * 0.9f * Time.deltaTime;
                //rigidBody.velocity -= 0.1f*rigidBody.velocity;
                 //rigidBody.drag = 200;
                isBraking = true;
                _animator.SetBool(_animIDBrake, false);
                Debug.Log("BrakingNow");

            }
        }*/

        /*if(currentSpeed < 3.4f)
        {
            isBraking=false;
        }*/

        

        
        

        


        if (moveInput.x == 0 && moveInput.y == 0 && Grounded == true && currentSpeed >5f)
        {
            Gliding = true;
            _animator.SetBool("Gliding", true);
            //Debug.Log("NOTMOVING");
        }

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            Gliding = false;
            _animator.SetBool("Gliding", false);
            //Debug.Log("Moving");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if(animManager.misControlEnabled == true || isBraking == false) //Stop Overlapping Actions - Raul
        {
        PlayerMovement();
        }
        
        //Grounded Check
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); 
        _animator.SetBool(_animIDGrounded, Grounded);

        //gravity 
        GetComponent<Rigidbody>().AddForce( new Vector3(0.0f, -5.81f, 0.0f), ForceMode.Acceleration);
        
        if(animManager.misControlEnabled == true) //Stop Overlapping Actions - Raul
        {
        PlayerInput();

        }

        //Stop Overlapping Actions -Raul /////////////////////////////////////////////////////////////////////////////////////////////////////////

        if(animManager.misControlEnabled == true) 
        {
        playerActions.Player.Enable();
        }
        else 
        {
        playerActions.Player.Disable();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Drag
        if (Grounded == true && currentSpeed <= 0)
        {
            rigidBody.drag = groundDrag;
 
        }else 
        {
            rigidBody.drag = 0;  
        }


        //Variable Jump Height - Commented out for now
        /*if (jumpDelayRunning == false)
        {
            if (isJumping == true && jumpTimeCounter > 0)
            {
                TapJump(jumpForce/3);
                jumpTimeCounter -= Time.deltaTime;
            } else 
            {
                isJumping = false;
            }
        }*/

        //grinding anims
        if (gameObject.GetComponent<CollisionFollow>().isGrinding)
            _animator.SetBool(_animIDGrinding, true);
        else if (!gameObject.GetComponent<CollisionFollow>().isGrinding)
            _animator.SetBool(_animIDGrinding, false);

        
        

        //animation transitioning
        if(isBraking && currentSpeed > 0.1)
        {
            _animator.SetBool(_animIDBrake, true);
            ariRig.transform.rotation = Quaternion.Slerp(ariRig.transform.rotation, Quaternion.LookRotation(rigidBody.velocity.normalized), Time.deltaTime * 7f);
        }
        else 
        {
            
            if (rigidBody.velocity.magnitude > 6 )     
            {
                //slide by anim
                if (verticalInput == 0)
                    targetSpeed = Mathf.Lerp(targetSpeed, 2, 0.35f);
                else
                targetSpeed = Mathf.Lerp(targetSpeed, 4, 0.5f);
                
            }
            else if (rigidBody.velocity.magnitude <= 6 && rigidBody.velocity.magnitude > 1)
            {
                if (horizontalInput == 0 || verticalInput == 0)
                {
                //normal walking slow run animation, only plays during decel not accel
                    targetSpeed = Mathf.Lerp(targetSpeed, 3, 0.35f);
                }
            }
            else 
                targetSpeed = Mathf.Lerp(targetSpeed, 1, 0.35f);
        }
        

        if (healthRef.Dizzy && !isStunned)
        {
            StartCoroutine(Stun());
        }

        if (Grounded)
        {
            isJumping = false;
        }

        //THIS PREVENTS THE GLITCH WHERE SPINNING THE STICK MAKES ARI GET OFF-AXIS
        Quaternion ariRigRotation = ariRig.transform.localRotation;
        ariRig.transform.localRotation = new Quaternion(0, ariRigRotation.y, ariRigRotation.z, ariRigRotation.w);


        //DEBUG
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (GameObject.Find("Phase2Teleport"))
            {
                transform.position = GameObject.Find("Phase2Teleport").transform.position;
            } else if (GameObject.Find("Hernandez Checkpoint"))
            {
                transform.position = GameObject.Find("Hernandez Checkpoint").transform.position;
            }
            Debug.Log("Teleport");
        }
    }

    
    
    public void Jump(InputAction.CallbackContext context)
    {
        if(animManager.misControlEnabled == true) //Stop Overlapping Actions -Raul
        {

         if (isWalking || dialogueManager.freezePlayer) return;
         if (!canJump) return;
         //if (isGrappling) return;
        
         if (context.started && Grounded)
         { 
             //Grounded = false;
            isJumping = true;
            jumpTimeCounter = 0.35f;
            _animator.SetBool(_animIDJump, true);
            _animator.SetBool(_animIDGrounded, false);

            //stops skate sound
            sfxRef.stopSkateSFX();

            //Added to account for not jumping as high while going fast - this method doesn't work smoothly; jumping error likely has to do with forward velocity
            //if (currentSpeed > 15f)
            //{
            //    TapJump(jumpForce * 10f);
            //} else {
                TapJump(jumpForce);
            //}

            canJump = false;
            
            PlaySound(1);

            ExitRailMain();

            //Commented out - related to variable jump height
            //StartCoroutine(JumpHoldDelay());
         }

         if (context.canceled)
         {
          //  isJumping = false;
         }

        }
    }

    public void ToggleWalk(InputAction.CallbackContext context)
    {
        if (isBraking || !context.started || dialogueManager.freezePlayer || !Grounded || SceneManager.GetActiveScene().name.Equals("Ari's House")) return;
        isWalking = !isWalking;
        WalkToggleHelper();
    }

    void WalkToggleHelper()
    {
        //Debug.Log($"Walking: {isWalking}");

        foreach (GameObject skateWheel in skateWheels)
            skateWheel.SetActive(!isWalking); //if walking, turn off wheels. 

        foreach (GameObject shoe in skateShoes)
            shoe.GetComponent<SkinnedMeshRenderer>().material = isWalking ? skatesOffMaterial : skatesOnMaterial;

        if (isWalking)
        {
            ariCollider.center = new Vector3(ariCollider.center.x, walkCenterY, ariCollider.center.z);
            ariCollider.height = walkHeight;
            playerWeapon.SetActive(false);
            sprayCan.SetActive(true);
            ariWalkingShoes.SetActive(true);

            ManagerREF.ariSkateOptions[SaveManager.Instance.state.activeAriSkate].SetActive(false);

        }
        else
        {
            ariCollider.center = new Vector3(ariCollider.center.x, skateCenterY, ariCollider.center.z);
            ariCollider.height = skateHeight;
            playerWeapon.SetActive(true);
            sprayCan.SetActive(false);
            ariWalkingShoes.SetActive(false);

            //change to shoes
            ManagerREF.ariSkateOptions[SaveManager.Instance.state.activeAriSkate].SetActive(true);
        }
    }

    IEnumerator JumpHoldDelay()
    {
        jumpDelayRunning = true;
        yield return new WaitForSeconds(0.15f);
        jumpDelayRunning = false;
    }

    void FixedUpdate()
    {
        //PlayerMovement();
    }

    

    public void PlayerInput()
    {
        //Debug.Log($"DialogueBox is active in hierarchy? {dialogueBox.activeSelf}");
        if (!dialogueManager.freezePlayer )
        {
            moveInput = playerActions.Player.Move.ReadValue<Vector2>();
            horizontalInput = moveInput.x/2;
            verticalInput = moveInput.y/2;
        }
        else
        {
            moveInput = new Vector2(0, 0);
            horizontalInput = 0;
            verticalInput = 0;
        }


        //if (rigidBody.velocity.magnitude < 1)
        //{ currentSpeed = 0; }
        if (rigidBody.velocity.magnitude < 1)
            currentSpeed = 0;

        //if there is player input and we are, accelerate
        //ALLOW THIS DEAD ZONE VALUE (0.35) TO BE CUSTOMIZED IN THE SETTINGS
        if ((verticalInput >= 0.35 || horizontalInput >= 0.35 || verticalInput <= -0.35 || horizontalInput <= -0.35) && !GetComponent<WallRun>().isWallRunning && !isBraking)
        {
            moveKeyUp = false;
            //kick start movement
            if (rigidBody.velocity.magnitude < baseMoveSpeed && isBraking == false)
                currentSpeed = baseMoveSpeed;

            //accelerate
            float acceleration = 9f; //originally 2
            if(currentSpeed < maxSkateSpeed && isWalking == false)
            currentSpeed += acceleration * Time.deltaTime;
        } 
        else
        {
            //if no input forward
            moveKeyUp = true;
            
            //if moving, decelerate
            if (currentSpeed > 0)
            {
                currentSpeed -= decelerationSpeed * Time.deltaTime;
                _animator.SetBool(_animIDBrake, false);

            }
        }

    }

    public void Brake(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            isBraking = true;
            if (currentSpeed > 0 || rigidBody.velocity.magnitude > 0)
            {
                currentSpeed -= currentSpeed * brakeSpeed * Time.deltaTime;
                Debug.Log("BrakingNow");
            }
        }

        if (context.canceled)
        {
            isBraking = false;
            _animator.SetBool(_animIDBrake, false);
        }



    } 

    void PlayerMovement()
    {
        //Uncomment once air movement is programmed
        if (isGrappling == true || healthRef.Dizzy || CombatPause)
        {
            return;
        }
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!dialogueManager.freezePlayer && moveKeyUp == false)
            skateDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;



        if (isWalking)
        {
            rigidBody.velocity = new Vector3(moveDirection.normalized.x * walkSpeed * 10f, rigidBody.velocity.y, moveDirection.normalized.z * walkSpeed * 10f);


          



            //change anim
            if ((moveInput.x != 0 || moveInput.y != 0) && !dialogueManager.freezePlayer)
                _animator.SetBool(_animIDWalking, true);
            else
                _animator.SetBool(_animIDWalking, false);
                
            //target speed changes the anim blend tree.
            targetSpeed = 0;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //velocity = 0;
            
        }
        else if (Grounded)
        {
            rigidBody.velocity = new Vector3(skateDirection.normalized.x * currentSpeed, rigidBody.velocity.y, skateDirection.normalized.z * currentSpeed);

            //change anim
            _animator.SetBool(_animIDWalking, false);
            _animator.SetBool(_animIDJump, false);
  
        } 
        else 
        {
            if (isGrappling == true)
            {
                rigidBody.AddForce(moveDirection.normalized * currentSpeed * 10f * swingSpeed, ForceMode.Force);
            } else {
                rigidBody.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
            }
            _animator.SetBool(_animIDJump, true);
        }

        //checking for wall running animations
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            if (gameObject.GetComponent<WallRun>().wallLeft  &&   gameObject.GetComponent<WallRun>().isWallRunning)
                _animator.SetBool(_animIDWallRunLeft, true);
            else if (gameObject.GetComponent<WallRun>().wallRight   &&   gameObject.GetComponent<WallRun>().isWallRunning)
                _animator.SetBool(_animIDWallRunRight, true);
            else
            {
                _animator.SetBool(_animIDWallRunLeft, false);
                _animator.SetBool(_animIDWallRunRight, false);
            }
                
        }

        //checking for grinding animations
        if (gameObject.GetComponent<CollisionFollow>().isGrinding)
        {
            _animator.SetBool(_animIDGrinding, true);
        }else
            _animator.SetBool(_animIDGrinding, false);


        //clamp skate speed
        if (rigidBody.velocity.magnitude > maxSkateSpeed) //arbritrary cap point
        {

            Vector3 currentVelocity = rigidBody.velocity;
            Vector3 passing = Vector3.Normalize(currentVelocity);
            passing *= maxSkateSpeed;
            rigidBody.velocity = new Vector3(passing.x, rigidBody.velocity.y,passing.z);
        }
            



        
        //adjust animator speed
        _animationBlend = currentSpeed;
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat(_animIDSpeed, _animationBlend);
        //_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);

        //update speed UI
        SetSpeedUI();


        //Check if player is aiming
        if (GameObject.Find("AimingCam"))
        {
            GrappleDetection grappleDet = GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>();
            transform.RotateAround(grappleDet.currentAim.position, Vector3.up, currentSpeed * 5 * Time.deltaTime);
            /*if (Vector3.Distance(transform.position, grappleDet.currentAim.position) > 10f)
            {
                Debug.Log("Distance = "+Vector3.Distance(transform.position, grappleDet.currentAim.position));
                var lookPos = grappleDet.currentAim.position - transform.position;
                lookPos.y = 0;
                var newRotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0);
                transform.position = Vector3.MoveTowards(transform.position, grappleDet.currentAim.position, currentSpeed * 5 * Time.deltaTime);
            }*/
            
        }

    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z); 
            
            
        }

        if (horizontalInput == 0 && verticalInput == 0 && !moveKeyUp)
        {
            rigidBody.velocity -= 0.1f * rigidBody.velocity;
            moveKeyUp = true;
        }

        
        
    }

    void TapJump(float force)
    {
        if (isGrappling) return;
        if (!canJump) return;

        //rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(transform.up * force, ForceMode.Impulse);
    }

    void ResetJump(/*float cooldown*/)
    {
        //yield return new WaitForSeconds(cooldown);
        canJump = true;
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDWalking = Animator.StringToHash("Walking");
        _animIDGrinding = Animator.StringToHash("Grinding");
        _animIDWallRunLeft = Animator.StringToHash("WallRunLeft");
        _animIDWallRunRight = Animator.StringToHash("WallRunRight");
        _animIDBrake = Animator.StringToHash ("Brake");
    }


    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("wallrun") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") 
        || collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            currentSpeed = 0;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.tag == "Rail")
        {
            if (canJump == false)
            {
                ResetJump();
            }
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.tag == "END")
        {
            ExitRailMain();
        }
    }

    public void AddCoin(int coin)
    {
        Debug.Log(coin);
        coinCount = coinCount + coin;
        ProgressionManager.Get().coinCount = coinCount;
        Debug.Log(coinCount);
        coinCountText.text = $"{coinCount}";
        PlaySound(0);
        var questTracker = ProgressionManager.Get();
        if (questTracker.CurrentCountQuestType is CountQuestType.Coins)
        {
            CountQuest quest = (CountQuest) questTracker.currentQuest;
            quest.IncrementCount();
        }
    }

    //Camera Switch
    public void SwitchCameraTPM(InputAction.CallbackContext context)
    {
        if (context.performed || dialogueManager.freezePlayer) return;
        
        if (!isWalking)
        {
            if (context.started)
            {
                if (GameObject.Find("BasicCam") && isGrappling) 
                {
                    return;
                } else if (GameObject.Find("BasicCam"))
                {
                    currentCam = GameObject.Find("BasicCam");
                } else if (GameObject.Find("AimingCam"))
                {
                    currentCam = GameObject.Find("AimingCam");
                }
                currentCam.GetComponent<ThirdPersonCamera>().SwitchCameraStarted();
            }

            if (context.canceled)
            {
                currentCam.GetComponent<ThirdPersonCamera>().SwitchCameraCanceled();
            }
        }
        
    }

    
   public int print;
    private void SetSpeedUI()
    { 
        //m/s to mph
        var passing = rigidBody.velocity.magnitude;
        speedPrint =  (double) passing * 2.2369362920544;
        print = (int) speedPrint;

        //print
        if (print < 10)
        {
            speedUIText.text = "0" + print.ToString();
        }else
        speedUIText.text = print.ToString();
    }
    private void ExitRailMain ()
    {
        //release from rail if grinding
            if (playerCollisionFollowREF.isGrinding)
            {
                if (playerCollisionFollowREF.leftGrinding)
                    playerLeftColREF.ExitRailLeft();
                else
                    playerRightColREF.ExitRailRight();

            }
    }

    public void PlaySound(int sound)
    {
        if (PauseMenu.isPaused) return;

        audioSource.clip = playerSounds[sound];
        audioSource.Play(0);

        //Coin = 0
        //Jump = 1
        //Railing = 2
        //Skating = 3
    }

    private IEnumerator Stun()
    {
        isStunned = true;
        dialogueManager.freezePlayer = true;
        _animator.SetTrigger("StunStart");
        yield return new WaitForSeconds(3);
        _animator.SetTrigger("StunEnd");
       // yield return new WaitForSeconds(1);
        isStunned = false;
        dialogueManager.freezePlayer = false;
        healthRef.IsDizzy(false);
        StopCoroutine(Stun());
    }

    public void RecalculateStats ()
    {
        healthRef = gameObject.GetComponent<PlayerHealth>();
        if (SaveManager.Instance.state.activeAriSkate == 0 || SaveManager.Instance.state.activeAriSkate == 2 )
        {
            healthRef.maxHealth = 200; // base current stat says 100
            maxSkateSpeed = 30; //current stat is 15
            //suppose to be decrease charge time 
            Debug.Log("updating stats to shell 0, health 200, max speed 30");

        } else if (SaveManager.Instance.state.activeAriSkate == 1 || SaveManager.Instance.state.activeAriSkate == 4 )
        {
            playerAttackREF = GetComponent<PlayerAttack>();
            playerAttackREF.skateBuffDamage = 2;
            healthRef.maxHealth = 250;
            Debug.Log("updating stats to shell 1, damage buff 2, max health 250");

        } else if (SaveManager.Instance.state.activeAriSkate == 3 || SaveManager.Instance.state.activeAriSkate == 5 )
        {
            maxSkateSpeed = 40;
            healthRef.maxHealth = 300f;
            Debug.Log("updating stats to shell 2, max speed 40, health 300");
        }
    }

    

}