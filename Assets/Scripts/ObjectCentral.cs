using UnityEngine;

public class ObjectCentral : MonoBehaviour
{
    public Bolt[] Bolts { get; private set; }

    private void Awake()
    {
        Bolts = GetComponentsInChildren<Bolt>();
    }
}
