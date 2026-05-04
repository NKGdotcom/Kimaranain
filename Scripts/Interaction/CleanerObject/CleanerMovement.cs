using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ロボット掃除機の移動処理クラス
/// </summary>
public class CleanerMovement : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("NavmeshAgentで経路探索を行う")]
    [SerializeField] private NavMeshAgent agent;

    [Header("パラメータ")]
    [Tooltip("ロボット掃除機が進むスピード")]
    [SerializeField] private float cleanerMoveSpeed;
    [Tooltip("ロボット掃除機が回転するスピード")]
    [SerializeField] private float cleanerRotateSpeed;
    [Tooltip("目的地に到達したとみなされるずれの距離")]
    [SerializeField] private float destinationThreshold = 0.5f;

    //パラメータ
    //Agentが移動中とみなす距離
    private float moveThreshold = 0.1f;

    void Awake()
    {
        if(agent == null) { Debug.LogError("NavMeshAgentがアサインされていません。"); return; }

        //パラメータ設定
        agent.speed = cleanerMoveSpeed;
        agent.angularSpeed = cleanerRotateSpeed;
        // ロボット掃除機が目的地に近づくと自動的に減速・停止するよう設定
        agent.autoBraking = true;
        // NavMeshAgentに回転を任せる
        agent.updateRotation = true; 
    }

    /// <summary>
    /// 移動中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsMoving()
    {
        return agent.hasPath && agent.remainingDistance > moveThreshold;
    }

    /// <summary>
    /// 目的地に到達したかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsArrived()
    {
        if (agent.pathPending && agent.remainingDistance <= destinationThreshold) return false;
        return (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    /// <summary>
    /// 進む方向を決める
    /// </summary>
    /// <param name="_targetPos"></param>
    public void SetDestination(Vector3 _targetPos)
    {
        agent.SetDestination(_targetPos);
    }

    /// <summary>
    /// 移動を強制キャンセルして、指定した位置にワープさせる
    /// </summary>
    /// <param name="_warpPos"></param>
    public void StopMovement(Vector3 _warpPos)
    {
        if (agent.isActiveAndEnabled)
        {
            agent.ResetPath();
            agent.Warp(_warpPos);
        }
        else
        {
            transform.localPosition = _warpPos;
        }
    }
}
