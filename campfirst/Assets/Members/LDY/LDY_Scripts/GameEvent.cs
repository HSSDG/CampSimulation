using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public string eventName;
    public void Trigger()
    {
        Debug.Log($"Triggering Event: {eventName}");
        // 실제 구현은 이벤트 매니저가 이걸 감지하고 행동하도록
    }
}
