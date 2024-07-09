using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Tooltip("도착점")]
    public Transform arrivePoint;

    [SerializeField]
    private string portal;

    /// <summary>
    /// 지정된 위치로 순간이동
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //SoundManager.Instance.PlaySoundEffect(portal);
            GameManager.instance.DamageOn = false;
            Player.instance.Teleport(arrivePoint);

            Player.instance._hipsBone.position = arrivePoint.position;                                                        // Ragdoll의 중심점 또한 함께 이동
            Player.instance.transform.position=arrivePoint.position;                                                          // 지정된 위치로 순간이동
            GameManager.instance.DamageOn = true;
        }
    }
}
