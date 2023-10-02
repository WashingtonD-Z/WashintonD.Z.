using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("Grappling")]
    private bool grappling;
    public float maxGrappleDistance;
    public float grappleDelayTime;
    private Vector3 grapplePoint;
    public float overshootYAxis;

    [Header("Cooldown")]
    public float grappleCooldown;
    private float cooldownTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    [Header("References")]
    private BasicMovement bm;
    public Transform playerCamera;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        bm = GetComponent<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }
        if (cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate() 
    {
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }
    private void StartGrapple()
    {
        if (cooldownTimer > 0)
        {
            return;
        }

        grappling = true;
        
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            bm.freeze = true;
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = playerCamera.position + playerCamera.forward * maxGrappleDistance;

            Invoke(nameof(EndGrapple), grappleDelayTime);
        }   

        lr.enabled = true;
        lr.SetPosition(1,grapplePoint);
    }

    private void ExecuteGrapple()
    {
        bm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        bm.GrappleToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(EndGrapple), 1f);
    }

    public void EndGrapple()
    {
        grappling = false;
        bm.freeze = false;
        cooldownTimer = grappleCooldown;

        lr.enabled = false;
    }
    
}
