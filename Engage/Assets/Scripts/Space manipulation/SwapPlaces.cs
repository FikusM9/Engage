// vezuje se za jedan od dva objekta koji treba da se zamene drugi objekat prevuci u inspetkor u polje swapableObject

using UnityEngine;

public class SwapPlaces : MonoBehaviour
{
    [SerializeField] private GameObject swapableObject;
    Vector3 temp;
    private void OnSwap()
    {
        temp = transform.position;
        transform.position = swapableObject.transform.position;
        swapableObject.transform.position = temp;
    }
}
