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
    /// �ٸ� TextOn�� �ڷ�ƾ�� �浹�� ���ѷ��� �߻�, �׷��⿡ �ش��ϴ� TextOn ���� ������ ��� �ڷ�ƾ ����
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
