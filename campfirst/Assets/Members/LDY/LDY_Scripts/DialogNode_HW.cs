using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode_HW", menuName = "Scriptable Objects/DialogNode_HW")]
public class DialogNode_HW : ScriptableObject
{
    [TextArea(2, 5)]
    public string dialogText;

    public bool hasChoices;
    public ChoiceOption[] choices;
    public GameEvent[] eventToTriggers;    // 상황 변화 이벤트 (null이면 없음)
    public DialogNode_HW nextNodeIfNoChoice;   // 선택지 없는 경우, 다음 노드
    public DialogNode_HW worngAnswerNode;  // 오답 선택 시 보여줄 노드

    public bool requiresMissionSuccess = false; // 미션 성공 여부 

    [System.Serializable]   // 인스펙터에서 배열을 편집하려면 필요함
    public class ChoiceOption
    {
        public string choiceText;
        public bool isCorrect;  // 선택지가 정답인지 오답인지 체크
        public DialogNode_HW nextNode; // 정답일 때 이동할 노드 
    }
}

