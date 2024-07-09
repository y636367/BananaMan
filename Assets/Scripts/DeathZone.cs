using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public bool Nontouch;

    /// <summary>
    /// Player의 컨트롤러가 꺼졌을 때(래그돌 상태) 일시 Player의 Collider와 충돌 시 수행
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
