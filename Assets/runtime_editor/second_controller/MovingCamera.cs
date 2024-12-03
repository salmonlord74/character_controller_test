using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MovingCamera : MonoBehaviour
{
    [SerializeField]
    Transform focus = default;

    [SerializeField, Range(1f, 20f)]
    float distance = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate()
    {
        Vector3 focusPoint = focus.position;
        Vector3 lookDirection = transform.forward;
        transform.localPosition = focusPoint - lookDirection * distance;
    }
    // Update is called once per frame
 
}
