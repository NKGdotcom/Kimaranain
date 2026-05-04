using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ある位置(このスクリプトを設定するオブジェクトの位置)にオブジェクトが置かれた場合、ルートを変更
/// </summary>
public class CleanerChangeRootObject : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("ルートが変更した際に表示するテキスト")]
    [SerializeField] private Text rootChangeText;

    //パラメータ
    //UIを表示させる時間
    private float waitShowUITime = 1.0f;

    //イベント
    //ごみが置かれたことを検知
    public event Action OnPlacedTrash;

    private void Awake()
    {
        if(rootChangeText == null) { Debug.LogError("rootChangeTextが参照されていません"); return; }

        rootChangeText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //ごみが触れたら検知し、知らせる
        OnPlacedTrash?.Invoke();

        StartCoroutine(ShowUIAsync());
    }

    /// <summary>
    /// 一定時間UIを表示し、その後非表示に
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowUIAsync()
    {
        rootChangeText.enabled = true;
        yield return new WaitForSeconds(waitShowUITime);
        rootChangeText.enabled = false;
        yield break;
    }
}
