using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode", menuName = "Scriptable Objects/DialogNode")]
public class DialogNode : ScriptableObject
{
    public string dialogText;
    public bool hasChoices;
    public ChoiceOption[] choices;
    public GameEvent eventToTrigger;    // 상황 변화 이벤트 (null이면 없음)
    public DialogNode nextNodeIfNoChoice;   // 선택지 없는 경우, 다음 노드
    public DialogNode worngAnswerNode;  // 오답 선택 시 보여줄 노드
}

[System.Serializable]   // 인스펙터에서 배열을 편집하려면 필요함
public class ChoiceOption
{
    public string choiceText;
    public bool isCorrect;  // 선택지가 정답인지 오답인지 체크
    public DialogNode nextNode; // 정답일 때 이동할 노드 
}
