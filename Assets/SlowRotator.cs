using UnityEngine;

public class SlowRotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(20f, 10f, 0f); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}