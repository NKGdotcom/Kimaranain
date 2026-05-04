using UnityEngine;

/// <summary>
/// キッチンシンクの中にある水を統括するクラス
/// </summary>
public class Water : MonoBehaviour
{
    [Header("パラメータ")]
    [Tooltip("水が上がるスピード")]
    [SerializeField] private float moveSpeed = 3f;
    [Tooltip("上に上がりきる高さ")]
    [SerializeField] private float upHeight = 2.9f;
    [Tooltip("下に下がりきる高さ")]
    [SerializeField] private float downHeight = 0f;

    //パラメータ
    //ターゲットの位置に移動
    private float targetY = 0;

    private void Awake()
    {
        TimeStateManager.Instance.OnFutureTimeline += MoveUpWater;
        TimeStateManager.Instance.OnNormalTimeline += MoveDownWater;
    }

    void Update()
    {
        Vector3 _currentPos = transform.position;
        float _newY = Mathf.MoveTowards(_currentPos.y, targetY, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(_currentPos.x, _newY, _currentPos.z);
    }

    private void OnDestroy()
    {
        TimeStateManager.Instance.OnFutureTimeline -= MoveUpWater;
        TimeStateManager.Instance.OnNormalTimeline -= MoveDownWater;
    }

    private void MoveUpWater()
    {
        targetY = upHeight;
    }

    private void MoveDownWater()
    {
        targetY = downHeight;
    }
}
