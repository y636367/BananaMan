using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // �̺�Ʈ ������ ���� �ν��Ͻ� Ŭ���� ����
    public After_ Next_;

    [SerializeField]
    public Vector3 RespawnPosition;

    [SerializeField]
    private bool isTriggerPoint;
    [SerializeField]
    private bool isDefault;
    [SerializeField]
    private bool Flag;

    [Space(10f)]
    [Tooltip("������ �̸�")]
    public string PointName;

    [Space(10f)]
    [SerializeField]
    private GameObject Lamp;

    private void Awake()
    {
        RespawnPosition = this.transform.position;

        if (Lamp != null)
            Lamp.SetActive(false);
    }
    private void Start()
    {
        if (isDefault)
        {
            GameManager.instance.rm.Default_RespawnPoint = RespawnPosition;
            GameManager.instance.rm.nowPointName = PointName;

            Next_?.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggerPoint)
            return;
        else
        {
            if (other.CompareTag("Player") && !Flag)
            {
                Flag = true;
                GameManager.instance.rm.nowPointName = PointName;
                GameManager.instance.rm.NewRespawnPoint(RespawnPosition);
                Lamp.SetActive(true);

                Next_?.Invoke();
            }
        }
    }
}
