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
    public bool Flag;

    [Space(10f)]
    [Tooltip("Respawn Name")]
    public string PointName;

    [Space(10f)]
    [Tooltip("Respawn Numbering")]
    public int PointNumber;

    [Space(10f)]
    [SerializeField]
    public GameObject Lamp;

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
            GameManager.instance.rm.nowPointNumber = PointNumber;

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
                Utils.Instance.cleardata.SaveTransform(this.transform);
                GameManager.instance.rm.nowPointName = PointName;
                GameManager.instance.rm.nowPointNumber = PointNumber;
                GameManager.instance.rm.NewRespawnPoint(RespawnPosition);

                if(Lamp != null)
                    Lamp.SetActive(true);

                Next_?.Invoke();
            }
        }
    }
}
