

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ステージ選択を統括するクラス
/// </summary>
public class StageSelectPageManager : MonoBehaviour
{
    [Header("オブジェクト参照")]
    [Tooltip("ステージのページリスト")]
    [SerializeField] private List<GameObject> stagePagesList;

    [Header("コンポーネント参照")]
    [Tooltip("次のページに進むボタン")]
    [SerializeField] private HoverWhiteButton nextPageButton;
    [Tooltip("前のページに戻るボタン")]
    [SerializeField] private HoverWhiteButton prevPageButton;

    //パラメータ
    //現在のステージ選択ページ
    private int nowPageIndex = 0;
    //最大のステージ選択ページ
    private int maxPageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(stagePagesList == null) { Debug.LogError("stagePagesListが参照されていません"); return; }
        if(nextPageButton == null) { Debug.LogError("nextPageButtonが参照されていません"); return; }
        if(prevPageButton == null) { Debug.LogError("prevPageButtonが参照されていません"); return; }

        maxPageIndex = stagePagesList.Count - 1;

        prevPageButton.OnClicked += PrevPage;
        nextPageButton.OnClicked += NextPage;
    }

    private void OnEnable()
    {
        ShowPage(0);
    }

    private void OnDestroy()
    {
        prevPageButton.OnClicked -= PrevPage;
        nextPageButton.OnClicked -= NextPage;
    }

    /// <summary>
    /// 次のページに進む
    /// </summary>
    private void NextPage()
    {
        ShowPage(nowPageIndex++);
    }

    /// <summary>
    /// 前のページに戻る
    /// </summary>
    private void PrevPage()
    {
        ShowPage(nowPageIndex--);
    }

    /// <summary>
    /// 指定したステージ選択のページを表示
    /// </summary>
    /// <param name="_index"></param>
    private void ShowPage(int _index)
    {
        if (!prevPageButton.gameObject.activeSelf) { prevPageButton.gameObject.SetActive(true); }
        if (!nextPageButton.gameObject.activeSelf) { nextPageButton.gameObject.SetActive(true); }

        stagePagesList[nowPageIndex].SetActive(false);
        nowPageIndex = _index;
        stagePagesList[nowPageIndex].SetActive(false);

        if(nowPageIndex <= 0) { prevPageButton.gameObject.SetActive(false); }
        if(nowPageIndex >= maxPageIndex - 1) { nextPageButton.gameObject.SetActive(false);}
    }
}
