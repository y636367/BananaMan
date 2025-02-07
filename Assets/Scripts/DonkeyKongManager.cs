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
    private float elapsedTime;

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
        float count = 0.0f;

        while (true)
        {
            while (GameManager.instance.isPause)
                yield return null;

            count += Time.deltaTime;

            if (count >= elapsedTime)
            {
                count = 0;

                int attempts = 0;
                while (barrel[number].activeSelf&&attempts< PoolSize)
                {
                    number = (number + 1) % PoolSize;
                    attempts++;
                }

                if (!barrel[number].activeSelf)                                                                         // 비활성화 되어있다면
                {
                    barrel[number].transform.position = this.transform.position;                                        // 포지션 재설정(회전, 각속력, 속력 은 자체 활성화시 초기화되게 스크립트 부착)
                    barrel[number].SetActive(true);
                    number = (number + 1) % PoolSize;
                }
            }
            yield return null;
        }
    }
}
