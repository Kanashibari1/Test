using UnityEngine;

public class CreateObject : MonoBehaviour 
{
    [SerializeField] private ObjectCentral _prefab;

    public ObjectCentral Create()
    {
        ObjectCentral @object = Instantiate(_prefab, transform.position, transform.rotation);

        return @object;
    }
}
