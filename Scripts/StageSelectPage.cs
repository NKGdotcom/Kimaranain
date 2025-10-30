using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectPage : MonoBehaviour
{
    public bool IsArrowSelect { get; private  set; }
    [SerializeField] private Stage[] onePageStagesList;
    [SerializeField] private Image arrowSelect;
    private int onepageStageNum = 0;
    private int stageSelectIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (onePageStagesList.Length == 0)
        {
            Debug.LogError("1ページあたりに設定しているステージがありません");
        }
        else if ( onePageStagesList.Length >= 1)
        {
            onepageStageNum = onePageStagesList.Length - 1;
            stageSelectIndex = 0;
            onePageStagesList[stageSelectIndex].StageSelect();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            NextStageSelect();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousStageSelect();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (!IsArrowSelect)
            {
                onePageStagesList[stageSelectIndex].StageTransition();
            }
        }
    }
    public void NextStageSelect()
    {
        if (IsArrowSelect)
        {
            stageSelectIndex = 0;
            ArrowDeselect();
            onePageStagesList[stageSelectIndex].StageSelect();
        }
        else if (stageSelectIndex == onepageStageNum)
        {
            ArrowSelect();
            onePageStagesList[stageSelectIndex].StageDeselect();
        }
        else
        {
            onePageStagesList[stageSelectIndex].StageDeselect();
            stageSelectIndex++;
            onePageStagesList[stageSelectIndex].StageSelect();
        }
    }
    public void PreviousStageSelect()
    {
        if (IsArrowSelect)
        {
            stageSelectIndex = onepageStageNum;
            ArrowDeselect();
            onePageStagesList[stageSelectIndex].StageSelect();
        }
        else if (stageSelectIndex == 0)
        {
            ArrowSelect();
            onePageStagesList[stageSelectIndex].StageDeselect();
        }
        else
        {
            onePageStagesList[stageSelectIndex].StageDeselect();
            stageSelectIndex--;
            onePageStagesList[stageSelectIndex].StageSelect();
        }
    }
    public void ArrowSelect()
    {
        IsArrowSelect = true;
        arrowSelect.enabled = true;
    }
    public void ArrowDeselect()
    {
        IsArrowSelect = false;
        arrowSelect.enabled = false;
    }
}
