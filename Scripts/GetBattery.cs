using UnityEngine;
using UnityEngine.UI;

public class GetBattery : MonoBehaviour
{
    public GameObject BatteryImage;

    [Header("時計ギミックスクリプト")]
    [SerializeField] private ClockGimmick clockGimmick;
  
    void Start()
    {
        BatteryImage.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            clockGimmick.CanOpenClock();
            this.gameObject.SetActive(false);
            BatteryImage.SetActive(true);
        }
    }
}

