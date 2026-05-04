using UnityEngine;

/// <summary>
/// ”µŒv‚ğŠÇ—‚·‚éƒNƒ‰ƒX
/// </summary>
public class ClockGimmick : Item
{
    public bool canOpenClock;
    public GameObject Key;
    public GameObject BatteryImage;

    public bool HasSetBattery = false;
    private bool hasKeyPopped = false;

    private Vector3 keyStartPos;
    private Vector3 keyTargetPos;
    private float keyMoveTime = 0.3f;
    private float keyMoveTimer = 0f;
    private bool isKeyMoving = false;

    public GameObject SetText;

    public GameObject[] Door;

    public void Start()
    {
        SetText.SetActive(false);
        Key.SetActive(false);

        // Door[0] ‚Æ Door[1] ‚ğ”CˆÓ‚ÌŠp“x‚É‰ñ“]‚³‚¹‚é
        if (Door.Length >= 2)
        {
            Quaternion targetRotation0 = Quaternion.Euler(0f, -90f, 180f);
            Quaternion targetRotation1 = Quaternion.Euler(0f, 90f, 0f);
            Door[0].transform.rotation = targetRotation0;
            Door[1].transform.rotation = targetRotation1;
        }
    }

    private new void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TimeStateManager.Instance.IsNormalState() && HasSetBattery == false)
            {
                SetText.SetActive(true);

                if (canOpenClock && Input.GetKeyDown(KeyCode.E))
                {
                    HasSetBattery = true;
                    SetText.SetActive(false);
                    BatteryImage.SetActive(false);
                    SoundManager.Instance.OtherThanKeySE();
                }
            }

            if (!TimeStateManager.Instance.IsNormalState())
            {
                SetText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetText.SetActive(false);
        }
    }

    public void CanOpenClock()
    {
        canOpenClock = true;
    }
    /*
    private void Update()
    {
        if (HasSetBattery && TimeStateManager.Instance.IsFastForwardState() && !hasKeyPopped)
        {
            SoundManager.Instance.OpenClockSE();

            StartPopOutKey();
            
            if (!Item.triggered)
            {
                Key.SetActive(true);
            }
        }
        
        if (HasSetBattery && TimeStateManager.Instance.IsNormalState() && hasKeyPopped)
        {
            Key.transform.position = new Vector3(-10.5f, 7.15f, 3.1f);
            hasKeyPopped = false;
            Key.SetActive(false);

            if (Door.Length >= 2)
            {
                Quaternion targetRotation0 = Quaternion.Euler(0f, -90f, 180f);
                Quaternion targetRotation1 = Quaternion.Euler(0f, 90f, 0f);
                Door[0].transform.rotation = targetRotation0;
                Door[1].transform.rotation = targetRotation1;
            }
        }

        if (HasSetBattery && TimeStateManager.Instance.IsRewindingState() && hasKeyPopped)
        {
            Key.transform.position = new Vector3(-10.5f, 7.15f, 3.1f);
            hasKeyPopped = false;
            Key.SetActive(false);

            if (Door.Length >= 2)
            {
                Quaternion targetRotation0 = Quaternion.Euler(0f, -90f, 180f);
                Quaternion targetRotation1 = Quaternion.Euler(0f, 90f, 0f);
                Door[0].transform.rotation = targetRotation0;
                Door[1].transform.rotation = targetRotation1;
            }
        }

        if (isKeyMoving && Key != null)
        {
            keyMoveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(keyMoveTimer / keyMoveTime);
            Key.transform.position = Vector3.Lerp(keyStartPos, keyTargetPos, t);

            if (t >= 1f)
            {
                isKeyMoving = false;
            }
        }
    }*/

    private void StartPopOutKey()
    {
        if (Key == null) return;

        keyStartPos = Key.transform.position;
        keyTargetPos = keyStartPos + transform.up * 1.5f + transform.forward * 2.3f;
        keyMoveTimer = 0f;
        isKeyMoving = true;

        hasKeyPopped = true;

        // Door[0] ‚Æ Door[1] ‚ğ”CˆÓ‚ÌŠp“x‚É‰ñ“]‚³‚¹‚é
        if (Door.Length >= 2)
        {
            Quaternion targetRotation0 = Quaternion.Euler(0f, -10f, 180f);
            Quaternion targetRotation1 = Quaternion.Euler(0f, 10f, 0f);
            Door[0].transform.rotation = targetRotation0;
            Door[1].transform.rotation = targetRotation1;
        }
    }

}