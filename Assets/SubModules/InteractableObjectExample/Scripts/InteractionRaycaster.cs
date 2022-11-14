using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionRaycaster : MonoBehaviour
{
    [SerializeField] LayerMask raycastableLayers;
    [SerializeField] float raycastDistance;
    [SerializeField] Camera raycastCamera;

    [SerializeField] Image crosshair;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] Color idleColor;
    [SerializeField] Color interactableColor;
    [SerializeField] Color notInteractableColor;

    private Vector3 centerScreen = new Vector3(0.5f, 0.5f, 0);

    Coroutine raycastRoutine;

    Ray ray;
    RaycastHit hit;
    RaycastInteractable interactable = null;

    private void Awake()
    {
        crosshair.color = idleColor;
    }


    // Update is called once per frame
    void Update()
    {
        ray = raycastCamera.ViewportPointToRay(centerScreen);


        if (Physics.Raycast(ray, out hit, raycastDistance, raycastableLayers))
        {
            if (interactable != null && interactable.gameObject.GetInstanceID() != hit.transform.gameObject.GetInstanceID())
            {
                interactable.OnHoverExit();
                interactable = hit.transform.gameObject.GetComponent<RaycastInteractable>();
                if (interactable != null) 
                {
                    interactable.OnHoverEnter();
                    text.enabled = true;
                }
            }
            else if(interactable == null)
            {
                interactable = hit.transform.gameObject.GetComponent<RaycastInteractable>();
                if(interactable)
                {
                    interactable.OnHoverEnter();
                    text.enabled = true;
                }
                else
                {
                    crosshair.color = idleColor;
                    text.enabled = false;
                    return;
                }
                
            }

            text.text = interactable.HoverString;
            if (interactable.Interactable)
            {
                crosshair.color = interactableColor;

                if(Input.GetButtonDown("Fire1"))
                {
                    interactable.OnInteract();
                }
            }
            else
            {
                crosshair.color = notInteractableColor;
            }
        }
        else if (interactable != null)
        {
            interactable.OnHoverExit();
            crosshair.color = idleColor;
            text.enabled = false;
            interactable = null;
        }
    }
}
