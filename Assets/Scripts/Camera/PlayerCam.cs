using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private GameObject playerGO;

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

        if (!playerGO == false)
        {
            playerTransform = playerGO.GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
        
    }
    private void RotateCharacter()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    private void CenterCamera()
    {

    }
}
