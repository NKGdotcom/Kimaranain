using TMPro;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{
    [Header("���̃N���X")]
    [SerializeField] private GameResult gameResult;
    [Header("�������Ԃɂ���")]
    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private float timeLimit;
    [Header("�X�e�[�W�Љ��")]
    [SerializeField] private float stageIntroWaitTime;

    private bool gameStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       timerTMP.text = timeLimit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStart)
        {
            stageIntroWaitTime -= Time.deltaTime;
            if(stageIntroWaitTime <= 0)
            {
                gameStart = true;
            }
        }
        else
        {
            timeLimit -= Time.deltaTime;
            int _displayTime = Mathf.CeilToInt(timeLimit);
            timerTMP.text = _displayTime.ToString();
            if (timeLimit <= 0)
            {
                gameResult.GameOver();
            }
        }
    }
}
