using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Service _service;

    public event Action<Bolt> OnBolt;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray();
        }
    }

    private void Ray()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Bolt bolt))
            {
                bolt.Disable();
                OnBolt.Invoke(bolt);
            }
        }
    }
}
