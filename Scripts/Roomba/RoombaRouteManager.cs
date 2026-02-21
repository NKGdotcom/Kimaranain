using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Roomba;

public class RoombaRouteManager : MonoBehaviour
{
    [System.Serializable]
    public class Route
    {
        [Header("ルンバの経路")]
        [SerializeField] private Transform[] movePointList; //地点の格納
        public Transform[] MovePointList { get => movePointList; private set => movePointList = value; }
        public int RouteNum { get => movePointList.Length; }
    }

    [SerializeField] private Route[] routeList;

    private int routeNum; //1つの経路探索は何個ルートあるか
    private int routeSettingsNum; //設定したルートの種類の数
    private int routeTypeID; //ルートの種類ID
    private int nowRouteID; //現在は何個目のルートにいるか

    private const int CORRECT_CHANGE_ROUTE_NUM = 1; //正しいルンバのルートのための値
    private const int IN_CORRECT_CHANGE_ROUTE_NUM = 2; //間違ったルンバのルートのための値

    [Header("正しいルンバのルートID")]
    [SerializeField] private int correctRouteMinID;
    [SerializeField] private int correctRouteMaxID;
    [Header("間違ったルンバのルートID")]
    [SerializeField] private int incorrectRouteMinID;
    [SerializeField] private int incorrectRouteMaxID;

    private List<float> distanceDestination = new List<float>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        routeNum = routeList[routeTypeID].RouteNum;
    }

    //次の目的地
    public Vector3 NextDestination()
    {
        if (routeList == null || routeList.Length == 0) return Vector3.zero;
        
        Debug.Log($"現在のルートタイプID: {routeTypeID}, 現在のルートID: {nowRouteID}");
        Vector3 _nextDestination = routeList[routeTypeID].MovePointList[nowRouteID].position;

        nowRouteID++;

        if(nowRouteID >= routeNum) nowRouteID = 0;
        if(nowRouteID < 0) nowRouteID = routeNum;

        return _nextDestination;
    }

    //ルート変更
    public void ChangeRouteSet()
    {
        if(IsCorrectRoombaRoute())
        {
            routeTypeID = CORRECT_CHANGE_ROUTE_NUM;
        }
        else
        {
            routeTypeID = IN_CORRECT_CHANGE_ROUTE_NUM;
        }

        routeNum = routeList[routeTypeID].RouteNum;
        GetMinDistance();
    }

    //目的地の一番近い場所を取得
    private void GetMinDistance()
    {
        distanceDestination.Clear();
        for(int i = 0; i < routeNum; i++)
        {
            distanceDestination.Add(Distance(this.gameObject.transform.position, 
                routeList[routeTypeID].MovePointList[i].position));
        }

        float _minDistance = distanceDestination.Min();
        int _minIndex = distanceDestination.IndexOf(_minDistance);
        nowRouteID = _minIndex;
    }

    //距離を測る
    public float Distance(Vector3 _roombaPos, Vector3 _destinationPos)
    {
        Vector3 _startingPoint = _roombaPos;
        Vector3 _endPoint = _destinationPos;

        return Vector3.Distance(_startingPoint, _endPoint);
    }

    //ルートを変えたとき
    public bool IsCorrectRoombaRoute()
    {
        if (nowRouteID >= correctRouteMinID && nowRouteID <= correctRouteMaxID)
        {
            Debug.Log("上手くルートを変更することができました");
            return true;
        }
        else if (nowRouteID >= incorrectRouteMinID && nowRouteID <= incorrectRouteMaxID)
        {
            Debug.Log("ルートの変更に失敗しました");
            return false;
        }
        else
        {
            Debug.LogError("ルンバのルートIDが正しい範囲にありません");
            return false;
        }
    }

    public void OriginRoute()
    {
        routeTypeID = 0;
        routeNum = routeList[routeTypeID].RouteNum;
    }
}
