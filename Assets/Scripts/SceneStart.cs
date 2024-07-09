using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class SceneStart : MonoBehaviour
{
    #region Variable
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // �̺�Ʈ ������ ���� �ν��Ͻ� Ŭ���� ����
    public After_ Next_1;
    public After_ Next_2;
    public After_ Next_3;
    public After_ Next_4;

    [Space(10f)]
    [SerializeField]
    private Image FadeImage;
    [SerializeField]
    private float Speed;

    [Space(10f)]
    [Header("Setting_Variable")]
    [SerializeField]
    private Toggle fullscreen;
    [SerializeField]
    private Camera Main;
    [SerializeField]
    private Dropdown Resolution;
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider SFX;

    [Space(10f)]
    [Header("Option_Variable")]
    [SerializeField]
    private GameObject Option_Panel;
    [SerializeField]
    private GameObject Panel_Close;
    [SerializeField]
    private PanelControl PanelControl;

    [Space(10f)]
    [Header("Addtional_Option_Variable")]
    [SerializeField]
    private GameObject Middle_Panel;
    [SerializeField]
    private Button goMain;
    [SerializeField]
    private string SceneName;
    [SerializeField]
    private Button Option_Button;
    [SerializeField]
    private Button Apply;

    [Space(10f)]
    [Header("EtcManager")]
    [SerializeField]
    private Respawn_Manager rm;

    [Space(10f)]
    public bool NeedSetting;

    [Space(10f)]
    public bool TimerStart;

    [Space(10f)]
    [SerializeField]
    private string BGM_Name;
    #endregion
    private void Awake()
    {
        StartCoroutine(nameof(FindGameManager));

        try
        {
            SoundManager.Instance.StopAllSoundBGM();
            SoundManager.Instance.StopAllSoundEffect();
        }
        catch(NullReferenceException) { }
    }
    private IEnumerator FindGameManager()
    {
        while(true)
        {
            if (GameManager.instance != null)
            {
                Init();
                yield break;
            }
            else
                yield return null;
        }
    }
    /// <summary>
    /// �� ���۽� �ʱ�ȭ
    /// </summary>
    private void Init()
    {
        Next_1?.Invoke();
        FadeImage.gameObject.SetActive(true);
        Next_2?.Invoke();

        ManagerAllocation();

        StartCoroutine(nameof(FadeIn));
    }
    private void ManagerAllocation()
    {
        if (rm != null)
        {
            GameManager.instance.rm = this.rm;
        }
    }
    /// <summary>
    /// ���� �����
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        while (FadeImage.color.a > 0)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeImage.color.a - (Time.deltaTime / Speed));
            yield return null;
        }
        Next_3?.Invoke();

        BGM_Play();
        GameManager.instance.isStart = true;
    }
    /// <summary>
    /// �ܺ� ȣ���� ���� �Լ� ����
    /// </summary>
    public void FadeOut_()
    {
        FadeImage.gameObject.SetActive(true);

        StartCoroutine(nameof(FadeOut));
    }
    /// <summary>
    /// ���� ��ο���
    /// </summary>
    /// <returns></returns>
    public IEnumerator FadeOut()
    {
        while (FadeImage.color.a < 1f)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeImage.color.a + (Time.deltaTime / Speed));
            yield return null;
        }

        Next_4?.Invoke();
    }
    /// <summary>
    /// ViewOption, SoundManager�� �Ҵ�Ǿ�� �� ���� �Ҵ�
    /// </summary>
    public void Setting_Variation()
    {
        SoundManager.Instance.GetSliders(BGM, SFX);

        Utils.Instance.GetComponent<ViewOption>().Set_Variable(Resolution, Main);
        Utils.Instance.GetComponent<ViewOption>().screenbutton = fullscreen;
        Option_Button.onClick.AddListener(Utils.Instance.GetComponent<ViewOption>().SetResolution);
        Resolution.onValueChanged.AddListener(Utils.Instance.GetComponent<ViewOption>().OptionChange);
        fullscreen.onValueChanged.AddListener(Utils.Instance.GetComponent<ViewOption>().FullScreenCheck);
        Apply.onClick.AddListener(Utils.Instance.GetComponent<ViewOption>().Scene_Set_Resolution);
    }
    /// <summary>
    /// Option �г� On
    /// </summary>
    public void Option_On()
    {
        Panel_Close.SetActive(true);

        if (!GameManager.instance.InGame)
        {
            Option_Panel.SetActive(true);
            PanelControl.Option_On();
        }
        else
        {
            MiddleOption_On();
        }
    }
    /// <summary>
    /// Option �г� Off
    /// </summary>
    public void Option_Off()
    {
        Option_Panel.SetActive(false);
        Panel_Close.SetActive(false);

        try
        {
            MiddleOption_Off();
        }
        catch (UnassignedReferenceException) { }
    }
    /// <summary>
    /// �߰� �ɼ� â Ȱ��ȭ
    /// </summary>
    public void MiddleOption_On()
    {
        Middle_Panel.SetActive(true);
    }
    /// <summary>
    /// �߰� �ɼ� â ��Ȱ��ȭ
    /// </summary>
    public void MiddleOption_Off()
    {
        Middle_Panel.SetActive(false);
    }
    /// <summary>
    /// GameManager�� Optionâ Ȱ��ȭ, ��Ȱ��ȭ �̺�Ʈ �߰� �� Mainȭ�� �̺�Ʈ �߰�
    /// </summary>
    private void OnEnable()
    {
        try
        {
            GameManager.instance.Option_On.AddListener(Option_On);
            GameManager.instance.Option_Close.AddListener(Option_Off);
            GameManager.instance.SP = this.GetComponent<SoundPack>();

            goMain.onClick.AddListener(GoMain_Init);

            if (Utils.Instance.StageNumber + 1 == Utils.Instance.sceneCount)                                                                // ���� ���� ������ ���̶�� Ŭ����� �������� �Ѿ �� �ְ� ����
                GameManager.instance.ClearC.Option_On.AddListener(GoMain_Init);
        }
        catch (NullReferenceException) { }

        if (TimerStart)
        {
            StartCoroutine(GameManager.instance.GameTImer());                                                                              // �÷��� Ÿ�� ����� ���� �ڷ�ƾ ����
        }
    }
    /// <summary>
    /// GameManager�� ���� ���� �� �ʱ�ȭ
    /// </summary>
    private void GoMain_Init()
    {
        GameManager.instance.totalPlayTime = 0.0f;                                                                                         // ��� �ʱ�ȭ
        GameManager.instance.totalDeath = 0;

        GameManager.instance.InGame = false;
        GameManager.instance.DamageOn = false;
        GameManager.instance.rm = null;

        if(!GameManager.instance.isClear)
            GameManager.instance.Pause();

        SoundManager.Instance.Save_prview_SliderVale();
        SoundManager.Instance.Sounds_BGM_Fade_Out();
        FadeOut_();
    }
    /// <summary>
    /// �� �ε� �� Ŀ�� ���� �ʱ�ȭ
    /// </summary>
    public void GoMain()
    {
        CameraController.instance.Cursor_UnLocking_Show();
        Utils.Instance.StageNumber = 0;
        Utils.Instance.CombineStageName();
    }
    #region Sound
    private void BGM_Play()
    {
        SoundManager.Instance.Reset_BGM_Fade();
        SoundManager.Instance.StopAllSoundBGM();

        if (!string.IsNullOrEmpty(BGM_Name))
            SoundManager.Instance.PlaySoundBGM(BGM_Name);
    }
    #endregion
}
