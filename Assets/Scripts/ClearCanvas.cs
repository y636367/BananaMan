using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClearCanvas : MonoBehaviour
{
    #region Variable
    [Tooltip("바탕 이미지")]
    [SerializeField]
    private Image BackGround;

    [Space(10f)]
    [Tooltip("축하 멘트")]
    [SerializeField]
    private TextGradation Cong;

    [Space(10f)]
    [Tooltip("서브 축하 멘트")]
    [SerializeField]
    private Text SubCong;

    [Space(10f)]
    [Header("스코어")]
    [SerializeField]
    private Text TotalTime;
    [SerializeField]
    private Text TotalDeath;

    [Space(10f)]
    [Tooltip("안내문")]
    [SerializeField]
    private Text NextGuideButton;

    [Space(10f)]
    [SerializeField]
    private float speed;

    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // 이벤트 적용을 위한 인스턴스 클래스 생성
    public After_ Option_On;
    #endregion
    private void Update()
    {
        if (GameManager.instance.isClear)
        {
            if (Input.anyKeyDown)
            {
                Init();
                Option_On?.Invoke();                                                           // GomMain 함수를 이벤트로 하여 메인씬으로 전환하도록 이벤트연결
                GameManager.instance.isClear = false;
            }
        }
    }
    private void OnEnable()
    {
        Init();

        StartCoroutine(nameof(BackGround_Fade));
    }
    /// <summary>
    /// 알파값 초기화
    /// </summary>
    private void Init()
    {
        StopAllCoroutines();

        Cong.StopAll_C();

        BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, 0f);
        Cong.GetComponent<Text>().color = new Color(Cong.GetComponent<Text>().color.r, Cong.GetComponent<Text>().color.g, Cong.GetComponent<Text>().color.b, 0f);
        SubCong.color = new Color(SubCong.color.r, SubCong.color.g, SubCong.color.b, 0f);
        TotalTime.color = new Color(TotalTime.color.r, TotalTime.color.g, TotalTime.color.b, 0f);
        TotalDeath.color = new Color(TotalDeath.color.r, TotalDeath.color.g, TotalDeath.color.b, 0f);
        NextGuideButton.color = new Color(NextGuideButton.color.r, NextGuideButton.color.g, NextGuideButton.color.b, 0f);
    }
    /// <summary>
    /// 배경이미지 Fadeout
    /// </summary>
    /// <returns></returns>
    IEnumerator BackGround_Fade()
    {
        while (BackGround.color.a < 1f)
        {
            BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, BackGround.color.a + (Time.deltaTime / speed));
            yield return null;
        }
        BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, 1f);

        StartCoroutine(nameof(Cong_Fade));
    }
    /// <summary>
    /// 축하 멘트 FadeIn 및 Gradient 효과 실행
    /// </summary>
    /// <returns></returns>
    IEnumerator Cong_Fade()
    {
        Cong.StartGradient();

        while (Cong.GetComponent<Text>().color.a < 1f)
        {
            Cong.GetComponent<Text>().color = new Color(Cong.GetComponent<Text>().color.r, Cong.GetComponent<Text>().color.g, Cong.GetComponent<Text>().color.b, Cong.GetComponent<Text>().color.a + (Time.deltaTime / speed));
            yield return null;
        }
        Cong.GetComponent<Text>().color = new Color(Cong.GetComponent<Text>().color.r, Cong.GetComponent<Text>().color.g, Cong.GetComponent<Text>().color.b, 1f);

        StartCoroutine(nameof(SubCong_Fade));
    }
    /// <summary>
    /// 서브 축하 멘트 Fadein
    /// </summary>
    /// <returns></returns>
    IEnumerator SubCong_Fade()
    {
        while (SubCong.color.a < 1f)
        {
            SubCong.color = new Color(SubCong.color.r, SubCong.color.g, SubCong.color.b, SubCong.color.a + (Time.deltaTime / speed));
            yield return null;
        }
        SubCong.color = new Color(SubCong.color.r, SubCong.color.g, SubCong.color.b, 1f);

        StartCoroutine(nameof(TotalTime_Fade));
    }
    /// <summary>
    /// 총 소요시간 FadeOut
    /// </summary>
    /// <returns></returns>
    IEnumerator TotalTime_Fade()
    {
        TotalTime.GetComponent<HUD>().Update_HUD();

        while (TotalTime.color.a < 1f)
        {
            TotalTime.color = new Color(TotalTime.color.r, TotalTime.color.g, TotalTime.color.b, TotalTime.color.a + (Time.deltaTime / speed));
            yield return null;
        }
        TotalTime.color = new Color(TotalTime.color.r, TotalTime.color.g, TotalTime.color.b, 1f);

        StartCoroutine(nameof(TotalDeath_Fade));
    }
    /// <summary>
    ///  총 사망 횟수 FadeOut
    /// </summary>
    /// <returns></returns>
    IEnumerator TotalDeath_Fade()
    {
        TotalDeath.GetComponent<HUD>().Update_HUD();

        while (TotalDeath.color.a < 1f)
        {
            TotalDeath.color = new Color(TotalDeath.color.r, TotalDeath.color.g, TotalDeath.color.b, TotalDeath.color.a + (Time.deltaTime / speed));
            yield return null;
        }
        TotalDeath.color = new Color(TotalDeath.color.r, TotalDeath.color.g, TotalDeath.color.b, 1f);

        StartCoroutine(nameof(NextGuideFadeIn));
    }
    /// <summary>
    /// 다음 행동 안내 문구 Fade
    /// </summary>
    /// <returns></returns>
    IEnumerator NextGuideFadeIn()
    {
        GameManager.instance.isClear = true;

        while (NextGuideButton.color.a < 1f)
        {
            NextGuideButton.color = new Color(NextGuideButton.color.r, NextGuideButton.color.g, NextGuideButton.color.b, NextGuideButton.color.a + (Time.deltaTime / speed));
            yield return null;
        }
        NextGuideButton.color = new Color(NextGuideButton.color.r, NextGuideButton.color.g, NextGuideButton.color.b, 1f);

        StartCoroutine(nameof(NextGuideFadeOut));
    }
    IEnumerator NextGuideFadeOut()
    {
        while (NextGuideButton.color.a > 0f)
        {
            NextGuideButton.color = new Color(NextGuideButton.color.r, NextGuideButton.color.g, NextGuideButton.color.b, NextGuideButton.color.a - (Time.deltaTime / speed));
            yield return null;
        }
        NextGuideButton.color = new Color(NextGuideButton.color.r, NextGuideButton.color.g, NextGuideButton.color.b, 0f);

        StartCoroutine(nameof(NextGuideFadeIn));
    }
}
