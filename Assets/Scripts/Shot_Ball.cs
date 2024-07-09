using System.Collections;
using UnityEngine;

public class Shot_Ball : MonoBehaviour
{
    Rigidbody rigid;

    [Range(10f,15f)]
    public float Damage;
    [Range(1,100)]
    public int LifeTime;
    [SerializeField]
    private bool DeadBall;

    private bool isPause;
    private Vector3 saveVelocity;
    private Vector3 saveAngularVelocity;

    private AudioSource Collision_S;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Collision_S = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(nameof(Pause));
    }
    /// <summary>
    /// �Ѿ��� �����ֱ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator LifeCycle()
    {
        int timer = 0;

        while (true)
        {
            timer++;

            yield return new WaitForSeconds(1f);

            if (timer >= LifeTime)
            {
                StopLifeCycle();
            }
        }
    }
    /// <summary>
    /// �Ͻ����� �� ����� �ӷ� ���� �� �� �ο�
    /// </summary>
    /// <returns></returns>
    private IEnumerator Pause()
    {
        while(true)
        {
            if (GameManager.instance.isPause)
            {
                if (!isPause)
                {
                    isPause = true;
                    saveVelocity = rigid.velocity;
                    saveAngularVelocity = rigid.angularVelocity;
                    rigid.isKinematic = true;
                }
            }
            else
            {
                if(this.isPause)
                {
                    isPause = false;
                    rigid.isKinematic = false;
                    rigid.velocity = saveVelocity;
                    rigid.angularVelocity = saveAngularVelocity;
                }
            }
            yield return null;
        }
    }
    /// <summary>
    /// �Ѿ��� �Ҹ� ���� ���� �� �÷��̾� ������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Collision_S.mute = false;
        Collision_S.Play();

        if (!DeadBall)
        {
            if (collision.gameObject.GetComponent<Shot_Ball>())
                return;
            else if (collision.gameObject.CompareTag("Player"))
            {
                if (!Player.instance.isRagdoll)
                {
                    Player.instance.isRagdoll = true;
                    Player.instance.RagdollOK = false;
                    Player.instance.RagdollOn();

                    Player.instance.GetDamage(Damage);
                }
            }
            else if (collision.gameObject.CompareTag("DeathZone"))
            {
                this.StopAllCoroutines();
                this.gameObject.SetActive(false);
            }

            GameObject Dust = GameManager.instance.effectpool.SpawnEffect(0, GameManager.instance.effectpool.Buliets_P.transform);                                // ���� ����Ʈ ����
            Dust.transform.position = this.transform.position;
        }

        rigid.useGravity = true;
        DeadBall = true;
    }
    /// <summary>
    /// �Ҹ�
    /// </summary>
    private void StopLifeCycle()
    {
        this.StopAllCoroutines();
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// �� ���°� �� �÷��� ���� �ʱ�ȭ
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="speed"></param>
    public void Init(Vector3 dir, float speed)
    {
        StartCoroutine(nameof(LifeCycle));

        rigid.velocity = dir * speed;
        DeadBall = false;
        rigid.useGravity = false;
    }
}
