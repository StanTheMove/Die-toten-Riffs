using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationScript : MonoBehaviour
{

    public Transform InteractionSource;
    public float IntractionRange;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractionSource.position, InteractionSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, IntractionRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}

interface Interactable
{
    public void Interact();

}