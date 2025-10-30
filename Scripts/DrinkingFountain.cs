using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrinkingFountain : MonoBehaviour
{
    [SerializeField] private Text drinkingFountainText;
    [SerializeField] private DrinkingWater drinkingWater;
    [SerializeField] private Leaf leaf;
    private bool isOnDrinkingFountain = true;
    private bool isPlayerInRange = false;
    private float delayAnimationTrueTime = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && isPlayerInRange)
        {
            isOnDrinkingFountain = !isOnDrinkingFountain;
            if (isOnDrinkingFountain)
            {
                drinkingWater.OnDrinkingWater();
                leaf.LeafAnimator.enabled = false;
            }
            else
            {
                drinkingWater.OffDrinkWater();
                StartCoroutine(WaitFalseSinkWater());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            drinkingFountainText.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            drinkingFountainText.enabled = false;
        }
    }
    private IEnumerator WaitFalseSinkWater()
    {
        yield return new WaitForSeconds(delayAnimationTrueTime);
        leaf.LeafAnimator.enabled = true;
        yield break;
    }
}
