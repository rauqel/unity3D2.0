using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sticktoplatform : MonoBehaviour
{
    bool isOnMPlatform;
    public GameObject groundChecker;
    public LayerMask mPlatformLayer;

    void Update()
    {
        isOnMPlatform = Physics.CheckSphere(groundChecker.transform.position, 0.1f, mPlatformLayer);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && isOnMPlatform == true)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
