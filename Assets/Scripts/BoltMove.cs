using System;
using System.Collections;
using UnityEngine;

public class BoltMove : MonoBehaviour, IMoveCompleteHandler
{
    private float _speed = 1f;
    private float _distance = 0.001f;
    private Coroutine _coroutine;

    public event Action<Bolt> MoveComplete;

    public void StartCoroutine(Bolt bolt, Vector3 position, bool isStorageCell)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(MoveBolt(bolt, position, isStorageCell));
    }

    public IEnumerator MoveBolt(Bolt bolt, Vector3 positionCell, bool isStorageCell)
    {

        while ((bolt.transform.position - positionCell).sqrMagnitude > _distance * _distance)
        {
            bolt.transform.position = Vector3.MoveTowards(bolt.transform.position, positionCell, _speed * Time.deltaTime);

            yield return null;
        }

        if (isStorageCell)
        {
            MoveComplete.Invoke(bolt);
        }
    }
}
