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
    /// 로봇이 플레이어를 바라보도록
    /// </summary>
    private void CalcDir()
    {
        if (GameManager.instance.isPause)
            return;

        transform.LookAt(target);
        transform.rotation = transform.rotation * Quaternion.Euler(offset);                                             // 회전 값 오차 수정
    }
    public void SettingPlayer(Transform t_player) => target = t_player;
    public void MechaOn() => StartCoroutine(nameof(CoolTime));
    public void MeChaOff() => StopCoroutine(nameof(CoolTime));
    /// <summary>
    /// 공 발사
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoolTime()
    {
        while (true)
        {
            yield return StartCoroutine(Timer(time));                                                                       // 일정 시간마다 반복

            shot_Number=Random.Range(0,Shot_sounds.Length);

            animator.SetTrigger("Shot");
            LeftShot();
            RightShot();
        }
    }
    /// <summary>
    /// 이펙트 생성
    /// </summary>
    /// <param name="t_position"></param>
    private void EffectSpawn(Transform t_position)
    {
        GameObject Dust = GameManager.instance.effectpool.SpawnEffect(4, GameManager.instance.effectpool.Shots_P.transform);
        Dust.transform.position = t_position.position;
        Dust.transform.rotation = t_position.rotation * Quaternion.Euler(-offset);
    }
    /// <summary>
    /// 왼쪽 총 발사
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
    /// 오른쪽 총 발사
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
    /// 타이머 구현
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Timer(float time)
    {
        float t_time = time;
        float count = 0.0f;
        float t_timer;

        string numberSTR = t_time.ToString("G");                                                                   // 가장 간단한 형태의 문자열로 변환(.000 의 경우 .000 덜어냄)
        int decimalPoint = numberSTR.IndexOf('.');                                                                 // 소수점 포함 여부 확인(포함 시 포함된 인덱스 위치를 정수로 반한)

        if (decimalPoint == -1)                                                                                    // 미포함이므로 입력된 값은 정수로 판단
        {
            if (t_time < 10)                                                                                       // 10초를 넘어가지 않는 초의 경우 일시정지 시 원하는대로 정지가 되지 않기에 한단계 낮은 소수점단위로 설정
                t_timer = 0.1f;
            else
                t_timer = 1.0f;
        }
        else                                                                                                       // 포함이므로 입력된 값은 실수로 판단
        {
            int decimalPlaces = numberSTR.Length - decimalPoint - 1;                                               // 입력된 값에 소수점이 포함된 인덱스 값과 1을 합해 빼주어 소수점 길이 확인
            t_timer = Mathf.Pow(10, -decimalPlaces);                                                               // 10의 거듭제곱을 계산하여 소수점 이하의 자릿수 파악
        }

        while (count < t_time)
        {
           
            if (GameManager.instance.isPause)                                                                  // 타이머 또한 일시정지 구현하여 일시정지시 타이머가 다 돌아간 후 다시 재생 시 타이머 또한 정지되지 않은 시스템 오류 수정
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
