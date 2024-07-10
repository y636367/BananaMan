using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartUIAnime : MonoBehaviour
{
    #region Variable
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // �̺�Ʈ ������ ���� �ν��Ͻ� Ŭ���� ����
    public After_ Next_;

    [SerializeField]
    private GameObject Title;
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private Animator UI_animator;

    [Space(10f)]
    [SerializeField]
    private Animator BananaMan_;                                                                // BananaMan ���� ����
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
    #endregion
    private void Awake()
    {
        UI_animator.gameObject.SetActive(false);
        BananaManActingButton.gameObject.SetActive(false);                                      // ��ư ��Ȱ��ȭ�� �ִϸ��̼� ���������� ���ʿ��� ���� �� ����
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
    /// �ʱ� �̺�Ʈ�߰��� �����ؾ��� �Լ�
    /// </summary>
    public void Init_Event()
    {
        UI_animator.gameObject.SetActive(true);
        Start_Button.onClick.AddListener(StartButton_Init);
        BindingPanel.instance.Init();
    }
    /// <summary>
    /// ��ư ��Ȱ��ȭ
    /// </summary>
    private void Buttons_Interactable_false()
    {
        Start_Button.interactable = false;
        Option_Button.interactable = false;
        Exit_Button.interactable = false;
    }
    /// <summary>
    /// ��ư Ȱ��ȭ �� �̺�Ʈ �߰�
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
    /// ������ ��ư�� �� ���۹� On
    /// </summary>
    public void On_etc()
    {
        UI_animator.SetTrigger("Etc");
    }
    /// <summary>
    /// ��ư�� Ȱ��ȭ �� ���� �ִϸ����� �� �̻� �ʿ� ���⿡ ��Ȱ��ȭ
    /// </summary>
    public void UI_Off()
    {
        Buttons_Interactable_true();

        UI_animator.enabled = false;
    }
    /// <summary>
    /// ù ���۽� ĳ���� �λ� ���
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
    /// ���� ����� ���� �Լ�
    /// </summary>
    public void BananaMan_Random_Acting()
    {
        BananaManActingButton.interactable = false;                                                         // ���ʿ��� �߰� ���� ���ֱ� ���� ��ư ��Ȱ��ȭ

        int random_value = UnityEngine.Random.Range(1, 10);

        switch (random_value)                                                           // ���� Ȱ��ȭ �ǰ� �ִ� ����� Idle���� �ƴ��� Ȯ���� ���� string ������ ��� �̸� ����
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
    /// �ִϸ��̼� ���� Ȯ��, ���� ��ٸ� Idle�� ��� Return
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
    /// �Ϲ� Idle ��� ���� ���ƿ��� �� ��ư Ȱ��ȭ �Լ�
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
    /// Exit �̺�Ʈ �߰�
    /// </summary>
    private void Get_Exit_Buttons_Event()
    {
        Exit_Button.onClick.AddListener(Utils.Instance.Exit_Program);
    }
    /// <summary>
    /// Option ���� �̺�Ʈ �߰�
    /// </summary>
    private void Option_AddListener()
    {
        SoundManager.Instance.GetSliders(BGM_S, SFX_S);                                                     // ���� �����̴� �Ҵ�

        Utils.Instance.GetComponent<ViewOption>().Set_Variable(resoultion_drop, Main);                      // �ػ� ��Ӹ޴�, ��üȭ�� üũ, ī�޶� �Ҵ�
        Option_Button.onClick.AddListener(Utils.Instance.GetComponent<ViewOption>().SetResolution);         // Option_Button Ŭ�� �� �� �������� ����� ���� �ػ� �ɼ� �˻� �� ����޴��� �ؽ�Ʈ �Ҵ�
        resoultion_drop.onValueChanged.AddListener(Utils.Instance.GetComponent<ViewOption>().OptionChange); // ResolutionDrop �� �� ����� index��ȣ�� �Ҵ��� �Լ� �Ҵ�
        screen_mode.onValueChanged.AddListener(Utils.Instance.GetComponent<ViewOption>().FullScreenCheck);  // ��ü ȭ�� üũ ��� ��ư �̺�Ʈ �Ҵ�
        Apply_Button.onClick.AddListener(Utils.Instance.GetComponent<ViewOption>().Scene_Set_Resolution);   // Apply�� �ػ� �ɼ� �� ��üȭ�� �ɼ� ���� �� ����
    }
    /// <summary>
    /// InGame ������ ��ȯ
    /// </summary>
    public void isInGame()
    {
        GameManager.instance.InGame = true;
    }
    /// <summary>
    /// ���� ��ư �̺�Ʈ �� �ε� �̺�Ʈ �߰�
    /// </summary>
    private void StartButton_Init()
    {
        SoundManager.Instance.Sounds_BGM_Fade_Out();

        Next_?.Invoke();
    }
    /// <summary>
    /// ���� �� ���� �Լ�
    /// </summary>
    public void StartGame()
    {
        SoundManager.Instance.Save_prview_SliderVale();

        Utils.Instance.StageNumber++;
        Utils.Instance.CombineStageName();
    }
    private void OnEnable()
    {
        ClearCanvas = GameManager.instance.ClearC.gameObject;

        ClearCanvas.SetActive(false);
    }
}
