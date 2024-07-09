using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // 이벤트 적용을 위한 인스턴스 클래스 생성
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
    [Tooltip("리스폰 이름")]
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
