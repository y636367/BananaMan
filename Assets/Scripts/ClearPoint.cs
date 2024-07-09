using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    [SerializeField]
    private ExitStage Manager;

    bool Clear;
    /// <summary>
    /// Clear 확인
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Player.instance.isRagdoll && !Clear)
            {
                Clear = true;
                StopCoroutine(GameManager.instance.GameTImer());                                                        // 타이머 일시 정지
                StartCoroutine(Manager.MovingCamera());
            }
        }
    }
}
