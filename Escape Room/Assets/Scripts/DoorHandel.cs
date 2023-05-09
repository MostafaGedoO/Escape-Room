using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandel : XRBaseInteractable
{
    public Transform draggedTransform; // set to parent door object
    public Vector3 localDragDirection; // set to -1, 0, 0
    public float dragDistance; // set to 0.8
    public int doorWeight = 20;
    
    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;
    private Vector3 m_WorldDragDirection;
    
    private void Start()
    {
        m_WorldDragDirection = transform.TransformDirection(localDragDirection).normalized;

        m_StartPosition = draggedTransform.position;
        m_EndPosition = m_StartPosition + m_WorldDragDirection * dragDistance;
    }
    

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed && isSelected)
        {
            var interactorTransform = firstInteractorSelecting.GetAttachTransform(this);

            Vector3 selfToInteractor = interactorTransform.position - transform.position;

            float forceInDirectionOfDrag = Vector3.Dot(selfToInteractor, m_WorldDragDirection);

            bool dragToEnd = forceInDirectionOfDrag > 0.0f;

            float absoluteForce = Mathf.Abs(forceInDirectionOfDrag);

            float speed = absoluteForce / Time.deltaTime / doorWeight;

            draggedTransform.position = Vector3.MoveTowards(draggedTransform.position, dragToEnd ? m_EndPosition : m_StartPosition, speed * Time.deltaTime);

        }
    }
}
