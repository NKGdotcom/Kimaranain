using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Linq;


public class Roomba : MonoBehaviour
{
    [Header("NavMeshAgent")]
    [SerializeField] private NavMeshAgent agent;

    [Header("ルンバの移動スピード")]
    [SerializeField] private float roombaMoveSpeed;

    [Header("ルンバの回転スピード")]
    [SerializeField] private float roombaRotationSpeed;

    [Header("目的地到達の許容距離")]
    [SerializeField] private float destinationThreshold = 0.5f; // Agentが目的地に到達したとみなす距離

    [Header("経路の種類")]
    [SerializeField] private RouteSettings[] routeSettings;

    private int routeNum; // 1つの経路探索は何個ルートあるか
    private int routeSettingsNum; // 設定したルートの種類の数
    private int routeSetting; // randomに選ばれたルートの種類
    private int nowRouteID; // 現在のルートは何個目の方向にいるか

    private List<float> distanceDestination = new List<float>();

    private bool first = true;
    private bool isOnSwitch = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inspectorでagentがアサインされているか確認
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogError("NavMeshAgentがこのGameObjectに見つからないか、Inspectorでアサインされていません。");
                enabled = false; // エージェントが見つからない場合はスクリプトを無効化
                return;
            }
        }

        agent.speed = roombaMoveSpeed;
        agent.angularSpeed = roombaRotationSpeed;
        agent.autoBraking = true; // Roombaが目的地に近づくと自動的に減速・停止するよう設定
        agent.updateRotation = true; // NavMeshAgentに回転を任せる

        routeSettingsNum = routeSettings.Length;
        if (routeSettingsNum == 0)
        {
            Debug.LogError("経路設定が定義されていません。ルンバは移動できません。");
            enabled = false;
            return;
        }

        // Random.Range(int min, int max)はmaxが排他的なので、routeSettingsNumを直接指定
        routeSetting = 0;
        // 選択された経路設定が有効であり、移動ポイントがあることを確認
        if (routeSetting >= routeSettingsNum || routeSettings[routeSetting].MovePointList.Length == 0)
        {
            Debug.LogError("選択されたランダムな経路設定が無効であるか、移動ポイントがありません。");
            enabled = false;
            return;
        }

        routeNum = routeSettings[routeSetting].RouteNum;

        GetMinDistance(); //一番近い場所を取得
        SetNextDestination(); // 最初の目的地を設定
    }

    // Update is called once per frame
    void Update()
    {
        // エージェントがパスを計算中でなく、目的地に十分近づいているかチェック
        if (!agent.pathPending && agent.remainingDistance < destinationThreshold)
        {
            // エージェントが停止しているか、またはターゲットに非常に近い場合、次のポイントへ移動
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                SetNextDestination();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            isOnSwitch = false;
            SetNextDestination();
        }
        if (Input.GetKeyDown(KeyCode.H)) ChangeRouteSetting();
    }

    /// <summary>
    /// 次の目的地を設定
    /// </summary>
    private void SetNextDestination()
    {
        if (routeSettings == null || routeSettings.Length == 0) return;

        // シーケンス内の次のポイントへ移動
        if (!first && isOnSwitch) nowRouteID++; //スイッチがオン
        else if (!first && !isOnSwitch) nowRouteID--; //スイッチがオフ

        if (nowRouteID >= routeNum) nowRouteID = 0; //最後のポイントに到達したら最初の場所に
        if (nowRouteID < 0) nowRouteID = routeNum; //最初のポイントに到達したら最後の場所に

        Vector3 nextDestination = routeSettings[routeSetting].MovePointList[nowRouteID].position;
        agent.SetDestination(nextDestination);
        Debug.Log($"次の目的地を設定しました: {nextDestination}");
        if(first) first = false;
    }
    /// <summary>
    ///ルート変更
    /// </summary>
    private void ChangeRouteSetting()
    {
        routeSetting++;　//ここは今後変わる可能性大
        routeNum = routeSettings[routeSetting].RouteNum;
        GetMinDistance();
        nowRouteID--;
    }
    /// <summary>
    /// 目的地の一番近い場所を取得
    /// </summary>
    private void GetMinDistance()
    {
        distanceDestination.Clear();
        for(int i =0; i < routeNum; i++)
        {
            distanceDestination.Add(Distance(this.gameObject.transform.position, routeSettings[routeSetting].MovePointList[i].position));
        }
        float _minDistance = distanceDestination.Min();
        int _minIndex = distanceDestination.IndexOf(_minDistance);
        nowRouteID = _minIndex;
    }
    /// <summary>
    /// 距離を測る
    /// </summary>
    /// <param name="_roombaPos"></param>
    /// <param name="_destinationPos"></param>
    /// <returns></returns>
    public float Distance(Vector3 _roombaPos, Vector3 _destinationPos)
    {
        Vector3 _startingPoint = _roombaPos;
        Vector3 _endPoint = _destinationPos;

        return Vector3.Distance(_startingPoint, _endPoint);
    }
    /// <summary>
    /// 経路の地点管理クラス
    /// </summary>
    [System.Serializable]
    public class RouteSettings
    {
        [Header("ルンバの経路")]
        [SerializeField] private Transform[] movePointList; //地点の格納
        public Transform[] MovePointList { get => movePointList; private set => movePointList = value; }
        public int RouteNum { get => movePointList.Length;}
    }
}
