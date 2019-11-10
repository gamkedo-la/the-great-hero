using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportCane : MonoBehaviour
{
    public OVRInput.Button whichButton;
    public KeyCode orKey;
    public GameObject teleportCursor;
    public Transform playerBody;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rhInfo;
        bool foundDest = false;

        if (Physics.Raycast(transform.position, transform.up, out rhInfo))
        {
            Rigidbody hitRB = rhInfo.collider.gameObject.GetComponent<Rigidbody>();
            if(hitRB == null)
            {
                foundDest = true;
                teleportCursor.transform.position = rhInfo.point + Vector3.up * 0.5f;
                if (Input.GetKeyDown(orKey) || OVRInput.GetDown(whichButton))
                {
                    playerBody.position = teleportCursor.transform.position;
                }
            }
        }

        if (teleportCursor.activeSelf != foundDest)
        {
            teleportCursor.SetActive(foundDest);
        }
    }
}
