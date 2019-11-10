using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreObj : MonoBehaviour
{
    private Quaternion oldRot;
    private Vector3 oldPos;
    private Rigidbody rb;
    public OVRInput.Button whichButton;
    public KeyCode orKey;

    // Start is called before the first frame update
    void Start()
    {
        oldRot = transform.rotation;
        oldPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(orKey) || OVRInput.GetDown(whichButton))
        {
            transform.rotation = oldRot;
            transform.position = oldPos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
