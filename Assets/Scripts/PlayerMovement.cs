using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float groundSideMovementSpeed;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * groundSideMovementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.left * groundSideMovementSpeed * Time.deltaTime * -1);
        }
        Movement();
    }

    private void Movement()
    {
       
    }

}
