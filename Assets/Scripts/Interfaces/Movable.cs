using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface Movable {
    void Halt();
    
    void AddForce(Vector2 Direction, float Acceleration);
    void MoveRelative(Vector2 Direction);
    void MoveTowards(Vector2 Target);

    void RotateToFace(Vector3 Target);
    void SetRotation(Vector3 Rotation);

    Vector3 GetPosition();
    void SetPosition(Vector3 Position);
    Transform GetTransform();
}