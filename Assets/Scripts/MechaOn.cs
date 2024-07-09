using UnityEngine;

public class MechaOn : MonoBehaviour
{
    [SerializeField]
    private Mech_Head Mecha;

    /// <summary>
    /// 영역 내 들어올 시 로봇 기동
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Mecha.MechaOn_)
        {
            Mecha.MechaOn_ = true;
            Mecha.SettingPlayer(Player.instance.Spine3);
            Mecha.MechaOn();
        }
    }
    /// <summary>
    /// 영역 밖으로 나갈 시 로봇 오프
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Mecha.MechaOn_ = false;
            Mecha.MeChaOff();
        }
    }
}
