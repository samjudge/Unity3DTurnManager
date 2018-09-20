using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/**
 * @class OnMouseDragPlotMovementHandler
 * @extends MonoBehaviour
 * Detect mouse drags over object
 * interacts with the PlottableArrow interface in order to create
 * A draggable arrow/arrow head
 */
public class OnMouseDragHandler : MonoBehaviour {

    [SerializeField]
    public OnMouseClickEvent OnClick;
    [SerializeField]
    public OnMouseDragEvent OnDrag;
    [SerializeField]
    public OnMouseDragEvent OnRelease;
    
    private bool IsBeingDragged;
    private Vector2 From;
    private Vector2 To;

    void Start() {
        IsBeingDragged = false;
    }

    void Update() {
        if (MouseUtil.IsClicked(this.gameObject)) {
            From = MouseUtil.GetMouseWorldPosition();
            IsBeingDragged = true;
            OnClick.Invoke(From);
        }
        if (!MouseUtil.IsClickReleased() && IsBeingDragged) {
            To = MouseUtil.GetMouseWorldPosition();
            OnDrag.Invoke(From, To);
        }
        if (MouseUtil.IsClickReleased() && IsBeingDragged) {
            To = MouseUtil.GetMouseWorldPosition();
            IsBeingDragged = false;
            OnRelease.Invoke(From, To);
        }
    }
}
