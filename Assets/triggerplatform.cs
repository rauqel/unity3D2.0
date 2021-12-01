using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerplatform : MonoBehaviour
{
    public Movingplatform platform;
    float timer = 5;
    public bool isActive;

    private void OnTriggerEnter(Collider collision)
    {
        if (!isActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                platform.isMoving = true;
                isActive = true;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                platform.isMoving = false;
                isActive = false;
            }
        }
    }
}