using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private ObjectGrabbable objectGrabbable;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // Enter grabbing mode
            if(objectGrabbable == null)
            {
                // Not carrying an object, try to grab
                float pickupDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            // Exit grabbing mode
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
            
        }
        // When grabbing an object
        if(objectGrabbable != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                objectGrabbable.Throw();
            }
        }
    }
}
