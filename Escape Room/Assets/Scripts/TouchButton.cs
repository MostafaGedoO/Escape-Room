using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchButton : XRBaseInteractable
{
    [SerializeField] private Material highLightMat;
    [SerializeField] private string buttonNum;

    private Material originalMat;
    private Renderer renderer;
    
    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
        originalMat = renderer.material;
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        renderer.material = highLightMat;
        NumberPad.Instance.SetNumber(buttonNum);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        renderer.material = originalMat;
    }
}
