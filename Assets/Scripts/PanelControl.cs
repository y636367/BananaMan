using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    #region Variable
    [Header("Buttons")]
    [SerializeField]
    private Button Option_B;
    [SerializeField]
    private Button KeyBinding_B;

    [Header("Panels")]
    [SerializeField]
    private GameObject Option_P;
    [SerializeField]
    private GameObject KeyBinding_P;

    [SerializeField]
    private GameObject Option;
    #endregion
    /// <summary>
    /// Option 패널 On
    /// </summary>
    public void Option_On()
    {
        Option_B.GetComponent<Image>().color = new Color(Option_B.GetComponent<Image>().color.r, Option_B.GetComponent<Image>().color.g, Option_B.GetComponent<Image>().color.b, 0f);
        Option_B.interactable = false;
        KeyBinding_B.GetComponent<Image>().color = new Color(KeyBinding_B.GetComponent<Image>().color.r, KeyBinding_B.GetComponent<Image>().color.g, KeyBinding_B.GetComponent<Image>().color.b, 0.5f);
        KeyBinding_B.interactable = true;

        Option_P.SetActive(true);
        KeyBinding_P.SetActive(false);

        GameManager.instance.isPause = true;
    }
    /// <summary>
    /// Key 바인딩 패널 On
    /// </summary>
    public void KeyBiding_On()
    {
        Option_B.GetComponent<Image>().color = new Color(Option_B.GetComponent<Image>().color.r, Option_B.GetComponent<Image>().color.g, Option_B.GetComponent<Image>().color.b, 0.5f);
        Option_B.interactable = true;
        KeyBinding_B.GetComponent<Image>().color = new Color(KeyBinding_B.GetComponent<Image>().color.r, KeyBinding_B.GetComponent<Image>().color.g, KeyBinding_B.GetComponent<Image>().color.b, 0);
        KeyBinding_B.interactable = false;

        Option_P.SetActive(false);
        KeyBinding_P.SetActive(true);
    }
    /// <summary>
    /// Option 관련 패널 전부 Off
    /// </summary>
    public void Off_Panels()
    {
        Option.SetActive(false);
        Option_P.SetActive(false);
        KeyBinding_P.SetActive(false);

        GameManager.instance.isPause = false;
    }
    /// <summary>
    /// Option 관련 패널 전부 On
    /// </summary>
    public void On_Panels()
    {
        Option.SetActive(true);
        Option_P.SetActive(true);
        KeyBinding_P.SetActive(true);
    }
    /// <summary>
    /// (Pause 상황일때만 사용) Pause함수 호출로 재게
    /// </summary>
    public void isResume()
    {
        GameManager.instance.Pause();
    }
}
