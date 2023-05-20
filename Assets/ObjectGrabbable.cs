using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{

    public Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    [SerializeField] Transform cameraTransform;
    //public float rotationSpeedX = 10f;
    //public float rotationSpeedY = 10f;

    public Vector2 turn;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
    }

    public void Throw()
    {
        objectRigidbody.isKinematic = false;
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.AddForce(cameraTransform.forward * 500f);
    }

    private void FixedUpdate()
    {
        if(objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            //if (Input.GetKey(KeyCode.R))
            //{
            //    {
            //        float mouseX = Input.GetAxis("Mouse X") * rotationSpeedX;
            //        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeedY;
            //        transform.Rotate(new Vector3(-mouseY, mouseX, 0));
            //    }
            //}

            objectRigidbody.MovePosition(newPosition);
        }
    }
}
