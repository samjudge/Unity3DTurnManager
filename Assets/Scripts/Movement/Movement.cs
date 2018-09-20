using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
/**
 * @class Movement
 * @extends MonoBehaviour
 * Exposes various methods to move a Rigidbody2d in various ways
 */
public class Movement : MonoBehaviour, Movable {

    /**
     * @var Rigidbody2D Body the Rigidbody2D to be moved
     */
    [SerializeField]
    public Rigidbody2D Body;
    /**
     * @var float the speed at which this body accelerates
     */
    [SerializeField]
    public float Acceleration;
    /**
     * @var Vector2 MaxSpeed the max speed the body will move at (by individual axis)
     */
    [SerializeField]
    public Vector2 MaxSpeed;
    /**
     * @var Vector2 ModAngle setting this will determine the direction of relative movement commands (MoveUp,MoveRight,MoveForward etc.)
     *                       Leave as 0 for default directions. Typically mutated by a camera controller.
     */
    [SerializeField]
    public float ModAngle;
    /**
     * @var Vector3 ModAxis The axis which this body's relative movement occurs to (modified by ModAngle).
     *                      Leave as 0,0,0 for default directions. Typically mutated by a camera controller.
     */
    [SerializeField]
    public Vector3 ModAxis;
    /**
     * @method MoveTowards
     * @param Vector2 target the position to move towards
     * @return void
     */
    public void MoveTowards(Vector2 Target) {
        MoveTowards(Target, Acceleration);
    }
    /**
     * @method MoveTowards
     * @param Vector2 target the position to move towards
     * @param float MaxSpeed the maximum speed of this body
     * @return void
     */
    public void MoveTowards(Vector2 Target, float MaxSpeed) {
        if (Body.velocity.magnitude < MaxSpeed) {
            Vector2 t = ((Target - (Vector2)this.transform.position) * 10000).normalized;
            Body.AddForce(MaxSpeed * t);
        }
    }
    /**
     * @method Halt
     * @return void
     * Stop all movement
     */
    public void Halt() {
        Body.velocity = new Vector3(0,0,0);
    }
    /**
     * @method AddForce
     * @param Vector2 Direction the direction to apply force to
     * @param float the magnitude of the direction to apply
     * @return void
     * Apply a force to this body
     */
    public void AddForce(Vector2 Direction, float Acceleration) {
        Body.AddForce((Acceleration * Direction.normalized));
    }
    /**
     * @method RotateToFace
     * @param Vector3 Target the position in world space to look at
     * @return void
     */
    public void RotateToFace(Vector3 Target) {
        this.transform.LookAt(Target);
    }
    /**
     * @method SetRotation
     * @param Vector3 the x,y,z rotation
     * @return void
     * set's the movable body to face the given Rotation
     */
    public void SetRotation(Vector3 Rotation) {
        this.transform.rotation = Quaternion.Euler(Rotation.x, Rotation.y, Rotation.z);
    }
    /**
     * @method RotateVectorByMod
     * @param Vector2 Dir the vector to rotate
     * @return Vector2 the rotated Vector2
     * Rotate the direction Dir by this component's internal mod* variables
     */
    public Vector2 RotateVectorByMod(Vector2 Dir) {
        Dir = Quaternion.AngleAxis(ModAngle, ModAxis) * Dir;
        return Dir;
    }
    
    public void MoveRelative(Vector2 Direction) {
        MoveRelative(Direction, this.MaxSpeed);
    }

    /**
     * @method RotateVectorByMod
     * @param Vector2 Direction the vector to rotate
     * @param Vector2 MaxSpeedByAxis the max speeds (bound by each individual x,y,z axis)
     * @return void
     * Move in the given Direction, modified by this component's internal mod* variables
     */
    public void MoveRelative(Vector2 Direction, Vector2 MaxSpeedByAxis) {
        Vector2 relativeDirection = RotateVectorByMod(Direction);
        Vector2 relativeMaxSpeed = new Vector2(
            relativeDirection.x * MaxSpeedByAxis.x,
            relativeDirection.y * MaxSpeedByAxis.y
        );
        Vector2 relativeCurrentSpeed = new Vector2(
            this.Body.velocity.x * relativeDirection.x,
            this.Body.velocity.y * relativeDirection.y
        ); 
        if (relativeCurrentSpeed.magnitude < relativeMaxSpeed.magnitude) {
            Body.AddForce(Acceleration * relativeDirection);
        }
    }

    /**
     * @method MoveUp
     * MoveRelative( Vector3.up )
     */
    public void MoveUp() {
        MoveRelative(Vector2.up);
    }

    /**
     * @method MoveDown
     * MoveRelative( Vector3.down )
     */
    public void MoveDown() {
        MoveRelative(Vector2.down);
    }

    /**
     * @method MoveLeft
     * MoveRelative( Vector3.left )
     */
    public void MoveLeft() {
        MoveRelative(Vector2.left);
    }

    /**
     * @method MoveRight
     * MoveRelative( Vector3.right )
     */
    public void MoveRight() {
        MoveRelative(Vector2.right);
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetPosition(Vector3 Position) {
        this.transform.position = Position;
    }

    public Transform GetTransform() {
        return transform;
    }
}
