using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnContainer : PoolObject<Container>
{
    [SerializeField] private Container _container;

    private Dictionary<Transform, Container> _spawnPosition = new();
    private BoltHolder _boltHolder;
    private int _spawnCount = 4;

    public event Action<Container> OnSpanned;

    //private void Start()
    //{
    //    Transform[] spawnPoint = GetComponentsInChildren<Transform>().Where(transform => transform != this.transform).ToArray();

    //    foreach (Transform spawn in spawnPoint)
    //    {
    //        _spawnPosition[spawn] = null;
    //    }

    //    for (int i = 0; i < _spawnCount; i++)
    //    {
    //        Create(_container);
    //    }
    //}

    public void Initialized(BoltHolder boltHolder)
    {
        _boltHolder = boltHolder;
    }

    public Container GetContainer(Transform position, Color color)
    {
        Container container = GetObject(_container);
        container.Full += PutContainer;
        container.gameObject.SetActive(true);
        container.transform.position = position.position;
        container.SetColor(color);

        return container;
    }

    public void Spawn()
    {
        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            Transform spawn = FindFreeSpawnPosition();

            if (spawn != null)
            {
                if (_boltHolder.GetRandomColor(out Color color))
                {
                    Container container = GetContainer(spawn, color);
                    _spawnPosition[spawn] = container;
                    OnSpanned.Invoke(container);
                }
            }
        }
    }

    public Container FindContainerColor(Color colorBolt)
    {
        Container container = _spawnPosition.FirstOrDefault(container => container.Value != null && container.Value.Color == colorBolt).Value;

        return container;
    }

    public Transform FindFreeSpawnPosition()
    {
        Transform position = _spawnPosition.FirstOrDefault(position => position.Value == null).Key;

        return position;
    }

    public void RemoveContainer(Container container)
    {
        Transform transform = _spawnPosition.FirstOrDefault(position => position.Value == container).Key;

        _spawnPosition[transform] = null;
    }

    public void PutContainer(Container container)
    {
        _boltHolder.RemoveBolts(container.Cells);
        RemoveContainer(container);
        PutObject(container);
        container.Full -= PutContainer;
        Spawn();
    }

    public void Restart(BoltHolder boltHolder)
    {
        Initialized(boltHolder);

        Transform[] spawnPoint = GetComponentsInChildren<Transform>().Where(transform => transform != this.transform).ToArray();

        foreach (Transform spawn in spawnPoint)
        {
            _spawnPosition[spawn] = null;
        }

        for (int i = 0; i < _spawnCount; i++)
        {
            Create(_container);
        }

        Spawn();
    }
}
