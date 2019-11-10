using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunForce : MonoBehaviour
{
    public OVRInput.Button whichButton;
    public KeyCode orKey;
    public GameObject hitParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(orKey) || OVRInput.GetDown(whichButton))
        {
            RaycastHit rhInfo;
            if(Physics.Raycast(transform.position, transform.forward, out rhInfo)) {
                Rigidbody hitRB = rhInfo.collider.gameObject.GetComponent<Rigidbody>();
                if(hitRB)
                {
                    hitRB.AddForceAtPosition(transform.forward * 100.0f, rhInfo.point);
                }
                GameObject.Instantiate(hitParticle, rhInfo.point, Quaternion.LookRotation(rhInfo.normal));
            }
        }
    }
}
