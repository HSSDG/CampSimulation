using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class DialogManager_DY : MonoBehaviour
{
    public Text dialogTextUI;
    public Button[] choiceButtons;
    public EventManager_DY eventManager;

    private DialogNode_HW currentNode;    
    private bool showingWorngNode = false;  // 오답 노드를 보여주는 중인지 체크
    private DialogNode_HW nodeBeforeWrong;     // 오답 이전의 노드 저장

    public InputActionReference continueAction;
    private bool waitingForContinue = false;    // 컨트롤러 버튼 클릭 여부

    public DialogNode_HW firstNode;    // 맨 처음 실행 할 노드
    
    private bool isWaitingMissionSuccess = false; // 성공할 때까지 대기
    
    public DialogNode_HW[] allNodesToReset;  // 인스펙터에서 원본 노드들 등록

    private AudioSource soundPlayer;
    public AudioClip buttonSound;
    public AudioClip correctSound;
    public AudioClip failSound;

    // 추가 ******************************
    public void ResetDialogNodes()
    {
        foreach (var node in allNodesToReset)
        {
            node.requiresMissionSuccess = true;
            // 다른 필드 초기화도 필요시 여기에 추가
        }
    }
    // ************************************

    void MakeChoice(int index)
    {
        var choice = currentNode.choices[index];
        if (choice.isCorrect)
        {
            soundPlayer.clip = correctSound;
            soundPlayer.Play();
            currentNode = choice.nextNode;
            ShowNode();
        }
        else
        {
            // 오답 처리: 오답 노드를 보여주고, 끝나면 다시 원래 노드(선택지)로 돌아감
            if (currentNode.worngAnswerNode != null)
            {
                soundPlayer.clip = failSound;
                soundPlayer.Play();
                nodeBeforeWrong = currentNode;
                currentNode = currentNode.worngAnswerNode;
                showingWorngNode = true;
                ShowNode();
            }
        }
    }

    // ShowNode()에서 오답 노드 처리: 다음 노드가 이전 선택지 노드라면, 다시 돌아가게 함
    void ShowNode()
    {
        // 스크립트 노드가 없는 경우
        if (currentNode == null)
        {
            dialogTextUI.text = "스크립트가 존재하지 않습니다";
            foreach (var btn in choiceButtons)
            {
                btn.gameObject.SetActive(false);
            }
            return;
        }

        // GameEvent: 이벤트 발생 - 이벤트 먼저 발생하게 수정 HW
        if (currentNode.eventToTriggers != null)
        {
            foreach (var o in currentNode.eventToTriggers)
            {
                eventManager.HandleEvent(o);
            }
        }

        dialogTextUI.text = currentNode.dialogText;
        
        // 선택지가 있는 경우 버튼 활성화
        if (currentNode.hasChoices)
        {
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentNode.choices.Length)
                {
                    choiceButtons[i].gameObject.SetActive(true);
                    int index = i;
                    choiceButtons[i].GetComponentInChildren<Text>().text = currentNode.choices[i].choiceText;
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() => MakeChoice(index));
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else    // 선택지가 없는 경우 버튼 비활성화
        {
            foreach (var btn in choiceButtons)
            {
                btn.gameObject.SetActive(false);
            }

            waitingForContinue = true;
        }
    }

    // 실행
    void Start()
    {
        ResetDialogNodes(); // << 추가한 부분 ******************************************
        StartDialog(firstNode);
        soundPlayer = GetComponent<AudioSource>();
    }

    public void StartDialog(DialogNode_HW startNode)
    {
        currentNode = startNode;
        ShowNode();
    }

    // 컨트롤러 버튼 클릭 여부 확인
    void Update()
    {
        if (!waitingForContinue) return;

        // 테스트용 키보드 입력(엔터키)
        // if (Input.GetKeyDown(KeyCode.Return))
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("버튼 눌림");
            AdvanceDialog();
        }

        // XR 버튼 입력 (아무 버튼)
        if (continueAction != null && continueAction.action.WasPressedThisFrame())
        {
            Debug.Log("버튼 눌림");
            AdvanceDialog();
        }

        /*  만일 위 코드가 안 된다면 에셋스토어에서 Oculus Input 다운 받아 아래 코드 쓰기
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("버튼 눌림");
            AdvanceDialog();
        }
        */
    }

    // 다음 노드로 이동하는 함수 (버튼 입력 시 호출)
    public void AdvanceDialog()
    {
        // 미션 완료 조건인 경우엔 대기
        if (currentNode.requiresMissionSuccess)
        {
            isWaitingMissionSuccess = true;
            Debug.Log("AdvanceDialog() 대기 중: 미션 성공 필요");
            return;
        }

        ContinueToNextNode();
    }

    // text 갈아끼우기
    public void SetDialogTextUI(Text newTextUI)
    {
        dialogTextUI = newTextUI;
    }

    void ContinueToNextNode()
    {
        waitingForContinue = false;

        if (showingWorngNode)
        {
            showingWorngNode = false;
            currentNode = nodeBeforeWrong;
        }
        else
        {
            currentNode = currentNode.nextNodeIfNoChoice;
        }

        soundPlayer.clip = buttonSound;
        soundPlayer.Play();
        ShowNode();
    }
}
