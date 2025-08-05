using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerActiveHolder : MonoBehaviour
{
    private List<Container> _storage = new();

    public void AddBolt(Bolt bolt)
    {
        foreach (Container container in _storage.ToList())
        {
            if (container.Color == bolt.Color && container != null)
            {
                container.AddBolt(bolt);
            }
        }
    }

    public void AddContainer(Container container)
    {
        if (_storage.Contains(container) != true)
        {
            container.Full += Remove;
            _storage.Add(container);
        }
    }

    public void Remove(Container container)
    {
        Cell[] cells = container.Cells;

        foreach (Cell cell  in cells)
        {
            cell.Remove();
        }

        container.Full -= Remove;
    }

    public void Restart()
    {
        _storage.Clear();
    }
}
