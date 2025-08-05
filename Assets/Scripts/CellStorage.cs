using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellStorage : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    public event Action EndGame;

    public Cell TryGetCell()
    {
        if(_cells.All(cell => cell.CurrentBolt != null))
        {
            EndGame.Invoke();
        }

        Cell cell = _cells.FirstOrDefault(cell => cell.IsFree);

        return cell;
    }

    public IEnumerable<Bolt> Sort(Container container)
    {
        List<Cell> matchingCells = _cells.Where(cell => cell.CurrentBolt != null && cell.CurrentBolt.Color == container.Color).ToList();

        List<Bolt> bolts = matchingCells.Select(cell => cell.CurrentBolt).ToList();

        foreach (Cell cell in matchingCells)
        {
            cell.Remove();
        }

        return bolts;
    }

    public void Restart()
    {
        foreach (Cell cell in _cells)
        {
            cell.Remove();
        }
    }
}
