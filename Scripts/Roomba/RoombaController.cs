using UnityEngine;

public class RoombaController : MonoBehaviour
{
    [SerializeField] private RoombaMovement movement;
    [SerializeField] private RoombaRouteManager routeManager;
    [SerializeField] private RoombaInteraction interaction;
    private float destinationCooldown = 0.5f;
    private float destinationTimer = 0f;

    [SerializeField] private float roombaSEInterval = 2.0f; // SEを鳴らす間隔（秒
    private float roombaSETimer = 0f;

    private bool isPower; //電源
    private Vector3 idlePos;

    void Awake()
    {
        idlePos = transform.localPosition;
        destinationTimer = destinationCooldown;

        if (movement == null) { Debug.LogError("RoombaMovementがアサインされていません。"); return; }
        if (routeManager == null) { Debug.LogError("RoombaRouteManagerがアサインされていません。"); return; }
        if (interaction == null) { Debug.LogError("RoombaInteractionがアサインされていません。"); return; }

        interaction.OnInteractionStart += TurnOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPower) return;

        if(TimeStateManager.Instance.IsRewindingState())
        {
            TurnOff();
            return;
        }

        if (movement.IsMoving())
        {
            roombaSETimer -= Time.deltaTime;
            if (roombaSETimer <= 0f)
            {
                SoundManager.Instance.RoombaSE();
                roombaSETimer = roombaSEInterval;
            }
        }
        else
        {
            roombaSETimer = roombaSEInterval; // 停止中ならタイマーリセット
        }

        if(destinationTimer > 0f)
        {
            destinationTimer -= Time.deltaTime;
        }

        else if (movement.IsArrived())
        {
            Vector3 _targetPos = routeManager.NextDestination();
            movement.SetDestination(_targetPos);

            destinationTimer = destinationCooldown;
        }
    }

    //ルンバの電源オン
    private void TurnOn()
    {
        isPower = true;

        Vector3 _firstPos = routeManager.NextDestination();
        movement.SetDestination(_firstPos);

        destinationTimer = destinationCooldown;
    }

    //ルンバの電源オフ
    private void TurnOff()
    {
        isPower = false;
        movement.StopMovement(idlePos);
        interaction.ResetInteract();
    }
}
