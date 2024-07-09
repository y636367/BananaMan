using UnityEngine;

public class SoundPack : MonoBehaviour
{
    [Tooltip("���� ���Ǵ� �Ҹ�")]
    [SerializeField]
    private string Button;

    [SerializeField]
    private string Window;

    [SerializeField]
    private string Clear;

    public void Button_S()
    {
        SoundManager.Instance.PlaySoundEffect(Button);
    }
    public void Window_S()
    {
        SoundManager.Instance.PlaySoundEffect(Window);
    }
    public void Clear_S()
    {
        SoundManager.Instance.PlaySoundEffect(Clear);
    }
}
