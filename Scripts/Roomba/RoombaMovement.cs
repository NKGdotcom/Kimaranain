using UnityEngine;
using UnityEngine.AI;

public class RoombaMovement : MonoBehaviour
{
    [Header("NavMeshAgent")]
    [SerializeField] private NavMeshAgent agent;
    [Header("ルンバの移動スピード")]
    [SerializeField] private float rombaMoveSpeed;
    [Header("ルンバの回転スピード")]
    [SerializeField] private float roombaRotateSpeed;
    [Header("目的地到達の許容距離")]
    [SerializeField] private float destinationThreshold = 0.5f; // Agentが目的地に到達したとみなす距離
    private float moveThreshold = 0.1f; // Agentが移動中とみなす距離

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(agent == null) { Debug.LogError("NavMeshAgentがアサインされていません。"); return; }
        SetUp();
    }

    // NavMeshAgentの基本的な設定
    private void SetUp()
    {
        agent.speed = rombaMoveSpeed;
        agent.angularSpeed = roombaRotateSpeed;
        agent.autoBraking = true; // Roombaが目的地に近づくと自動的に減速・停止するよう設定
        agent.updateRotation = true; // NavMeshAgentに回転を任せる
    }

    //移動中かどうか
    public bool IsMoving()
    {
        return agent.hasPath && agent.remainingDistance > moveThreshold;
    }

    // 目的地に到達したかどうか
    public bool IsArrived()
    {
        if (agent.pathPending && agent.remainingDistance <= destinationThreshold) return false;
        return (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    // 目的地を設定するメソッド
    public void SetDestination(Vector3 _targetPos)
    {
        agent.SetDestination(_targetPos);
    }

    //移動を強制キャンセルして、指定した位置にワープさせるメソッド
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
