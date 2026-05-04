using UnityEngine;

/// <summary>
/// 皿を統括するクラス
/// </summary>
public class Plate : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("皿の上に乗っかっているお椀")]
    [SerializeField] private Bowl bowl;
    [Header("状態")]
    [Tooltip("お椀が上に乗っかっているか")]
    [SerializeField] private bool isPlacedBowl = false;

    [Header("パラメータ")]
    [Tooltip("重力の強さの倍率")]
    [SerializeField] private float gravityScale = 1.0f;

    //コンポーネント参照
    //シンクの水
    private Water water;
    //お皿自体の物理挙動
    private Rigidbody plateRb;

    //状態
    //現在の重力が反転状態か
    private bool isReversed = false;
    private void Awake()
    {
        if(isPlacedBowl) { Debug.LogError("bowlが参照されていません"); return; }
        
        TryGetComponent<Rigidbody>(out plateRb);
        if(plateRb == null) { Debug.LogError("plateRbが参照されていません"); return; }

        bowl.OnPickup += PickBowl;
    }

    void FixedUpdate()
    {
        if (isReversed)
        {
            // 反転状態のときだけ、上向きの力を加える
            Vector3 reverseGravityForce = -Physics.gravity * gravityScale;
            plateRb.AddForce(reverseGravityForce, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (water == null) { TryGetComponent(out water); }

        else
        {
            if (!isPlacedBowl)
            {
                UpdateGravitySystem();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isPlacedBowl)
        {
            UpdateGravitySystem();
        }
    }

    /// <summary>
    /// お椀を取り、皿が浮くように
    /// </summary>
    private void PickBowl()
    {
        isPlacedBowl = false;
    }

    /// <summary>
    /// フラグに応じてUnity標準の重力のON/OFFを切り替える
    /// </summary>
    private void UpdateGravitySystem()
    {
        // 反転しているなら標準重力をOFF、通常に戻すならONにする
        plateRb.useGravity = !isReversed;
    }
}
