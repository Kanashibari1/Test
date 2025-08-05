using System;
using System.Linq;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private Renderer _top;
    [SerializeField] private Renderer _down;
    [SerializeField] private Cell[] _cells;

    public event Action<Container> Full;

    public Cell[] Cells => _cells;

    public Color Color { get; private set; }

    public void SetColor(Color containerColor)
    {
        Color = containerColor;
        _top.material.color = containerColor;
        _down.material.color = containerColor;
    }

    public Cell GetCell()
    {
        Cell cell = _cells.FirstOrDefault(cell => cell.IsFree);

        return cell;
    }

    public void AddBolt(Bolt bolt)
    {
        Cell cell = _cells.FirstOrDefault(cell => cell.CurrentBolt == null);

        if(cell != null)
        {
            cell.Occupy(bolt);
        }

        if(_cells.All(cell => cell.CurrentBolt != null))
        {
            Full.Invoke(this);
        }
    }
}
