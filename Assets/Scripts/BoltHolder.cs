using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoltHolder : MonoBehaviour
{
    private Dictionary<Color, List<Bolt>> _holder = new();
    private bool _canGetColor = true;
    private const int CountCellContainer = 3;

    public void Initialize(Bolt[] bolts)
    {
        if (bolts != null)
        {
            foreach (Bolt bolt in bolts)
            {
                Color color = bolt.Color;

                if (_holder.ContainsKey(color) != true)
                {
                    _holder[color] = new List<Bolt>();
                }

                _holder[color].Add(bolt);
            }
        }
    }

    public bool GetRandomColor(out Color color)
    {
        color = default;

        if (_holder.Count == 0)
        {
            _canGetColor = false;

            return _canGetColor;
        }

        List<Color> colors = _holder.Keys.ToList();

        color = colors[Random.Range(0, colors.Count)];

        if (_holder.TryGetValue(color, out List<Bolt> bolts))
        {
            bolts.RemoveRange(0, CountCellContainer);

            if (bolts.Count == 0)
            {
                _holder.Remove(color);
            }
        }

        return _canGetColor;
    }

    public void RemoveBolts(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            Destroy(cell.CurrentBolt.gameObject);
        }
    }

    public void Restart(Bolt[] bolts)
    {
        _holder.Clear();
        Initialize(bolts);
    }
}
