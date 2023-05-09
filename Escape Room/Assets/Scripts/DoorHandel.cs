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

            //we get the vector that goes from this to the interactor
            Vector3 selfToInteractor = interactorTransform.position - transform.position;

            //project onto the movement vector
            float forceInDirectionOfDrag = Vector3.Dot(selfToInteractor, m_WorldDragDirection);

            //we then need to check in which direction are we dragging : toward the end (positive direction) or toward
            //the start (megative direction)
            bool dragToEnd = forceInDirectionOfDrag > 0.0f;

            //we take the absolute of that value now, as we need a speed, not a direction anymore
            float absoluteForce = Mathf.Abs(forceInDirectionOfDrag);

            //we transform our force into a speed (by dividing it by delta Time). Then we "scale" that speed by the door
            //weight. The "heavier" the door, the lower the speed will be.
            float speed = absoluteForce / Time.deltaTime / doorWeight;

            //finally we move the target either toward end or start based on the speed.
            draggedTransform.position = Vector3.MoveTowards(draggedTransform.position,
                //the target depend on the direction of drag we recovered earlier
                dragToEnd ? m_EndPosition : m_StartPosition,
                speed * Time.deltaTime);

        }
    }
}
