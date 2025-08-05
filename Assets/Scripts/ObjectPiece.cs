using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPiece : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private List<Bolt> Bolts;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Bolts = GetComponentsInChildren<Bolt>().ToList();

        foreach (Bolt bolt in Bolts)
        {
            bolt.OnBoltRemoved += RemoveBolt;
        }
    }

    public void RemoveBolt(Bolt bolt)
    {
        Bolts.Remove(bolt);

        if (Bolts.Count == 0)
        {
            _rigidbody.isKinematic = false;
            transform.SetParent(null);
        }

        bolt.OnBoltRemoved -= RemoveBolt;
    }
}
