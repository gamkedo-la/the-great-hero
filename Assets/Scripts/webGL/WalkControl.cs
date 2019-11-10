using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WalkControl : MonoBehaviour {
	[HideInInspector]
    public Rigidbody rb;
	private bool onGround=true;
    private Vector3 prevValidPosition;
    public float jumpForce = 5.0f;
    public float walkSpeed = 6.0f;
    public float strafeSpeed = 4.0f;
    public float speedFalloffAmt = 0.9f; // friction only for lateral motion

    public float suchLowYMustHaveFallenThroughFloor = -150.0f;
    Vector3 lastKnownSafelyOnGround = Vector3.zero;

	private Vector3 forward, right;

    //private float powerUp = 1.0f;

    public static WalkControl instance;

	// Use this for initialization
	void Start () {
        lastKnownSafelyOnGround = transform.position;
        instance = this;
        rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate()
	{
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/


        if (Cursor.lockState == CursorLockMode.Locked)
        {
			forward = transform.forward;
			right = transform.right;
            Vector3 lateralDecay = rb.velocity;
            lateralDecay.x *= speedFalloffAmt;
            lateralDecay.z *= speedFalloffAmt;
            rb.velocity = lateralDecay;
            float scaleForCompatibilityWithOlderTuning = 4.0f; // added to keep pre-physics walk tuning numbers
            rb.velocity += forward * Time.deltaTime * walkSpeed * scaleForCompatibilityWithOlderTuning *
                Input.GetAxisRaw("Vertical");
            rb.velocity += right * Time.deltaTime * strafeSpeed * scaleForCompatibilityWithOlderTuning *
                Input.GetAxisRaw("Horizontal");

            transform.Rotate(Vector3.up, Time.deltaTime * 165.0f * Input.GetAxis("Mouse X"));

        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
	}

	// Update is called once per frame
	void Update () {
        RaycastHit rhInfo;

        prevValidPosition = transform.position;

        if (onGround && Input.GetButtonDown("Jump"))
        {
            onGround = false;
            rb.velocity += Vector3.up * jumpForce;
        }

        int layerMask = ~LayerMask.GetMask("Ignore Raycast");
        if (Physics.Raycast(transform.position, Vector3.down, out rhInfo, 1.2f, layerMask))
        {
			if (rhInfo.collider != null)
			{
                if (onGround == false && rb.velocity.y < 0.0f)
                {
                    // FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MainHub/JumpLand", gameObject);
                }

                // Debug.Log("standing on " + rhInfo.collider.name);
				lastKnownSafelyOnGround = transform.position;
				forward = Vector3.Cross(transform.right, rhInfo.normal).normalized;
				right = Vector3.Cross(-transform.forward, rhInfo.normal).normalized;
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.1f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 01f &&
                    Input.GetButton("Jump") == false)
				{
					transform.position = new Vector3(rhInfo.point.x, rhInfo.point.y + 1.045f, rhInfo.point.z);
					rb.velocity = Vector3.zero;
				}
                onGround = true;
			}
        }
        else
        {
            onGround = false;
            if (transform.position.y < suchLowYMustHaveFallenThroughFloor)
            {
                Debug.Log("Fell through or off world edge, resetting to last ground touch");
                Debug.Log("If this shouldn't have happened or fell too far, set lastKnownSafelyOnGround");
                transform.position = lastKnownSafelyOnGround;
            }
        }
    }
}
