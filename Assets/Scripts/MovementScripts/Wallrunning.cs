using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;

    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;
    public KeyCode jumpKey = KeyCode.Space;
    private bool exitingWallrun;
    public float exitWallrunTime;
    public float forceExitWallrunTime;
    private float exitWallrunTimer;

    [Header("Walljump")]
    public float wallJumpUpForce;
    public float wallJumpSideForce;


    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    public PlayerCam playerCamera;
    private BasicMovement bm;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bm = GetComponent<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (bm.wallrunning)
        {
            WallrunningMovement();
        }
    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
    }

    private bool NotGrounded()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if((wallLeft || wallRight) && verticalInput > 0 && NotGrounded() && !exitingWallrun)
        {
            if (!bm.wallrunning)
            {
                StartWallrun();
            }
            if (wallRunTimer > 0)
            {
                wallRunTimer -= Time.deltaTime;
            }
            if (wallRunTimer <= 0 && bm.wallrunning)
            {
                exitingWallrun = true;
                exitWallrunTimer = forceExitWallrunTime;
            }
            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
        else if (exitingWallrun)
        {
            if (bm.wallrunning)
            {
                StopWallrun();
            }
            if (exitWallrunTimer > 0)
            {
                exitWallrunTimer -= Time.deltaTime;
            }
            if (exitWallrunTimer <- 0)
            {
                exitingWallrun = false;
            }
        }
        else
        {
            if (bm.wallrunning)
            {
                StopWallrun();
            }
        }
    }

    private void StartWallrun()
    {
        bm.wallrunning = true;
        wallRunTimer = maxWallRunTime;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        playerCamera.DoFov(90f);
        if(wallLeft)
        {
            playerCamera.DoTilt(-5f);
        }
        if(wallRight)
        {
            playerCamera.DoTilt(5f);
        }
    }

    private void WallrunningMovement()
    {
        rb.useGravity = useGravity;
        

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        if (useGravity)
        {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
    }
    
    public void StopWallrun()
    {
        bm.wallrunning = false;
        playerCamera.DoFov(80f);
        playerCamera.DoTilt(0f);
    }

    private void WallJump()
    {
        exitingWallrun = true;
        exitWallrunTimer = exitWallrunTime;
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 jumpForce = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(jumpForce, ForceMode.Impulse);
    }
}
