using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    [SerializeField]
    private ExitStage Manager;

    bool Clear;
    /// <summary>
    /// Clear Ȯ��
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Player.instance.isRagdoll && !Clear)
            {
                Clear = true;
                StopCoroutine(GameManager.instance.GameTImer());                                                        // Ÿ�̸� �Ͻ� ����
                StartCoroutine(Manager.MovingCamera());
            }
        }
    }
}
