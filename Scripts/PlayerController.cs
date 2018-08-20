using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController character;

    Animator animator;
    int jumpingHash;
    int runningHash;

    bool isAlive = true;

    public float speed;
    public float riseOutOfTheGroundSpeed;
    public float forwardMovementSpeed = 1;
    public float gravity;
    public Vector2 friction;

    public Animator GameOverUI;
    public ScreenManager screenManager;

    Vector2 velocity;
    Vector2 oldVelocity;
    Vector3 oldPosition;
    Vector2 acceleration;

    public float jumpHeight;
    public float jumpDistance;
    private bool hasJumped = false;
    bool isGrounded;
    bool isGroundedForJump;
    bool isGroundedNow;
    public GameObject groundChecker;
    public float groundDistance;
    public Vector3 checkSize;
    public LayerMask ground;
    public Vector3 drag;

    public Transform obsHitPosition;
    public float obsCheckRadius;
    public LayerMask obstacle;
    bool hasHitObstacle;

    public GameStateManager GSM;
    GameState currentState;

    public Transform startPosition;

    public TextMesh isGroundedText;


    public GameState GetCurrentState()
    {
        return GSM.gameState;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        jumpingHash = Animator.StringToHash("Jumping");
        runningHash = Animator.StringToHash("Running");

    }

    // Use this for initialization
    void Start()
    {
        character = GetComponent<CharacterController>();
        currentState = GetCurrentState();
        isGrounded = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState = GetCurrentState();
        if (currentState == GameState.GAMEPLAY)
        {
            AliveState();
        }
        else if(currentState == GameState.MAIN_MENU)
        {
            MenuState();
        }
        else if(currentState == GameState.GAME_OVER)
        {
            DeadState();
        }
        else if(currentState == GameState.RESTART)
        {

            this.transform.position = startPosition.position + Vector3.right * 16 + Vector3.up * 5;

            RaycastHit2D onGround = Physics2D.Raycast(this.transform.position, Vector3.down, 30, ground);
            this.transform.position += Vector3.down * (onGround.distance-0.5f);

            //TODO: Add fall down to the pig to make it start on the floor
        }
    }

    void DeadState()
    {
        //Be dead
    }

    void AliveState()
    {

        Vector3 move = new Vector3(forwardMovementSpeed, 0, 0);

        character.Move(move * Time.deltaTime * speed);

        hasHitObstacle = Physics2D.CircleCast(obsHitPosition.position, obsCheckRadius, obsHitPosition.position - this.transform.position, 0, obstacle);

        if (hasHitObstacle)
        {
            //Launch game over
            isAlive = false;
            GSM.SetGameState(GameState.GAME_OVER);
            screenManager.OpenPanel(GameOverUI);
        }

        isGroundedNow = CheckIfGrounded();
        isGroundedForJump = Physics2D.BoxCast(groundChecker.transform.position, checkSize, 0, Vector2.down, 0.05f, ground);
        oldVelocity = velocity;
        oldPosition = this.transform.position;
        velocity.y += gravity * Time.deltaTime;

        if (isGroundedNow && !isGrounded)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, Vector2.down, 10, ground);
            if (rayHit)
            {
                this.transform.position += new Vector3(0, 0.5f - rayHit.distance, 0);
            }
        }


        if (isGrounded)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, Vector2.down, 10, ground);
            if (rayHit)
            {
                this.transform.position += new Vector3(0, (0.5f - rayHit.distance)*Time.deltaTime * riseOutOfTheGroundSpeed, 0);
            }
        }

        isGrounded = isGroundedNow;

        if (isGrounded && velocity.y < 0)
        {
            Land();
        }



        if (GetJumpInput(isGroundedForJump, hasJumped))
        {
            Jump();
        }
        else if (GetJumpInput(isGroundedForJump, hasJumped) && !isGroundedForJump)
        {
            velocity.y *= 0.5f;
            velocity.x *= 0.5f;
            hasJumped = false;
        }
        
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && hasJumped)
        {
            hasJumped = false;
        }
        else if (!Input.GetButton("Jump") && hasJumped)
        {
            hasJumped = false;
        }

        velocity.x /= 1 + drag.x * Time.deltaTime;
        velocity.y /= 1 + drag.y * Time.deltaTime;

        character.Move(velocity);
    }


    float timeTillJump = 2;
    float timeToWait = 1;
    void MenuState()
    {
        isGroundedNow = CheckIfGrounded();

        oldVelocity = velocity;
        oldPosition = this.transform.position;
        velocity.y += gravity * Time.deltaTime;

        if(isGroundedNow && !isGrounded)
        {
            timeTillJump = timeToWait;
            RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, Vector2.down, 10, ground);
            if (rayHit)
            {
                this.transform.position += new Vector3(0, 0.5f - rayHit.distance, 0);
            }
        }
        isGrounded = isGroundedNow;
        if (isGrounded && velocity.y < 0)
        {
            Land();
        }

        if (isGrounded && timeTillJump < 0)
        {
            Jump(false);
        }else if(timeTillJump > 0)
        {
            timeTillJump -= Time.deltaTime;
        }

        character.Move(velocity);
    }

    bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(groundChecker.transform.position, checkSize, 0, Vector2.up, groundDistance, ground);
    }

    void Jump(bool forward = true)
    {
        velocity.y += Mathf.Sqrt(jumpHeight * -0.2f * gravity);
        if (forward)
        {
            velocity.x += Mathf.Sqrt(jumpDistance * -0.2f * gravity);
        }
        if(velocity.y > 0.3f)
        {
            velocity.y = 0.3f;
        }
        if(velocity.x > 0.1f)
        {
            velocity.x = 0.1f;
        }
        //isGroundedText.text = velocity.ToString();
        animator.SetBool(jumpingHash, true);
        if (forward)
        {
            hasJumped = true;
        }
    }

    void Land()
    {
        velocity.y = 0f;
        velocity.x = 0f;
        animator.SetBool(jumpingHash, false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector3.down);
        Gizmos.DrawWireCube(groundChecker.transform.position, checkSize);
        //RaycastHit2D[] hits = Physics2D.RaycastAll(groundChecker.transform.position, Vector2.down, groundDistance, ground);
        //if(hits.Length > 0)
        //{
        //    Debug.Log(Vector3.Distance(hits[0].point, groundChecker.transform.position));
        //}
    }

    bool GetJumpInput(bool isGrounded, bool hasJumped)
    {
        if((Input.GetButton("Jump") || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Stationary)) && isGrounded && !hasJumped)
        {
            return true;
        }
        else if(!Input.GetButton("Jump") || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
        {
            return false;
        }
        else
        {
            return false;
        }
    }
}
