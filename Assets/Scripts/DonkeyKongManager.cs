using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongManager : MonoBehaviour
{
    [Tooltip("오브젝트 풀링")]
    [SerializeField]
    private GameObject Barrel;
    private List<GameObject> barrel = new List<GameObject>();

    [SerializeField]
    private int PoolSize;
    [SerializeField]
    private float Time;

    [Space(10f)]
    [Tooltip("오브젝트 회전값")]
    public Vector3 RotationValue;
    [Tooltip("오브젝트 속력값")]
    public float Velocity;
    [Tooltip("오브젝트 방향")]
    public Vector3 Direction;
    [Tooltip("0-x 1-y 2-z")]
    public int dirnumber;
    
    private Transform PostionValue;
    private void Start()
    {
        for (int index = 0; index < PoolSize; index++)                                                              // 오브젝트 풀링을 위한 선작업
        {
            GameObject Cylinder = Instantiate(Barrel, this.transform);                                              // 크기만큼 미리 생성
            Cylinder.gameObject.SetActive(false);                                                                   // 비활성화
            barrel.Add(Cylinder);                                                                                   // 리스트에 추가
        }

        StartCoroutine(nameof(GameStart));                                                                         
    }
    /// <summary>
    /// 코루틴으로 계속해서 동작
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        int number = 0;

        while(true)
        {
            yield return StartCoroutine(Timer(Time));                                                               // 일정 시간마다 반복

            if (number >= PoolSize)                                                                                 // 풀 사이즈만큼 돌고난 후 초기화
                number = 0;

            if (!barrel[number].activeSelf)                                                                         // 비활성화 되어있다면
            {
                barrel[number].transform.position = this.transform.position;                                        // 포지션 재설정(회전, 각속력, 속력 은 자체 활성화시 초기화되게 스크립트 부착)
                barrel[number].SetActive(true);
            }
            number++;
        }
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
            if (GameManager.instance.isPause)                                                                   // 타이머 또한 일시정지 구현하여 일시정지시 타이머가 다 돌아간 후 다시 재생 시 타이머 또한 정지되지 않은 시스템 오류 수정
                yield return null;

            count += t_timer;

            yield return new WaitForSeconds(t_timer);
        }
    }
}
