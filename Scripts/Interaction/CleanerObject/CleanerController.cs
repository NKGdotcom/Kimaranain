using UnityEngine;

/// <summary>
/// ロボット掃除機の動きを統括するクラス
/// </summary>
public class CleanerController : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("ロボット掃除機の移動を管理する処理")]
    [SerializeField] private CleanerMovement movement;
    [Tooltip("ロボット掃除機の進むルートを管理する処理")]
    [SerializeField] private CleanerRouteManager routeManager;
    [Tooltip("ロボット掃除機のインタラクトに関する処理")]
    [SerializeField] private BaseInteraction interaction;

    [Header("パラメータ")]
    [Tooltip("ロボット掃除機のSEを鳴らす間隔")]
    [SerializeField] private float cleanerSEInterval = 2.0f;

    //パラメータ
    //ロボット掃除機のSEを鳴らす間隔をこの変数で計測
    private float cleanerSETimer = 0f;
    //目的地に到達したら、少しの時間待機する
    private float destinationCooldown = 0.5f;
    //目的地に到達した後、この変数でクールダウン処理を計測
    private float destinationTimer = 0f;
    //初期位置を保存
    private Vector3 idlePos;

    //状態
    //電源が付いているかどうか
    private bool isPower;


    void Awake()
    {
        if (movement == null) { Debug.LogError("CleanerMovementが参照されていません。"); return; }
        if (routeManager == null) { Debug.LogError("CleanerRouteManagerが参照されていません。"); return; }
        if (interaction == null) { Debug.LogError("CleanerInteractionが参照されていません。"); return; }

        idlePos = transform.localPosition;
        destinationTimer = destinationCooldown;

        interaction.OnInteractionStart += TurnOn;
    }

    void Update()
    {
        //電源がオンでない場合は動かない
        if (!isPower) return;

        //時を戻したら電源オフ
        if(TimeStateManager.Instance.IsPastState())
        {
            TurnOff();
            return;
        }

        //移動中はSEをインターバル間で鳴らす
        if (movement.IsMoving())
        {
            cleanerSETimer -= Time.deltaTime;
            if (cleanerSETimer <= 0f)
            {
                SoundManager.Instance.RoombaSE();
                cleanerSETimer = cleanerSEInterval;
            }
        }
        // 停止中ならタイマーリセット
        else
        {
            cleanerSETimer = cleanerSEInterval;
        }

        if(destinationTimer > 0f)
        {
            destinationTimer -= Time.deltaTime;
        }
        //到達したら、次の目的地にターゲットを設定し移動
        else if (movement.IsArrived())
        {
            Vector3 _targetPos = routeManager.NextDestination();
            movement.SetDestination(_targetPos);

            destinationTimer = destinationCooldown;
        }
    }

    /// <summary>
    /// ロボット掃除機の電源オン
    /// </summary>
    private void TurnOn()
    {
        isPower = true;

        //目的地を設定
        Vector3 _firstPos = routeManager.NextDestination();
        movement.SetDestination(_firstPos);

        destinationTimer = destinationCooldown;
    }

    /// <summary>
    /// ロボット掃除機の電源オフ
    /// </summary>
    private void TurnOff()
    {
        isPower = false;
        movement.StopMovement(idlePos);
        routeManager.ResetOriginRoute();
        interaction.ResetInteract();
    }
}
