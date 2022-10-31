using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastInteractable : MonoBehaviour
{
    [SerializeField] string hoverString;
    [SerializeField] private bool interactable;

    [SerializeField] UnityEvent onHoverEnter;
    [SerializeField] UnityEvent onHoverExit;
    [SerializeField] UnityEvent onInteract;



    public void OnHoverEnter()
    {
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        onHoverExit?.Invoke();
    }

    public void OnInteract()
    {
        onInteract?.Invoke();
    }

    public bool Interactable { get => interactable; set => interactable = value; }
    public string HoverString { get => hoverString; }
}
