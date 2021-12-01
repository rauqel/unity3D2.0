using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingplatform : MonoBehaviour
{
    public Transform myPlatform;
    public Transform myStartPoint;
    public Transform myEndPoint;

    public float platformSpeed;

    bool isReversing = false;
    public bool isMoving;
    public triggerplatform trigger;

    // Start is called before the first frame update
    void Start()
    {
        myPlatform.position = myStartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            trigger.isActive = true;

            if (isReversing == false)
            {
                //                                                                                  platformSpeed defines the speed of the platforms movement
                myPlatform.position = Vector3.MoveTowards(myPlatform.position, myEndPoint.position, platformSpeed);
                if (myPlatform.position == myEndPoint.position)
                {
                    isReversing = true;
                }
            }
            else
            {
                trigger.isActive = false;

                myPlatform.position = Vector3.MoveTowards(myPlatform.position, myStartPoint.position, platformSpeed);
                if (myPlatform.position == myStartPoint.position)
                {
                    isReversing = false;
                }
            }
        }
    }
}
