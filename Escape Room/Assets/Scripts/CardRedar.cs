using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CardRedar : XRSocketInteractor
{
    [SerializeField] private GameObject locker;
    [SerializeField] private Collider doorHandle;
    [SerializeField] private Transform keyCard;

    private Vector3 cardEnterPos;
    private Vector3 cardExitPos;
    private float dotProdectFowrord;
    private float dotProdectUp;
    private bool canCheck;
    
    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractor(updatePhase);

        if (canCheck)
        {
            dotProdectFowrord = Vector3.Dot(Vector3.left, keyCard.right);
            dotProdectUp = Vector3.Dot(Vector3.up, keyCard.forward);
            
            if (dotProdectFowrord < 0.95f || dotProdectUp < 0.95f)
            {
                canCheck = false;
            }
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        CardEntered();
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        CardExited();
    }

    private void CardEntered()
    {
        cardEnterPos = keyCard.position;
        canCheck = true;

    }

    private void CardExited()
    {
        cardExitPos = keyCard.position;
        if (canCheck)
        {
            Vector3 _diff = cardEnterPos - cardExitPos;
            if (_diff.y > 0.15f)
            {
                locker.SetActive(false);
                doorHandle.enabled = true;
            }
        }
    }
    
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return false;
    }
}
