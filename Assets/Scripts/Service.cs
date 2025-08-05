using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContainerActiveHolder))]
public class Service : MonoBehaviour
{
    [SerializeField] private Raycast _raycast;
    [SerializeField] private SpawnContainer _spawnContainer;
    [SerializeField] private CellStorage _cellStorage;

    private const bool IsMoveToCellContainer = true;
    private const bool IsMoveToCellFree = false;

    private ContainerActiveHolder _containerHolder;

    private void Awake()
    {
        _containerHolder = GetComponent<ContainerActiveHolder>();
    }

    private void OnEnable()
    {
        _raycast.OnBolt += TryGetCell;
        _spawnContainer.OnSpanned += MoveBoltContainer;
        _spawnContainer.OnSpanned += AddContainer;
    }

    private void OnDisable()
    {
        _raycast.OnBolt -= TryGetCell;
        _spawnContainer.OnSpanned -= MoveBoltContainer;
        _spawnContainer.OnSpanned -= AddContainer;
    }

    public void TryGetCell(Bolt bolt)
    {
        Container container = _spawnContainer.FindContainerColor(bolt.Color);

        if (container == null)
        {
            Cell cell = _cellStorage.TryGetCell();

            if (cell != null)
            {
                cell.Occupy(bolt);
                bolt.Move(cell.transform.position, IsMoveToCellFree);
            }

            return;
        }

        if (container != null)
        {
            Cell cellInContainer = container.GetCell();
            cellInContainer.Reserve(bolt);

            bolt.Handler.MoveComplete += OnMoveComplete;
            bolt.Move(cellInContainer.transform.position, IsMoveToCellContainer);
        }
    }

    public void MoveBoltContainer(Container container)
    {
        IEnumerable<Bolt> bolts = _cellStorage.Sort(container);

        if (bolts != null)
        {
            foreach (Bolt bolt in bolts)
            {
                bolt.Handler.MoveComplete += OnMoveComplete;
                Cell cell = container.GetCell();
                cell.Reserve(bolt);
                bolt.Move(cell.transform.position, IsMoveToCellContainer);
            }
        }
    }

    public void OnMoveComplete(Bolt bolt)
    {
        _containerHolder.AddBolt(bolt);
        bolt.Handler.MoveComplete -= OnMoveComplete;
    }

    private void AddContainer(Container container)
    {
        _containerHolder.AddContainer(container);
    }
}
