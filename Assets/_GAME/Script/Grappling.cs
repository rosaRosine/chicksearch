using UnityEngine;
using UnityEngine.InputSystem;


public class Grappling : MonoBehaviour
{
    [Header("References")]
    public Transform gunTip, cam;
    private PlayerController pm;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerController>();
    }

    private void Update()
    {

        if(grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }

    public void StartGrapple(InputAction.CallbackContext context)
    {
        if(grapplingCdTimer > 0) return;

        if(!context.performed) return;
        grappling = true;
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);

        Debug.Log("Grapple started");
    }

    private void ExecuteGrapple()
    {
        
    }

    private void StopGrapple()
    {
        grappling = false;
        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }
}
