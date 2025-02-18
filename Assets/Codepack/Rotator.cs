using UnityEngine;

// Transform.Rotate example
//
// This script creates two different cubes: one red which is rotated using Space.Self; one green which is rotated using Space.World.
// Add it onto any GameObject in a scene and hit play to see it run. The rotation is controlled using xAngle, yAngle and zAngle, modifiable on the inspector.

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    void Update()
    {
        transform.Rotate(rotation * speed *Time.deltaTime);
    }
}