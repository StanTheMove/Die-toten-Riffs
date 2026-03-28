using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Interactable
{
    public void Interact();

}

public class InterationScript : MonoBehaviour
{

    public Transform InteractionSource;
    public float IntractionRange;

    void Start()
    {

    }

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