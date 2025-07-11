using UnityEngine;

public class EventManager : MonoBehaviour
{
    public void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent == null) return;

        switch (gameEvent.eventName)
        {
            case "ActivateFirepit":
                // 화로에 불 붙이기
                Debug.Log($"Handeling Event: {gameEvent.eventName}");
                break;
            case "ShowSandbags":
                // 모래주머니 활성화
                Debug.Log($"Handeling Event: {gameEvent.eventName}");
                break;
            default:
                Debug.LogWarning($"Unknown event: {gameEvent.eventName}");
                break;
        }
    }
}
