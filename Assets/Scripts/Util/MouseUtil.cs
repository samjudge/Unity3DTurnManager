using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MouseUtil {

    public static Vector3 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
        ));
    }

    public static bool IsClickReleased() {
        if (Input.GetMouseButtonUp(0)) {
            return true;
        } else {
            return false;
        }
    }

    public static bool IsClicked(GameObject ClickTarget) {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    Camera.main.nearClipPlane
                )), Vector3.zero
            );
            foreach (RaycastHit2D hit in hits) {
                //check if the mouse is over this gameobject's Traversable component
                if (hit.transform.gameObject == ClickTarget) {
                    return true;
                }
            }
        }
        return false;
    }

}
