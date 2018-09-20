using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class OnMouseDragPlotMovementHandler
 * @extends MonoBehaviour
 * Detect mouse drags over object
 * interacts with the PlottableArrow interface in order to create
 * A draggable arrow/arrow head
 */
public class PlottedMovement : MonoBehaviour {
    /**
     * @var Movement Movement the object to be moved
     */
    [SerializeField]
    public Movement Movable;
    /**
     * @var CardinalPlottableArrow PlotArrow the PlottableArrow that is used to render the path provided by this component
     */
    [SerializeField]
    public CardinalPlottableArrow PlotArrow;

    public Vector2 MoveToLocation { get; private set; }
    public bool IsInputLocked { get; private set; }

    void Start() {
        IsInputLocked = false;
        MoveToLocation = Movable.GetPosition();
    }

    public bool IsValidPlottedMovement() {
        Vector2[] Points = PlotArrow.GetPoints();
        if (Points.Length <= 1) {
            return false;
        }
        return true;
    }

    public void AttemptMovement() {
        if (!IsInputLocked) {
            Vector2[] Points = PlotArrow.GetPoints();
            if (IsValidPlottedMovement()) {
                MoveToLocation = Points[Points.Length - 1];
                IsInputLocked = true;
                StartCoroutine(MoveToPlottedPoint(MoveToLocation));
            }
        }
    }
 
    /**
     * @method PlotPathTo
     * @param Vector3 To position to plot a path to
     * Plot a path from current Movement location to provided one
     */
    public void PlotPathTo(Vector3 To) {
        if (!IsInputLocked) {
            Vector2[] PlotLocations = PathToLocation(To);
            PlotArrow.ClearPlotPoints();
            PlotArrow.AddPlotPoint(Movable.GetPosition());
            PlotArrow.AddPlotPoints(PlotLocations);
        }
    }

    /**
     * @method MoveToPlottedPoint
     * @param Vector2 Point 
     * Apply a force moving Movable Body from current location to Point (meant to be applied constantly)
     */
    private IEnumerator MoveToPlottedPoint(Vector2 TargetPosition) {
        while (((Vector2)Movable.GetPosition() - TargetPosition).magnitude > 0.05f) {
            Movable.MoveTowards(TargetPosition);
            yield return null;
        }
        Movable.Halt();
        Movable.SetPosition(new Vector3(
            TargetPosition.x,
            TargetPosition.y,
            Movable.GetPosition().z
        ));
        IsInputLocked = false;
    }

    /**
     * @method PathToLocation
     * @param Vector3 Location The location to path to
     * @return Vector2[]
     * Create a set of positions that mark out the points that are to be plotted
     * between the Movable body's Location and the one provided.
     * Specifically it
     *   - Gets the cardinal direction from current movable body's location to input location
     *   - Plots x tiles in the given direction
     *   - The number of tiles is dermined by colliders that block movemement,
     * and distance from Location (it will not surpass it by the magnitude diff between the Movable body and Location)
     */
    private Vector2[] PathToLocation(Vector3 Location) {
        List<Vector2> positions = new List<Vector2>();
        ArrowDirection travelDirection = ArrowDirectionUtil.GetBestFitRelativeCardinalDirection(
            Movable.GetPosition(),
            Location
        );
        Vector3 dir = Quaternion.Euler(0,0, (float)travelDirection + 180) * Vector2.up;
        //Debug.DrawLine(Movable.GetPosition(), Location, new Color(0f, 1f, 0f));
        int distance = (int) Mathf.Round(((Vector2)Location - (Vector2)Movable.GetPosition()).magnitude);
        positions.AddRange(PlotUntilCollision(Movable.GetPosition(), dir, distance));
        return positions.ToArray();
    }

    /**
     * @method PlotUntilCollision
     * @param Vector3 From The location to plot from
     * @param Vector2 Dir The direction the plotting should occur
     * @param int MaxDepth The maximum number of plots that can be added
     * @return Vector2[]
     * Create a set of positions that mark out the points that should be plotted
     */
    private Vector2[] PlotUntilCollision(Vector2 From, Vector2 Dir, int MaxDepth) {
        List<Vector2> plots = new List<Vector2>();
        bool hasHitCollider = false;
        int cDepth = 0;
        while (!hasHitCollider && cDepth < MaxDepth) {
            RaycastHit2D[] hits = Physics2D.LinecastAll(From, From + Dir);
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.isTrigger) continue;
                if (hit.transform.gameObject != this.Movable.GetTransform().gameObject) {
                    hasHitCollider = true;
                    break;
                }
            }
            if (hasHitCollider) break;
            plots.Add(From + Dir);
            cDepth++;
            From = From + Dir;
        }
        return plots.ToArray();
    }
}
