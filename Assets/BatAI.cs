using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    public float batSpeed = 5f;
    public float batHoverAmt = 0.3f;
    public float batHoverSpeed = 4.0f;
    public float batHoverDist = 3.0f;
    public float batStealTime = 2.0f;

    //Doing State with Primitives First, could refactor to Enumerators later
    private const int B_State_Approach = 0;
    private const int B_State_Stealing = 1;
    private const int B_State_Retreat = 2;
    private int stateNow = B_State_Approach;
    private Vector3 stealPos = Vector3.zero;
    private float stealStartTime = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stateNow == B_State_Approach)
        {
            transform.LookAt(FromAnywhereSingleton.instance.transform.position);

            transform.position += transform.forward * Time.deltaTime * batSpeed;

            if(Vector3.Distance(transform.position, FromAnywhereSingleton.instance.transform.position) < batHoverDist)
            {
                stateNow++;
                stealPos = transform.position;

                stealStartTime = Time.timeSinceLevelLoad;
            }
        }
        else if(stateNow == B_State_Stealing)
        {
            float timeStealing = Time.timeSinceLevelLoad - stealStartTime;
            transform.position = stealPos + batHoverAmt * Vector3.up * Mathf.Cos(timeStealing * batHoverSpeed);

            if(timeStealing > batStealTime)
            {
                stateNow++;
            }
        }
        else if(stateNow == B_State_Retreat)
        {
            transform.LookAt(FromAnywhereSingleton.instance.transform.position);

            transform.position -= transform.forward * Time.deltaTime * batSpeed;
        }

    }
}
