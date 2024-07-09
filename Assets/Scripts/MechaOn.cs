using UnityEngine;

public class MechaOn : MonoBehaviour
{
    [SerializeField]
    private Mech_Head Mecha;

    /// <summary>
    /// ���� �� ���� �� �κ� �⵿
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
    /// ���� ������ ���� �� �κ� ����
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
