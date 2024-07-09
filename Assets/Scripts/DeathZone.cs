using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public bool Nontouch;

    /// <summary>
    /// Player�� ��Ʈ�ѷ��� ������ ��(���׵� ����) �Ͻ� Player�� Collider�� �浹 �� ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(!Nontouch)
            if (collision.gameObject.CompareTag("Player") && !GameManager.instance.isForcedDeath)
            {
                GameManager.instance.ForcedDeath();
            }
    }
}
