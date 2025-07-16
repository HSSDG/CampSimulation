using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject torch;
    public GameObject grill;
    public GameObject arrow;
    public GameObject sandBag;
    public void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent == null) return;

        switch (gameEvent.eventName)
        {
            case "ShowTorch":
                // 토치 활성화
                torch.SetActive(true);
                grill.SetActive(true);
                arrow.SetActive(true);
                break;
            case "ActivateFirepit":
                // 화로에 불 붙이기
                Debug.Log($"Handeling Event: {gameEvent.eventName}");
                break;
            case "ShowSandbags":
                // 모래주머니 활성화
                Debug.Log($"Handeling Event: {gameEvent.eventName}");
                arrow.SetActive(true);
                sandBag.SetActive(true);
                break;
            default:
                Debug.LogWarning($"Unknown event: {gameEvent.eventName}");
                break;
        }
    }
}
