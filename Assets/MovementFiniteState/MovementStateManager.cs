using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float walkspeed = 5f;
    public float crouchSpeed = 3f;
    public float airSpeed = 1.5f;
    Vector3 airDir;
    [HideInInspector] public Vector3 dir;
    float Xinput, Yinput;
    CharacterController controllerToken;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    public bool isGround;
    Vector3 spherePos;

    [SerializeField] float gravity = 9.81f;
    float jumpForce = 5f;
    public bool onJumping;
    Vector3 velocity;

    public MovementBaseState currentState;
    public IdleState idleToken = new IdleState();
    public RunState runToken = new RunState();
    public WalkState walkToken = new WalkState();
    public CrouchState crouchToken = new CrouchState();
    public JumpState jumpToken = new JumpState();
    public Animator animToken;
    // Start is called before the first frame update
    void Start()
    {
        animToken = GetComponent<Animator>();
        controllerToken = GetComponent<CharacterController>();
        SwitchState(idleToken);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectional();
        Move();
        IsGrounded();
        Gravity();        
        currentState.UpdateState(this);
    }
    public void SwitchState(MovementBaseState state) 
    {
        currentState = state;
        currentState.EnterState(this);
    }
    void GetDirectional() 
    {
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");
        animToken.SetFloat("Xinput", Xinput);
        animToken.SetFloat("Yinput", Yinput);
        airDir = Vector3.zero;
        if (!isGround) airDir = transform.forward * Yinput + transform.right * Xinput;
        else dir = transform.forward * Yinput + transform.right * Xinput;
    }
    void Move()
    {
        controllerToken.Move(((Vector3.ClampMagnitude(dir, 1.0f) * moveSpeed)+(Vector3.ClampMagnitude(airDir, 1.0f) * airSpeed)) * Time.deltaTime);
    }

    void IsGrounded() 
    {
        spherePos = new Vector3(transform.position.x,transform.position.y - groundYOffset,transform.position.z);
        isGround = Physics.CheckSphere(spherePos,controllerToken.radius-0.05f,groundMask);
        animToken.SetBool("OnGround",isGround);
    }

    void Gravity() 
    {
        if (!isGround)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2f;
        }
        controllerToken.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (controllerToken)
        {
            Gizmos.DrawWireSphere(spherePos, controllerToken.radius - 0.05f);
        }
    }

    public void AddJumpForce() => velocity.y += jumpForce;
    public void Jumped() => onJumping = true;
}
