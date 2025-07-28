using UnityEngine;

public class RotateCar : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float rotationSpeed = 30f;

    private void Update()
    {
        targetObject.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}