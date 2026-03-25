using UnityEngine;

public class FilterScript : MonoBehaviour
{
    public GameObject Filter;

    void OnTriggerEnter(Collider other)
    {
        Filter.SetActive(false);       
    }

}
