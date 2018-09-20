using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
* @interface Plottable
* A plottable interface designates a type composed of several points
*/
public interface Plottable {
    void AddPlotPoint(Vector2 Point);
    void AddPlotPoints(Vector2[] Points);
    void ClearPlotPoints();
    Vector2[] GetPoints();
}
