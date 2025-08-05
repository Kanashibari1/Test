using System;
using UnityEngine;

[RequireComponent(typeof(BoltMove))]
public class Bolt : MonoBehaviour
{
    private BoltMove _boltMoveCell;
    private Renderer _renderer;
    private Collider _collider;
    private Quaternion _rotation = new(180, 0, 0, 0);

    public event Action<Bolt> OnBoltRemoved;

    public IMoveCompleteHandler Handler => _boltMoveCell.GetComponent<IMoveCompleteHandler>();

    public Color Color => _renderer.material.color;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        _boltMoveCell = GetComponent<BoltMove>();
    }

    public void Move(Vector3 position, bool isStorageCell)
    {
        _boltMoveCell.StartCoroutine(this, position, isStorageCell);
        transform.rotation = _rotation;
        transform.SetParent(null);
    }

    public void Disable()
    {
        _collider.enabled = false;
        OnBoltRemoved.Invoke(this);
    }
}
