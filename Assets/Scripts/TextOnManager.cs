using UnityEngine;

public class TextOnManager : MonoBehaviour
{
    public static TextOnManager Instance;

    [SerializeField]
    private TextOn[] TextOns;

    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 다른 TextOn의 코루틴과 충돌시 무한루프 발생, 그렇기에 해당하는 TextOn 제외 나머지 모든 코루틴 정지
    /// </summary>
    /// <param name="now_TextOn"></param>
    public void managed(TextOn t_text)
    {
        foreach(var text in TextOns)
        {
            if(t_text!=text)
                text.AllStop();
        }
    }
}
