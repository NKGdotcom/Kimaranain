using NUnit.Framework;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private StageSelectPage[] stageSelectPages;
    private int stageSelectPageMax;
    private int stageSelectPageIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageSelectPageMax = stageSelectPages.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(stageSelectPages[stageSelectPageIndex].IsArrowSelect)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(stageSelectPageIndex == 0)
                {
                    stageSelectPages[stageSelectPageIndex].ArrowDeselect();
                    stageSelectPages[stageSelectPageIndex].gameObject.SetActive(false);
                    stageSelectPageIndex++;
                    stageSelectPages[stageSelectPageIndex].gameObject.SetActive(true);
                }
                else if(stageSelectPageIndex == stageSelectPageMax)
                {
                    stageSelectPages[stageSelectPageIndex].ArrowDeselect();
                    stageSelectPages[stageSelectPageIndex].gameObject.SetActive(false);
                    stageSelectPageIndex--;
                    stageSelectPages[stageSelectPageIndex].gameObject.SetActive(true);
                }
                
            }
        }
    }
}
