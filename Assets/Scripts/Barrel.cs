using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private DonkeyKongManager kongManager;
    [SerializeField]
    private AudioSource source_1;
    [SerializeField]
    private AudioSource source_2;

    private Vector3 RotationValue;                                                                           // 회전값 저장을 위한 변수
    private Vector3 direction;
    private Quaternion rotation_;
    private bool maintain_v;
    private bool isPause;

    private Vector3 savedVelocity;                                                                           // 일시정지 시 저장할 변수(속력)
    private Vector3 savedAngularVelocity;                                                                    // 각속력
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        kongManager=GetComponentInParent<DonkeyKongManager>();

        Setting();
    }
    /// <summary>
    /// Manager에 등록된 회전값 및 속력 받아오기
    /// </summary>
    private void Setting()
    {
        RotationValue = kongManager.RotationValue;
        rotation_ = Quaternion.Euler(RotationValue);
        direction = kongManager.Direction;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collision_D"))
        {
            source_2.mute = false;
            source_2.Play();
        }
    }
    /// <summary>
    /// 오직 땅에만 접촉 시 비활성화
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (source_1.mute)
            source_1.mute = false;

        source_1.Play();

        if (collision.gameObject.CompareTag("DeathZone") && !collision.gameObject.GetComponent<Barrel>())
        {
            source_1.mute = true;
            source_1.Stop();
            source_2.mute = true;
            source_2.Stop();

            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("VelocityChanger"))
            maintain_v = true;
            
        if(collision.gameObject.CompareTag("Player"))
            GameManager.instance.ForcedDeath();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("VelocityChanger"))
            maintain_v = false;
    }
    IEnumerator Maintain_V()
    {
        while (true)
        {
            if (GameManager.instance.isPause)
            {
                if (!isPause)
                {
                    isPause = true;
                    savedVelocity = rb.velocity;
                    savedAngularVelocity = rb.angularVelocity;
                    rb.isKinematic = true;
                }
                else
                    yield return null;
            }
            else
            {
                if (isPause)
                {
                    isPause = false;
                    rb.isKinematic = false;
                    rb.velocity = savedVelocity;
                    rb.angularVelocity = savedAngularVelocity;
                }
            }

            if (maintain_v)
                switch (kongManager.dirnumber)
                {
                    case 0:
                        rb.velocity = new Vector3(kongManager.Velocity, 0, 0);
                        break;
                    case 1:
                        rb.velocity = new Vector3(0, kongManager.Velocity, 0);
                        break;
                    case 2:
                        rb.velocity = new Vector3(0, 0, kongManager.Velocity);
                        break;
                }

            yield return null;
        }
    }
    /// <summary>
    /// 회전값, 속력, 각속력 초기화
    /// </summary>
    private void OnEnable()
    {
        maintain_v = false;

        transform.localRotation = rotation_;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;

        rb.velocity = new Vector3(0, -kongManager.Velocity, 0);

        StopAllCoroutines();
        StartCoroutine(nameof(Maintain_V));
    }
}
