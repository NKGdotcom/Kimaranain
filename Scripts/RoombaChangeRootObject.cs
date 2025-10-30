using System.Collections;
using UnityEngine;

public class RoombaChangeRootObject : MonoBehaviour
{
    [SerializeField] private Roomba roombaScripts;
    private float waitRBFreezeTime = 0.5f; //�������~�߂�̂ɂ����鎞��
    public bool isPlaced;
    public bool IsPlaced => isPlaced;
    private Animator trashAnimator;
    private float waitTime = 1.0f;
    private float time = 0;
    public GameObject Trash;
    public GameObject RootChangeText;
    private bool previousRewindState = false;

    private void Start()
    {
        RootChangeText.SetActive(false);

        if (roombaScripts == null)
        {
            enabled = false;
            return;
        }
    }
    private void Update()
    {
        bool currentRewindState = TimeStateManager.Instance.IsRewindingState();

        // 巻き戻し状態に切り替わった瞬間だけ実行
        if (!previousRewindState && currentRewindState)
        {
            Vector3 resetPosition = new Vector3(7f, 5.888f, 0f);
            Trash.transform.position = resetPosition;
        }

        previousRewindState = currentRewindState;


        if (isPlaced && TimeStateManager.Instance.IsRewindingState() /*&& Input.GetMouseButton(0)*/)
        {
            isPlaced = false;
            trashAnimator.enabled = true;
            trashAnimator.SetTrigger("RewindingTrash");
            roombaScripts.BackToOriginalRoute();
            StartCoroutine(WaitAnimation());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Trash")
        {
            if (trashAnimator == null)
                trashAnimator = other.gameObject.GetComponent<Animator>();

            Debug.Log("ChangeRoombaRoot");
            isPlaced = true;

            // RootChangeTextを1秒だけ表示
            if (RootChangeText != null)
            {
                RootChangeText.SetActive(true);
                StartCoroutine(HideRootChangeTextAfterDelay());
            }

            Rigidbody _trashRb = other.gameObject.GetComponent<Rigidbody>();
            if (_trashRb == null) return;

            _trashRb.linearVelocity = Vector3.zero;
            _trashRb.angularVelocity = Vector3.zero;
            _trashRb.isKinematic = true;

            roombaScripts.ChangeRouteSetting();
        }
    }

    private IEnumerator HideRootChangeTextAfterDelay()
    {
        yield return new WaitForSeconds(3.0f);
        RootChangeText.SetActive(false);
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(waitTime);
        trashAnimator.enabled = false;
        yield break;
    }
}
