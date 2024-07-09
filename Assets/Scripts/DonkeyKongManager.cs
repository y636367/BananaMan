using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongManager : MonoBehaviour
{
    [Tooltip("������Ʈ Ǯ��")]
    [SerializeField]
    private GameObject Barrel;
    private List<GameObject> barrel = new List<GameObject>();

    [SerializeField]
    private int PoolSize;
    [SerializeField]
    private float Time;

    [Space(10f)]
    [Tooltip("������Ʈ ȸ����")]
    public Vector3 RotationValue;
    [Tooltip("������Ʈ �ӷ°�")]
    public float Velocity;
    [Tooltip("������Ʈ ����")]
    public Vector3 Direction;
    [Tooltip("0-x 1-y 2-z")]
    public int dirnumber;
    
    private Transform PostionValue;
    private void Start()
    {
        for (int index = 0; index < PoolSize; index++)                                                              // ������Ʈ Ǯ���� ���� ���۾�
        {
            GameObject Cylinder = Instantiate(Barrel, this.transform);                                              // ũ�⸸ŭ �̸� ����
            Cylinder.gameObject.SetActive(false);                                                                   // ��Ȱ��ȭ
            barrel.Add(Cylinder);                                                                                   // ����Ʈ�� �߰�
        }

        StartCoroutine(nameof(GameStart));                                                                         
    }
    /// <summary>
    /// �ڷ�ƾ���� ����ؼ� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        int number = 0;

        while(true)
        {
            yield return StartCoroutine(Timer(Time));                                                               // ���� �ð����� �ݺ�

            if (number >= PoolSize)                                                                                 // Ǯ �����ŭ ���� �� �ʱ�ȭ
                number = 0;

            if (!barrel[number].activeSelf)                                                                         // ��Ȱ��ȭ �Ǿ��ִٸ�
            {
                barrel[number].transform.position = this.transform.position;                                        // ������ �缳��(ȸ��, ���ӷ�, �ӷ� �� ��ü Ȱ��ȭ�� �ʱ�ȭ�ǰ� ��ũ��Ʈ ����)
                barrel[number].SetActive(true);
            }
            number++;
        }
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
            if (GameManager.instance.isPause)                                                                   // Ÿ�̸� ���� �Ͻ����� �����Ͽ� �Ͻ������� Ÿ�̸Ӱ� �� ���ư� �� �ٽ� ��� �� Ÿ�̸� ���� �������� ���� �ý��� ���� ����
                yield return null;

            count += t_timer;

            yield return new WaitForSeconds(t_timer);
        }
    }
}
