using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ロボットの進むルートを設定
/// </summary>
[System.Serializable]
public class Route
{
    [Header("ロボット掃除機の経路")]
    [SerializeField] private Transform[] movePointList; //地点の格納
    public Transform[] MovePointList { get => movePointList; private set => movePointList = value; }
    public int RouteNum { get => movePointList.Length; }
}

/// <summary>
/// ロボット掃除機の経路に関する処理
/// </summary>
public class CleanerRouteManager : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("ロボット掃除機の周回ルート")]
    [SerializeField] private Route[] routeList;
    [Header("ロボット掃除機の周回ルートを変更するトリガー")]
    [SerializeField] private CleanerChangeRootObject changeRootObject;

    //パラメータ
    //1つの経路探索ルートは何個あるか
    private int routeNum;
    //設定した経路探索ルートの種類の数
    private int routeSettingsNum;
    //経路探索ルートの種類ID
    private int routeTypeID;
    //現在は何個目のルートにいるか
    private int nowRouteID;
    //新しいロボット掃除機のルートのための値
    private const int NEW_CHANGE_ROUTE_NUM = 1;
    //一番近い距離はどこかを調べる
    private List<float> distanceDestination = new List<float>();

    void Awake()
    {
        routeSettingsNum = routeList.Length;
        if (routeSettingsNum == 0)
        {
            Debug.LogError("経路設定が定義されていません。ルンバは移動できません。");
            return;
        }

        routeTypeID = 0;
        if (routeTypeID >= routeSettingsNum || routeList[routeTypeID].MovePointList.Length == 0)
        {
            Debug.LogError("選択されたランダムな経路設定が無効であるか、移動ポイントがありません。");
            return;
        }

        if(changeRootObject == null) { Debug.LogError("changeRootObjectが参照されていません"); return; }

        routeNum = routeList[routeTypeID].RouteNum;
        changeRootObject.OnPlacedTrash += ChangeRouteSet;
    }

    private void OnDestroy()
    {
        changeRootObject.OnPlacedTrash -= ChangeRouteSet;
    }

    /// <summary>
    /// 次の目的地を設定し、ルートを周回するようにする
    /// </summary>
    /// <returns></returns>
    public Vector3 NextDestination()
    {
        if (routeList == null || routeList.Length == 0) return Vector3.zero;
        
        Vector3 _nextDestination = routeList[routeTypeID].MovePointList[nowRouteID].position;

        nowRouteID++;

        if(nowRouteID >= routeNum) nowRouteID = 0;
        if(nowRouteID < 0) nowRouteID = routeNum;

        return _nextDestination;
    }

    /// <summary>
    /// ルートを変更
    /// </summary>
    public void ChangeRouteSet()
    {
        //新しいルートのIDを設定し、変更
        routeTypeID = NEW_CHANGE_ROUTE_NUM;

        //目的地の一番近い場所を取得し、その場所にロボットが進むようにする
        routeNum = routeList[routeTypeID].RouteNum;
        GetMinDistance();
    }

    /// <summary>
    /// 時間を戻す際に元の場所に戻す
    /// </summary>
    public void ResetOriginRoute()
    {
        routeTypeID = 0;
        routeNum = routeList[routeTypeID].RouteNum;
    }

    /// <summary>
    /// 目的地の一番近い場所を取得
    /// </summary>
    private void GetMinDistance()
    {
        distanceDestination.Clear();
        for (int i = 0; i < routeNum; i++)
        {
            distanceDestination.Add(Distance(this.gameObject.transform.position,
                routeList[routeTypeID].MovePointList[i].position));
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
    private float Distance(Vector3 _roombaPos, Vector3 _destinationPos)
    {
        Vector3 _startingPoint = _roombaPos;
        Vector3 _endPoint = _destinationPos;

        return Vector3.Distance(_startingPoint, _endPoint);
    }
}
