using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TextOn : MonoBehaviour
{
    #region Variable
    [SerializeField]
    private Text Tutorial_Text;
    [SerializeField]
    private Image BackGround;

    [SerializeField]
    private float Speed;

    [TextArea]
    [SerializeField]
    private string Guide;
    [SerializeField]
    private int GuideNumber;

    [SerializeField]
    private bool Guide_On;
    [SerializeField]
    private bool Guiding_On;
    [SerializeField]
    private bool Exit;
    #endregion
    private void Awake()
    {
        Init();
    }
    /// <summary>
    /// 튜토리얼 조작키 텍스트 초기화
    /// </summary>
    private void TextInit()
    {
        switch (GuideNumber)
        {
            case 0:
                Tutorial_Text.text = string.Format(Guide, MouseText_Change(Utils.Instance.binding.Bindings[Action.MoveForward]).ToString(),
                    MouseText_Change(Utils.Instance.binding.Bindings[Action.MoveLeft]).ToString(),
                    MouseText_Change(Utils.Instance.binding.Bindings[Action.MoveBackward]).ToString(),
                    MouseText_Change(Utils.Instance.binding.Bindings[Action.MoveRight]).ToString());
                break;
            case 1:
                Tutorial_Text.text = string.Format(Guide, MouseText_Change(Utils.Instance.binding.Bindings[Action.Dash]).ToString());
                break;
            case 2:
                Tutorial_Text.text = string.Format(Guide, MouseText_Change(Utils.Instance.binding.Bindings[Action.RotateCamera]).ToString());
                break;
            case 3:
                Tutorial_Text.text = string.Format(Guide, MouseText_Change(Utils.Instance.binding.Bindings[Action.Jump]).ToString());
                break;
            case 4:
                Tutorial_Text.text = string.Format(Guide, MouseText_Change(Utils.Instance.binding.Bindings[Action.Ragdoll]).ToString());
                break;
            case 5:
                Tutorial_Text.text = string.Format(Guide, MouseText_Change(Utils.Instance.binding.Bindings[Action.ForcedDeath]).ToString());
                break;
            case 6:
                Tutorial_Text.text = string.Format(Guide);
                break;
        }
    }
    /// <summary>
    /// 마우스 텍스트 변경
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private string MouseText_Change(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.Mouse0:
                return $"LeftButton";
            case KeyCode.Mouse1:
                return $"RightButton";
            case KeyCode.Mouse2:
                return $"MiddleButton";
            default:
                return $"{code}";
        }
    }
    /// <summary>
    /// 텍스트를 위한 패널 및 텍스트 초기화
    /// </summary>
    private void Init()
    {
        BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, 0.0f);
        Tutorial_Text.color = new Color(Tutorial_Text.color.r, Tutorial_Text.color.g, Tutorial_Text.color.b, 0.0f);

        BackGround.gameObject.SetActive(false);
        Tutorial_Text.gameObject.SetActive(false);
    }
    /// <summary>
    /// 가이드 부분 접근 시 점차 나타남
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnGuide()
    {
        BackGround.gameObject.SetActive(true);
        Tutorial_Text.gameObject.SetActive(true);

        while (Tutorial_Text.color.a < 1.0f)
        {
            BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, BackGround.color.a + (Time.deltaTime / Speed));
            Tutorial_Text.color = new Color(Tutorial_Text.color.r, Tutorial_Text.color.g, Tutorial_Text.color.b, Tutorial_Text.color.a + (Time.deltaTime / Speed));

            yield return null;
        }

        BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, 1.0f);
        Tutorial_Text.color = new Color(Tutorial_Text.color.r, Tutorial_Text.color.g, Tutorial_Text.color.b, 1.0f);

        Guide_On = true;
        Guiding_On = false;
    }
    /// <summary>
    /// 가이드 Off
    /// </summary>
    /// <returns></returns>
    private IEnumerator OffGuide()
    {
        while (Tutorial_Text.color.a > 0.0f)
        {
            BackGround.color = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, BackGround.color.a - (Time.deltaTime / Speed));
            Tutorial_Text.color = new Color(Tutorial_Text.color.r, Tutorial_Text.color.g, Tutorial_Text.color.b, Tutorial_Text.color.a - (Time.deltaTime / Speed));

            yield return null;
        }

        Init();

        Guide_On = false;
    }
    /// <summary>
    /// 가이드 구역 벗어날 시 이후 생존시간
    /// </summary>
    /// <returns></returns>
    private IEnumerator GuideLifeTIme()
    {
        int Count = 0;

        while (Count < 3)
        {
            if (!Exit || !Guide_On)
                break;

            if (Guide_On)
            {
                Count++;
                yield return new WaitForSeconds(1f);
            }
        }

        if (Exit && !Guiding_On && Guide_On)
        {
            StartCoroutine(nameof(OffGuide));
        }
    }
    /// <summary>
    /// 가이드 구역 접촉 시
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TextOnManager.Instance.managed(this);
            Exit = false;

            TextInit();
            if (!Guiding_On && !Guide_On)
            {
                Guiding_On = true;
                StartCoroutine(nameof(OnGuide));
            }
        }
    }
    /// <summary>
    /// 가이드 구역에서 벗어날 시
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Exit = true;

            StartCoroutine(nameof(GuideLifeTIme));
        }
    }
    /// <summary>
    /// 모든 코루틴 정지 및 관련 변수 초기화
    /// </summary>
    public void AllStop()
    {
        StopAllCoroutines();
        Guide_On = false;
        Guiding_On = false;
        Exit = true;
    }
}
