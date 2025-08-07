using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveTimeAbility : MonoBehaviour
{
    public float NowTime { get; private set; }

    private float nowTimeMax = 12;
    private float nowTimeMin = -12;

    [SerializeField] private float timeMoveSpeed = 0.5f; //���𓮂����X�s�[�h


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //����߂�
        {
            if (NowTime < nowTimeMin)
            {
                TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Normal);
            }
            else
            {
                NowTime -= Time.deltaTime;
                TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Rewinding);
            }
        }
        if (Input.GetMouseButton(1)) //����i�߂�
        {
            if (NowTime > nowTimeMin)
            {
                TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Normal);
            }
            else
            {
                NowTime -= Time.deltaTime; NowTime += Time.deltaTime;
                TimeStateManager.Instance.SetState(TimeStateManager.TimeState.FastForward);
            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) TimeStateManager.Instance.SetState(TimeStateManager.TimeState.Normal); //�}�E�X�𗣂�����
    }
}
