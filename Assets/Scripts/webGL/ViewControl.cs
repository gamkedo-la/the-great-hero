using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ViewControl : MonoBehaviour {
    [SerializeField]
    private float lookAngLimit = 45.0f;

    public static ViewControl instance;

    // Use this for initialization
    void Start () {
        instance = this;
    }

	// Update is called once per frame
	void Update () {
        if(Cursor.lockState != CursorLockMode.Locked) {
			return;
		}

        float angleBefore = transform.rotation.eulerAngles.x;
        float angleMoveBy = Time.deltaTime * -120.0f * Input.GetAxis("Mouse Y");
        float angleAfter = angleBefore + angleMoveBy;
        if (angleAfter > 180.0f)
        {
            angleAfter = angleAfter - 360.0f;
            if (angleAfter < -lookAngLimit)
            {
                angleMoveBy = (-lookAngLimit) - angleBefore;
            }
        }
        else
        {
            if (angleAfter > lookAngLimit)
            {
                angleMoveBy = lookAngLimit - angleBefore;
            }
        }
        transform.Rotate(Vector3.right, angleMoveBy);
	
	}

}
