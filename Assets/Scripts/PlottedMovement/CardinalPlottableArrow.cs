using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardinalPlottableArrow : MonoBehaviour, Plottable {

    public bool IsRendered() {
        return this.LineRenderer.enabled;
    }

    public void SetRenderable(bool on) {
        if (on) {
            this.ArrowHead.enabled = true;
            this.LineRenderer.enabled = true;
        } else {
            this.ArrowHead.enabled = false;
            this.LineRenderer.enabled = false;
        }
    }

    private class PlottableArrowPoint {
        public PlottableArrowPoint(Vector2 Point, ArrowDirection Direction) {
            this.Point = Point;
            this.Direction = Direction;
        }
        public Vector2 Point;
        public ArrowDirection Direction;
    }

    private List<PlottableArrowPoint> Points;

    void Awake() {
        Points = new List<PlottableArrowPoint>();
    }

    [SerializeField]
    public LineRenderer LineRenderer;

    [SerializeField]
    public SpriteRenderer ArrowHead;

    void Update() {
        if (this.Points.Count >= 1) {
            PlottableArrowPoint HeadPoint = this.Points[this.Points.Count - 1];
            ArrowHead.transform.position = new Vector3(
                HeadPoint.Point.x,
                HeadPoint.Point.y,
                ArrowHead.transform.position.z
            );
            if (HeadPoint.Direction != ArrowDirection.Undefined) {
                ArrowHead.transform.rotation = Quaternion.Euler(0, 0, ((float)HeadPoint.Direction + 90));
            }
        }
        Vector3[] Positions = GetPoints3d();
        LineRenderer.positionCount = Positions.Length;
        LineRenderer.SetPositions(Positions);
    }

    private int PlotPointIndex = 0;

    public void AddPlotPoint(Vector2 Point) {
        ArrowDirection Dir = ArrowDirection.Undefined;
        if (Points.Count != PlotPointIndex++) {
            return;
        }
        if (Points.Count >= 1) {
            Dir = ArrowDirectionUtil.GetBestFitRelativeCardinalDirection(Points[Points.Count - 1].Point, Point);
        }
        PlottableArrowPoint ArrowPoint = new PlottableArrowPoint(Point, Dir);
        Points.Add(ArrowPoint);
    }

    public void AddPlotPoints(Vector2[] Points) {
        foreach (Vector2 p in Points) {
            AddPlotPoint(p);
        }
    }

    public void ClearPlotPoints() {
        PlotPointIndex = 0;
        Points.Clear();
    }

    public Vector2[] GetPoints() {
        List<Vector2> pointsAsVectors = new List<Vector2>();
        foreach (PlottableArrowPoint p in Points) {
            pointsAsVectors.Add(p.Point);
        }
        return pointsAsVectors.ToArray();
    }

    private Vector3[] GetPoints3d() {
        List<Vector3> pointsAsVectors = new List<Vector3>();
        foreach (PlottableArrowPoint p in Points) {
            pointsAsVectors.Add(new Vector3(p.Point.x, p.Point.y,this.transform.position.z));
        }
        return pointsAsVectors.ToArray();
    }
}
