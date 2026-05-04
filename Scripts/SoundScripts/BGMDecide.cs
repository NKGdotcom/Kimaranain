using UnityEngine;

public class BGMDecide : MonoBehaviour
{
    public Stage KindOfStage { get => stage; }
    [SerializeField] private SoundList bgm;
    public enum Stage
    {
        Park,Way,Kitchen,Living,Room
    }
    [SerializeField] private Stage stage;

    private void Start()
    {
        switch(stage)
        {
            case Stage.Park:
                SoundManager.Instance.PlayBGM(BGMSource.OutsideBGM);
                break;
            case Stage.Way:
                SoundManager.Instance.PlayBGM(BGMSource.OutsideBGM);
                break;
            case Stage.Kitchen:
                SoundManager.Instance.PlayBGM(BGMSource.RoomBGM);
                break;
            case Stage.Living:
                SoundManager.Instance.PlayBGM(BGMSource.RoomBGM);
                break;
            case Stage.Room:
                SoundManager.Instance.PlayBGM(BGMSource.RoomBGM);
                break;
        }
    }
}
