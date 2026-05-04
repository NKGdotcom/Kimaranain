using UnityEngine;

/// <summary>
/// 状態の構造体
/// </summary>
public enum State
{
    GameScene, //ゲームプレイ中
    Pause, //ポーズ中
}

/// <summary>
/// ゲームシーンの現在の状態を管理するクラス
/// </summary>
public class GameSceneState : MonoBehaviour
{
    //パラメータ
    //現在の状態
    private State currentState;

    //シングルトン
    public static GameSceneState Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentState = State.GameScene;
    }

    /// <summary>
    /// ゲームプレイ中
    /// </summary>
    /// <returns></returns>
    public bool IsGameSceneState()
    {
        return currentState == State.GameScene;
    }

    /// <summary>
    /// ポーズ中
    /// </summary>
    /// <returns></returns>
    public bool IsPauseState()
    {
        return currentState == State.Pause;
    }

    /// <summary>
    /// 指定した状態を新しい状態としてセットする
    /// </summary>
    /// <param name="_newState"></param>
    public void SetState(State _newState)
    {
        currentState = _newState;
    }
}
