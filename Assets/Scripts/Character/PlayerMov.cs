using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private Rigidbody mRigidBody = null;
    private Animator mAnimator;
    public float mSpeed = 5f;

    private bool jumping;
    private bool stopInput;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mRigidBody = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        TestTools();

        if (!stopInput)
        {
            if (mRigidBody.velocity.y == 0)
            {
                jumping = false;
            }

            MovePlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //if (grounded)
        //{
        //    rb.drag = groundDrag;
        //} else
        //{
        //    rb.drag = 0;
        //}

        if (!stopInput)
        {
            MyInput();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        bool verticalValue = verticalInput > 0f || verticalInput < 0f;
        bool horizontalValue = horizontalInput > 0f || horizontalInput < 0f;

        if (verticalValue || horizontalValue)
        {
            mAnimator.SetBool("isMoving", true);
        }
        else if (!verticalValue && !horizontalValue)
        {
            mAnimator.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumping = true;
            mRigidBody.AddForce(Vector3.up * 500);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void TestTools()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            stopInput = stopInput ? false : true;
            Cursor.lockState = stopInput ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = stopInput ? true : false;
        }
    }

}
