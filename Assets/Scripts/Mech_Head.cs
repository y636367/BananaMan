using System.Collections;
using UnityEngine;

public class Mech_Head : MonoBehaviour
{
    #region variable
    [Space(10f)]
    [Tooltip("Offset")]
    [SerializeField]
    private Vector3 offset;

    [Space(10f)]
    [SerializeField]
    private Transform Gun_Left;
    [SerializeField]
    private Transform Gun_Right;

    Animator animator;

    [Space(10f)]
    Transform target;
    public bool MechaOn_;

    [Space(10f)]
    public float time;
    public float ballSpeed;

    [Space(10f)]
    [SerializeField]
    private AudioClip[] Shot_sounds;
    [SerializeField]
    private AudioSource Left_S;
    [SerializeField]
    private AudioSource Right_S;
    private int shot_Number;
    #endregion
    private void Awake()
    {
        animator = GetComponent<Animator>();

        Left_S.mute = true;
        Right_S.mute = true;
    }
    private void Update()
    {
        if (MechaOn_)
            CalcDir();
    }
    /// <summary>
    /// �κ��� �÷��̾ �ٶ󺸵���
    /// </summary>
    private void CalcDir()
    {
        if (GameManager.instance.isPause)
            return;

        transform.LookAt(target);
        transform.rotation = transform.rotation * Quaternion.Euler(offset);                                             // ȸ�� �� ���� ����
    }
    public void SettingPlayer(Transform t_player) => target = t_player;
    public void MechaOn() => StartCoroutine(nameof(CoolTime));
    public void MeChaOff() => StopCoroutine(nameof(CoolTime));
    /// <summary>
    /// �� �߻�
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoolTime()
    {
        while (true)
        {
            yield return StartCoroutine(Timer(time));                                                                       // ���� �ð����� �ݺ�

            shot_Number=Random.Range(0,Shot_sounds.Length);

            animator.SetTrigger("Shot");
            LeftShot();
            RightShot();
        }
    }
    /// <summary>
    /// ����Ʈ ����
    /// </summary>
    /// <param name="t_position"></param>
    private void EffectSpawn(Transform t_position)
    {
        GameObject Dust = GameManager.instance.effectpool.SpawnEffect(4, GameManager.instance.effectpool.Shots_P.transform);
        Dust.transform.position = t_position.position;
        Dust.transform.rotation = t_position.rotation * Quaternion.Euler(-offset);
    }
    /// <summary>
    /// ���� �� �߻�
    /// </summary>
    private void LeftShot()
    {
        Left_S.clip = Shot_sounds[shot_Number];
        Left_S.mute = false;
        Left_S.Play();

        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - Gun_Left.transform.position;

        Transform buliet = RobotPoolManager.Instance.Get(0).transform;


        buliet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        buliet.position = Gun_Left.position;
        buliet.GetComponent<Shot_Ball>().Init(dir.normalized, ballSpeed);

        EffectSpawn(Gun_Left.transform);
    }
    /// <summary>
    /// ������ �� �߻�
    /// </summary>
    private void RightShot()
    {
        Right_S.clip = Shot_sounds[shot_Number];
        Right_S.mute = false;
        Right_S.Play();

        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - Gun_Right.transform.position;

        Transform buliet = RobotPoolManager.Instance.Get(1).transform;

        buliet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        buliet.position = Gun_Right.position;
        buliet.GetComponent<Shot_Ball>().Init(dir.normalized, ballSpeed);

        EffectSpawn(Gun_Right.transform);
    }
    /// <summary>
    /// Ÿ�̸� ����
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Timer(float time)
    {
        float t_time = time;
        float count = 0.0f;
        float t_timer;

        string numberSTR = t_time.ToString("G");                                                                   // ���� ������ ������ ���ڿ��� ��ȯ(.000 �� ��� .000 ���)
        int decimalPoint = numberSTR.IndexOf('.');                                                                 // �Ҽ��� ���� ���� Ȯ��(���� �� ���Ե� �ε��� ��ġ�� ������ ����)

        if (decimalPoint == -1)                                                                                    // �������̹Ƿ� �Էµ� ���� ������ �Ǵ�
        {
            if (t_time < 10)                                                                                       // 10�ʸ� �Ѿ�� �ʴ� ���� ��� �Ͻ����� �� ���ϴ´�� ������ ���� �ʱ⿡ �Ѵܰ� ���� �Ҽ��������� ����
                t_timer = 0.1f;
            else
                t_timer = 1.0f;
        }
        else                                                                                                       // �����̹Ƿ� �Էµ� ���� �Ǽ��� �Ǵ�
        {
            int decimalPlaces = numberSTR.Length - decimalPoint - 1;                                               // �Էµ� ���� �Ҽ����� ���Ե� �ε��� ���� 1�� ���� ���־� �Ҽ��� ���� Ȯ��
            t_timer = Mathf.Pow(10, -decimalPlaces);                                                               // 10�� �ŵ������� ����Ͽ� �Ҽ��� ������ �ڸ��� �ľ�
        }

        while (count < t_time)
        {
           
            if (GameManager.instance.isPause)                                                                  // Ÿ�̸� ���� �Ͻ����� �����Ͽ� �Ͻ������� Ÿ�̸Ӱ� �� ���ư� �� �ٽ� ��� �� Ÿ�̸� ���� �������� ���� �ý��� ���� ����
            {                        
                if(animator.speed>=1f)
                    animator.speed = 0f;

                yield return null;
            }

            if (animator.speed == 0f)
                animator.speed = 1f;

            count += t_timer;

            yield return new WaitForSeconds(t_timer);
        }
    }
}
