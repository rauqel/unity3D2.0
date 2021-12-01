using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public Transform myPickup;
    public Transform myStartPoint;
    public Transform myEndPoint;

    public float upSpeed = 0.00175f;
    public float downSpeed = 0.0015f;

    bool isReversing = false;

    private void OnTriggerEnter(Collider other)
    {
        coincount.scoreValue += 1;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        myPickup.position = myStartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReversing == false)
        {
            myPickup.position = Vector3.MoveTowards(myPickup.position, myEndPoint.position, upSpeed);
            if (myPickup.position == myEndPoint.position)
            {
                isReversing = true;
            }
        }
        else
        {
            myPickup.position = Vector3.MoveTowards(myPickup.position, myStartPoint.position, downSpeed);
            if (myPickup.position == myStartPoint.position)
            {
                isReversing = false;
            }
        }
    }
}