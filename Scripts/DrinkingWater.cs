using System.Collections;
using UnityEngine;

public class DrinkingWater : MonoBehaviour
{
    public bool IsOffDrinkWater { get; private set; }
    private float moveSpeed = 3f;
    private float targetY = 0f;
    private SinkWater sinkWater;
    private float waitFalseSinkWater = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sinkWater = GetComponent<SinkWater>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOffDrinkWater)
        {
            targetY = -1.5f;
        }
        else
        {
            targetY = 1.5f;
        }
        Vector3 currentPos = transform.position;
        float newY = Mathf.MoveTowards(currentPos.y, targetY, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(currentPos.x, newY, currentPos.z);
    }
    public void OffDrinkWater()
    {
        IsOffDrinkWater = true;
        StartCoroutine(WaitFalseSinkWater());
        sinkWater.enabled = false;
    }
    public void OnDrinkingWater()
    {
        IsOffDrinkWater = false;
        sinkWater.enabled = true;
    }
    private IEnumerator WaitFalseSinkWater()
    {
        yield return new WaitForSeconds(waitFalseSinkWater);
        sinkWater.enabled = false;
        yield break;
    }
}
