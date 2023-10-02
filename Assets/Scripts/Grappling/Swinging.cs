using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("Swinging")]
    public float maxSwingDistance;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    [Header("Joint settings")]
    public float jointMaxDistance;
    public float jointMinDistance;
    public float jointSpring;
    public float jointDamper;
    public float jointMassScale;

    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip;
    public Transform playerCamera;
    public Transform player;
    private BasicMovement bm;
    public LayerMask whatIsSwingable;
    // Start is called before the first frame update
    void Start()
    {
        bm = GetComponent<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(swingKey))
        {
            StartSwing();
        }
        if (Input.GetKeyUp(swingKey))
        {
            StopSwing();
        }

        CheckForSwingPoints();
    }

    private void LateUpdate() 
    {
        DrawRope();
    }

    private void StartSwing()
    {
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }

        bm.swinging = true;

        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

        joint.maxDistance = distanceFromPoint * jointMaxDistance;
        joint.minDistance = distanceFromPoint * jointMinDistance;

        joint.spring = jointSpring;
        joint.damper = jointDamper;
        joint.massScale = jointMassScale; 

        lr.positionCount = 2;
        lr.enabled = true;
    }

    private void StopSwing()
    {
        bm.swinging = false;
        lr.positionCount = 0;
        lr.enabled = false;
        Destroy(joint);
    }

    private void DrawRope()
    {
        if (!joint)
        {
            return;
        }

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    public void CheckForSwingPoints()
    {
        if (joint != null)
        {
            return;
        }

        RaycastHit sphereCastHit;
        Physics.SphereCast(playerCamera.position, predictionSphereCastRadius, playerCamera.forward, out sphereCastHit, maxSwingDistance, whatIsSwingable);

        RaycastHit raycastHit;
        Physics.Raycast(playerCamera.position, playerCamera.forward, out raycastHit, maxSwingDistance, whatIsSwingable);

        Vector3 realHitPoint;

        // Option 1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;

        // Option 2 - Indirect (predicted) Hit
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;

        // Option 3 - Miss
        else
            realHitPoint = Vector3.zero;

        // realHitPoint found
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        // realHitPoint not found
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
        
    }
}
