using UnityEngine;

public class Cell : MonoBehaviour
{
    public Bolt CurrentBolt { get; private set; }
    public Bolt ReserveBolt { get; private set; }

    public bool IsFree => CurrentBolt == null && ReserveBolt == null;

    public void Occupy(Bolt bolt)
    {
        if (ReserveBolt != null)
        {
            CurrentBolt = ReserveBolt;
            ReserveBolt = null;
        }

        CurrentBolt = bolt;
    }

    public void Reserve(Bolt bolt)
    {
        ReserveBolt = bolt;
    }

    public void Remove()
    {
        CurrentBolt = null;
    }
}
