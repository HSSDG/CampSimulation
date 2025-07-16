using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager_HW : MonoBehaviour
{
    // 메인 카메라 하위 UI
    public GameObject UI_NoticeBar; // 안내바 - 여기가 나을까싶어서 넣어봄
    public GameObject UI_SpeechBubble; // 말풍선
    public GameObject UI_CorrectFeedback; // 정답
    public GameObject UI_FailFeedback; // 실패

    // XR Canvas 하위 UI (하나씩만 보이게 전환해서 사용)
    public GameObject UI_ScenarioDialog; // 시나리오
    public GameObject UI_ChoiceButtonA; // 선택a
    public GameObject UI_ChoiceButtonB; // 선택b

    public GameObject UI_NextButton;    // 다음 씬 버튼

    // Text 연결
    public Text dialogText_Scenario;
    public Text dialogText_SpeechBubble;
    public Text dialogText_Notice;
    public Text dialogText_Correct;
    public Text dialogText_Fail;
    // DialogManager 연결
    public DialogManager_HW dialogManager;

    // HW
    public GameObject cook;

    // LDY
    public GameObject torch;
    public GameObject grill;
    public GameObject arrow;
    public GameObject sandBag;

    public void HandleEvent(GameEvent gameEvent)
    {
        // HW
        // var component = gameObject.GetComponent<eventObjectOn>();

        if (gameEvent == null) return;

        switch (gameEvent.eventName)
        {
            case "ShowSpeech":
                Debug.Log($"[이벤트 실행] {gameEvent.eventName}");

                // 시나리오 -> 선택지로 전환
                HideAllUI();
                UI_SpeechBubble.SetActive(true);
                dialogManager.SetDialogTextUI(dialogText_SpeechBubble);
                break;

            case "ShowScenario":
                Debug.Log($"[이벤트 실행] {gameEvent.eventName}");

                // 시나리오로 전환
                // UI_NoticeBar.SetActive(false);
                HideAllUI();
                UI_ScenarioDialog.SetActive(true);
                dialogManager.SetDialogTextUI(dialogText_Scenario);
                break;

            case "ShowNotice":
                HideAllUI();
                UI_NoticeBar.SetActive(true);
                dialogManager.SetDialogTextUI(dialogText_Notice);
                break;

            case "ShowCorrect":
                HideAllUI();
                UI_CorrectFeedback.SetActive(true);
                dialogManager.SetDialogTextUI(dialogText_Correct);
                break;

            case "ShowFail":
                HideAllUI();
                UI_FailFeedback.SetActive(true);
                dialogManager.SetDialogTextUI(dialogText_Fail);
                break;

            case "HideAll":
                HideAllUI();
                break;

            case "NextScene":
                // Next 버튼 활성화
                HideAllUI();
                UI_ScenarioDialog.SetActive(true);
                UI_NextButton.SetActive(true);
                break;

            /*
            case "HW_1_Event":
                if (component != null)
                {
                    component.enabled = true; // MonoBehaviour 기반 컴포넌트는 enabled로 활성화
                }
                break;
            case "G_HW_2_Start":
                if (component != null)
                {
                    component.off();
                }
                cook.SetActive(true);
                break;
            */

            case "LDY_ShowTorch":
                // 토치 활성화
                Debug.Log($"Handeling Event: {gameEvent.eventName}");
                LDY_ShowTorch();
                break;

            case "LDY_ShowSandbags":
                // 모래주머니 활성화
                Debug.Log($"Handeling Event: {gameEvent.eventName}");
                LDY_ShowSandbags();
                break;

            default:
                Debug.LogWarning($"[이벤트 없음] Unknown event: {gameEvent.eventName}");
                break;
        }
    }

    void HideAllUI()
    {
        UI_NoticeBar.SetActive(false);
        UI_CorrectFeedback.SetActive(false);
        UI_FailFeedback.SetActive(false);
        UI_ScenarioDialog.SetActive(false);
        UI_SpeechBubble.SetActive(false);
        UI_ChoiceButtonA.SetActive(false);
        UI_ChoiceButtonB.SetActive(false);
        UI_NextButton.SetActive(false);
    }

    // 다음 씬으로 넘어가기
    public void LoadNextScene()
    {
        // 현재 씬의 인덱스를 가져와서 1을 더해 다음 씬으로 이동
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void LDY_ShowTorch()
    {
        torch.SetActive(true);
        grill.SetActive(true);
        arrow.SetActive(true);
    }

    void LDY_ShowSandbags()
    {
        arrow.SetActive(true);
        sandBag.SetActive(true);
    }
}