using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Garage : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float openHeight;
    [SerializeField] private float closeHeight;

    private bool moveGarage = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStateManager.Instance.IsNormalState())
        {
            if (!moveGarage) return;
            moveGarage = false;
        }
        else if(TimeStateManager.Instance.IsRewindingState())
        {
            if (moveGarage) return;
            moveGarage = true;

            StartCoroutine(GarageState("Close"));
        }
        else if (TimeStateManager.Instance.IsFastForwardState())
        {
            if (moveGarage) return;
            moveGarage = true;

            StartCoroutine(GarageState("Open"));
        }
    }
    /// <summary>
    /// ガレージの動きを決める
    /// </summary>
    /// <param name="_garageState"></param>
    private IEnumerator GarageState(string _garageState)
    {
        float _nowHeight = transform.position.y;
        switch (_garageState)
        {
            case "Open":
                while (_nowHeight < openHeight)
                {
                    _nowHeight += moveSpeed * Time.deltaTime;
                    transform.position = GarageHeight(_nowHeight);
                    yield return null;
                }
                _nowHeight = openHeight;

                break;

            case "Close":
                while (_nowHeight > closeHeight)
                {
                    _nowHeight -= moveSpeed * Time.deltaTime;
                    transform.position = GarageHeight(_nowHeight);
                    yield return null;
                }
                _nowHeight = closeHeight;

                break;
        }
    }

    /// <summary>
    /// ガレージの高さを取得
    /// </summary>
    /// <param name="_height"></param>
    /// <returns></returns>
    private Vector3 GarageHeight(float _height)
    {
        return new Vector3(transform.position.x, _height, transform.position.z);
    }
}
