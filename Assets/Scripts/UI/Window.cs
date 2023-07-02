using Enums;
using UnityEngine;
using Zenject;

public class Window : MonoBehaviour
{
    [field: SerializeField] public WindowTypeId WindowTypeId { get; private set; }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
