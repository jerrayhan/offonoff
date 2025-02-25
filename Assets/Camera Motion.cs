using UnityEngine;
using System.Collections;

public class CameraMotion : MonoBehaviour
{
    [System.Serializable]
    public struct CameraPoint
    {
        public Vector3 position;
        public Vector3 rotation; // Euler angles
        public float waitTime; // Unique delay before switching to the next position
    }

    public CameraPoint[] cameraPoints; // Array of camera points
    public float transitionDuration = 2f; // Time taken to transition

    private int currentPointIndex = 0;

    void Start()
    {
        StartCoroutine(SwitchCameraPosition());
    }

    IEnumerator SwitchCameraPosition()
    {
        while (true)
        {
            CameraPoint targetPoint = cameraPoints[currentPointIndex];
            yield return new WaitForSeconds(targetPoint.waitTime); // Wait for the unique delay

            int nextIndex = (currentPointIndex + 1) % cameraPoints.Length;
            yield return StartCoroutine(TransitionToPoint(cameraPoints[nextIndex]));

            currentPointIndex = nextIndex;
        }
    }

    IEnumerator TransitionToPoint(CameraPoint target)
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(target.rotation);
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            transform.position = Vector3.Lerp(startPosition, target.position, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target.position;
        transform.rotation = targetRotation;
    }
}
