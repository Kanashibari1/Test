using System.Linq;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    private float rotationSpeed = 5;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Rotate();
        }
    }

    public void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, -mouseX * rotationSpeed, Space.World);
        transform.Rotate(Vector3.right, mouseY * rotationSpeed, Space.World);
    }
}
