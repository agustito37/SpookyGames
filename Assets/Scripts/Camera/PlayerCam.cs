using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCam : MonoBehaviour
{
    private GameObject playerGO;
    private bool stopInput;

    public float sensX;
    public float sensY;

    public GameObject ViewCamera = null;
    public float mSpeed = 5f;

    public Transform orientation;
    private Transform playerTransform;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        if (playerGO != null)
        {
            playerTransform = playerGO.GetComponent<Transform>();
        }
    }
    private void FixedUpdate()
    {

        TestTools();
    }

    // Update is called once per frame
    void Update()
    {

        if (!stopInput)
        {
            RotateCharacter();
        }
    }
    private void RotateCharacter()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    private void TestTools()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            stopInput = stopInput ? false : true;
        }
    }

    private void CenterCamera()
    {

    }
}
