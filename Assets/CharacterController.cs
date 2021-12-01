using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody myRigidbody;

    public float maxSpeed;
    public float playerSpeed = 3.0f;
    public float sprintSpeed = 17.0f;
    public float maxSprint = 5.0f;
    float sprintTimer;

    public CapsuleCollider playerController;
    bool crouching = false;
    public float standingHeight = 2.0f;
    public float crouchingHeight = 0.3f;

    public float rotation = 0.0f;
    public float camRotation = 0.0f;
    public float rotationSpeed = 2.0f;
    public float camRotationSpeed = 1.5f;
    GameObject cam;

    bool isOnGround;
    bool doubleJump;
    public GameObject groundChecker;
    public LayerMask groundLayer;

    public float jumpForce = 300.0f;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();

        sprintTimer = maxSprint;

        myAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement mechanics
        Vector3 forwardsVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;

        Vector3 sidewaysVelocity = transform.right * Input.GetAxis("Horizontal") * maxSpeed;

        Vector3 finalVelocity = forwardsVelocity + sidewaysVelocity;

        myRigidbody.velocity = new Vector3(finalVelocity.x, myRigidbody.velocity.y, finalVelocity.z);
        myAnim.SetFloat("speed", finalVelocity.magnitude);

        //sprint and stamina
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0.0f)
        {
            maxSpeed = sprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        }
        else
        {
            maxSpeed = playerSpeed;
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                sprintTimer = sprintTimer + Time.deltaTime;
            }
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

        //rotation mechanics
        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
        //                                               (  X ,    Y    ,  Z  )

        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(-camRotation, 0.0f, 0.0f));

        //jump mechanics
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("jumped");
            myRigidbody.AddForce(transform.up * jumpForce);
        }

        if (isOnGround == false && Input.GetKeyDown(KeyCode.Space) && doubleJump == false)
        {
            myAnim.SetTrigger("double jumped");
            myRigidbody.AddForce(transform.up * jumpForce);
            doubleJump = true;
        }

        if (isOnGround == true)
        {
            doubleJump = false;
        }

        //crouch mechanic
        playerController = GetComponent<CapsuleCollider>();

        if (Input.GetKeyDown(KeyCode.C) && isOnGround == true)
        {
            if (crouching == false)
            {
                playerController.height = crouchingHeight;
                crouching = true;
            } else
            {
                playerController.height = standingHeight;
                crouching = false;
            }
        }

        //falling animation
        if (!isOnGround && Input.GetKey(KeyCode.None))
        {
            myAnim.SetBool("falling", true);
        }
        else
        {
            myAnim.SetBool("falling", false);
        }

    }
}
