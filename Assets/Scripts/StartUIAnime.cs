using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartUIAnime : MonoBehaviour
{
    #region Variable
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // 이벤트 적용을 위한 인스턴스 클래스 생성
    public After_ Next_;

    [SerializeField]
    private GameObject Title;
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private Animator UI_animator;

    [Space(10f)]
    [SerializeField]
    private Animator BananaMan_;                                                                // BananaMan 관련 변수
    [SerializeField]
    private Button BananaManActingButton;
    private string Now_Action;

    [Space(10f)]
    [SerializeField]
    private Button Start_Button;
    [SerializeField]
    private Button Option_Button;
    [SerializeField]
    private Button Apply_Button;
    [SerializeField]
    private Button Exit_Button;
    [SerializeField]
    private Button NewGame_Button;
    [SerializeField]
    private Button Continue_Button;

    [Space(10f)]
    [SerializeField]
    private Slider BGM_S;
    [SerializeField]
    private Slider SFX_S;

    [Space(10f)]
    [SerializeField]
    private Camera Main;
    [SerializeField]
    private Dropdown resoultion_drop;
    [SerializeField]
    private Toggle screen_mode;

    [Space(10f)]
    [Tooltip("ClearCanvas")]
    [SerializeField]
    private GameObject ClearCanvas;

    [Space(10f)]
    [Tooltip("Continue")]
    [SerializeField]
    private bool ContinueGame;
    #endregion
    private void Awake()
    {
        UI_animator.gameObject.SetActive(false);
        BananaManActingButton.gameObject.SetActive(false);                                      // 버튼 비활성화로 애니메이션 끝날때까지 불필요한 동작 미 방지
        Main = Camera.main;

        try
        {
            SoundManager.Instance.StopAllSoundBGM();
            SoundManager.Instance.StopAllSoundEffect();
        }
        catch (NullReferenceException) { }

        Buttons_Interactable_false();
    }
    /// <summary>
    /// 초기 이벤트추가로 설정해야할 함수
    /// </summary>
    public void Init_Event()
    {
        UI_animator.gameObject.SetActive(true);
        Start_Button.onClick.AddListener(StartButton_Init);
        BindingPanel.instance.Init();
    }
    /// <summary>
    /// 버튼 비활성화
    /// </summary>
    private void Buttons_Interactable_false()
    {
        Start_Button.interactable = false;
        Option_Button.interactable = false;
        Exit_Button.interactable = false;
    }
    /// <summary>
    /// 버튼 활성화 및 이벤트 추가
    /// </summary>
    private void Buttons_Interactable_true()
    {
        Start_Button.interactable = true;
        Option_Button.interactable = true;
        Exit_Button.interactable = true;


        Option_AddListener();
        Get_Exit_Buttons_Event();
    }
    /// <summary>
    /// 나머지 버튼들 및 조작법 On
    /// </summary>
    public void On_etc()
    {
        UI_animator.SetTrigger("Etc");
    }
    /// <summary>
    /// 버튼들 활성화 및 시작 애니메이터 더 이상 필요 없기에 비활성화
    /// </summary>
    public void UI_Off()
    {
        Buttons_Interactable_true();

        UI_animator.enabled = false;
    }
    /// <summary>
    /// 첫 시작시 캐릭터 인사 모션
    /// </summary>
    public void Banana_Hi()
    {
        BananaManActingButton.gameObject.SetActive(true);
        BananaManActingButton.interactable = false;

        int random_value = 4;
        Now_Action = "Standing Greeting";

        BananaMan_.SetInteger("Acting_value", random_value);
        StartCoroutine(nameof(AnimationOverCheck));

        On_etc();
    }
    /// <summary>
    /// 랜덤 모션을 위한 함수
    /// </summary>
    public void BananaMan_Random_Acting()
    {
        BananaManActingButton.interactable = false;                                                         // 불필요한 추가 동작 없애기 위해 버튼 비활성화

        int random_value = UnityEngine.Random.Range(1, 10);

        switch (random_value)                                                           // 현재 활성화 되고 있는 모션이 Idle인지 아닌지 확인을 위해 string 변수에 모션 이름 저장
        {
            case 1:
                Now_Action = "Dismissing Gesture";
                break;
            case 2:
                Now_Action = "Yawn";
                break;
            case 3:
                Now_Action = "Punching Bag";
                break;
            case 4:
                Now_Action = "Standing Greeting";
                break;
            case 5:
                Now_Action = "Surprised";
                break;
            case 6:
                Now_Action = "Telling A Secret";
                break;
            case 7:
                Now_Action = "Wave Hip Hop Dance";
                break;
            case 8:
                Now_Action = "Reacting";
                break;
            case 9:
                Now_Action = "Pointing";
                break;
        }

        BananaMan_.SetInteger("Acting_value", random_value);
        StartCoroutine(nameof(AnimationOverCheck));
    }
    /// <summary>
    /// 애니메이션 상태 확인, 종료 됬다면 Idle로 모션 Return
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimationOverCheck()
    {
        yield return new WaitForSeconds(0.1f);

        while (!string.Equals(BananaMan_.GetCurrentAnimatorStateInfo(0).IsName("Happy Idle"),Now_Action) &&
            BananaMan_.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        Now_Action = string.Empty;
        BananaMan_.SetInteger("Acting_value", 0);
        StartCoroutine(nameof(Return));
    }
    /// <summary>
    /// 일반 Idle 모션 으로 돌아왔을 시 버튼 활성화 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator Return()
    {
        while (BananaMan_.GetCurrentAnimatorStateInfo(0).IsName("Happy Idle") &&
    BananaMan_.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        BananaManActingButton.interactable = true;
    }
    /// <summary>
    /// Exit 이벤트 추가
    /// </summary>
    private void Get_Exit_Buttons_Event()
    {
        Exit_Button.onClick.AddListener(Utils.Instance.Exit_Program);
    }
    /// <summary>
    /// Option 관련 이벤트 추가
    /// </summary>
    private void Option_AddListener()
    {
        SoundManager.Instance.GetSliders(BGM_S, SFX_S);                                                     // 사운드 슬라이더 할당

        Utils.Instance.GetComponent<ViewOption>().Set_Variable(resoultion_drop, Main);                      // 해상도 드롭메뉴, 전체화면 체크, 카메라 할당
        Option_Button.onClick.AddListener(Utils.Instance.GetComponent<ViewOption>().SetResolution);         // Option_Button 클릭 시 현 실행중인 모니터 기준 해상도 옵션 검색 및 드랍메뉴에 텍스트 할당
        resoultion_drop.onValueChanged.AddListener(Utils.Instance.GetComponent<ViewOption>().OptionChange); // ResolutionDrop 에 값 변경시 index번호를 할당할 함수 할당
        screen_mode.onValueChanged.AddListener(Utils.Instance.GetComponent<ViewOption>().FullScreenCheck);  // 전체 화면 체크 토글 버튼 이벤트 할당
        Apply_Button.onClick.AddListener(Utils.Instance.GetComponent<ViewOption>().Scene_Set_Resolution);   // Apply시 해상도 옵션 및 전체화면 옵션 적용 후 변경
    }
    /// <summary>
    /// InGame 씬으로 전환
    /// </summary>
    public void isInGame()
    {
        GameManager.instance.InGame = true;
    }
    /// <summary>
    /// 시작 버튼 이벤트 씬 로드 이벤트 추가
    /// </summary>
    private void StartButton_Init()
    {
        SoundManager.Instance.Sounds_BGM_Fade_Out();

        NewGame_Button.onClick.AddListener(Next_.Invoke);
        Continue_Button.onClick.AddListener(Next_.Invoke);
    }
    /// <summary>
    /// 실제 씬 동작 함수
    /// </summary>
    public void StartGame()
    {
        SoundManager.Instance.Save_prview_SliderVale();

        if(ContinueGame)
        {
            GameManager.instance.isLoad = true;
            Utils.Instance.StageNumber = Utils.Instance.cleardata.data.scenenumber;
        }else
            Utils.Instance.StageNumber++;

        Utils.Instance.CombineStageName();
    }
    public void ChoiceContinue()
    {
        ContinueGame = true;
    }
    public void anotherAction()
    {
        NewGame_Button.gameObject.SetActive(false);
        Continue_Button.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ClearCanvas = GameManager.instance.ClearC.gameObject;

        ClearCanvas.SetActive(false);
        GameManager.instance.isContinue = Utils.Instance.cleardata.LoadTransform();

        if (!GameManager.instance.isContinue)
            Continue_Button.interactable = false;
    }
}