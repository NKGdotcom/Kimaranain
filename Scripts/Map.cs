using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private bool isGetMap = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isGetMap)
        {
            isGetMap = true;
            this.gameObject.SetActive(false);
        }
    }
}
