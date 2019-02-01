﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorSelectionMethod
{
    AUTOMATIC_BYCONTACT = 0,
    KEYBOARD_SPACEBAR,
    MOUSE_LEFTCLICK
}

public abstract class CursorPositioningController : MonoBehaviour
{
    public abstract Vector3 GetCurrentCursorPosition();
}

public class CursorInteractorBehaviour : CursorBehaviour
{
    public CursorPositioningController cursorPositionController;
    public CursorSelectionMethod selectionMethod = CursorSelectionMethod.AUTOMATIC_BYCONTACT;
   
    TargetBehaviour currentHighlightedTarget;

    TargetBehaviour currentAcquiredTarget;

    void Update()
    {
        this.transform.position = cursorPositionController.GetCurrentCursorPosition();

        switch (selectionMethod)
        {
            case CursorSelectionMethod.KEYBOARD_SPACEBAR:
                CheckSpaceBarSelection();
                break;
            case CursorSelectionMethod.MOUSE_LEFTCLICK:
                CheckMouseLeftClickSelection();
                break;
            case CursorSelectionMethod.AUTOMATIC_BYCONTACT:
            default:
                CheckAutomaticByContactSelection();
                break;
        }  
    }

    void CheckSpaceBarSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AcquireTarget(currentHighlightedTarget);
        } 
    }

    void CheckMouseLeftClickSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AcquireTarget(currentHighlightedTarget);
        }
    }

    void CheckAutomaticByContactSelection()
    {
        if (currentHighlightedTarget != null)
        {
            if (currentAcquiredTarget == null)
            {
                AcquireTarget(currentHighlightedTarget);
                currentAcquiredTarget = currentHighlightedTarget;
                Debug.Log("AUTOMATIC SELECTION Acquired");
            }
            if (currentHighlightedTarget != currentAcquiredTarget)
            {            
                AcquireTarget(currentHighlightedTarget);
                currentAcquiredTarget = currentHighlightedTarget;
                Debug.Log("AUTOMATIC SELECTION Acquired");
            }  
        }
        else
        {
            if (currentAcquiredTarget != null)
            {
                Vector3 cursorTargetDistance = currentAcquiredTarget.position - GetCursorPosition();
                if (cursorTargetDistance.magnitude > currentAcquiredTarget.localScale.magnitude)
                {
                    currentAcquiredTarget = null;
                    Debug.Log("AUTOMATIC SELECTION Released");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var targetBehaviour = other.GetComponent<TargetBehaviour>();
        if (targetBehaviour != null)
        {
            EnterTarget(targetBehaviour);
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        var targetBehaviour = other.GetComponent<TargetBehaviour>();
        if (targetBehaviour != null)
        {
            ExitTarget(targetBehaviour);
        }
    }

    public override void EnterTarget(TargetBehaviour target)
    {
        target.HighlightTarget();
        currentHighlightedTarget = target;
        Debug.Log("TargetEnter");
        if (listener != null)
        {
            listener.CursorEnteredTarget(target);         
        }
    }

    public override void ExitTarget(TargetBehaviour target)
    {
        target.UnhighlightTarget();
        currentHighlightedTarget = null;
        Debug.Log("TargetExit");
        if (listener != null)
        {
            listener.CursorExitedTarget(target);
        }
    }

    public override void AcquireTarget(TargetBehaviour target)
    {
        if (listener != null)
        {
            listener.CursorAcquiredTarget(target);
        }

#if DEBUG
        if (target != null)
        {
            Debug.Log("Acquired target: " + target.name);
        }
        else
        {
            Debug.Log("Acquired target: none");
        }
#endif
    }

    public override Vector3 GetCursorPosition()
    {
        return this.transform.position;
    }
}
