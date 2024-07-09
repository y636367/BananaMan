using System.Collections;
using UnityEngine;

public class HideBlock : MonoBehaviour
{
    [SerializeField]
    private bool Default_B;
    [SerializeField]
    private float Speed;
    [SerializeField]
    public int ColorNumber;

    [Space(10f)]
    [SerializeField]
    public HideBlockManager HM;

    [Space(10f)]
    [SerializeField]
    private Material material;

    AudioSource source;
    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();

        material = renderer.material;
    }
    private void Start()
    {
        StartHide();
    }
    /// <summary>
    /// ���� ���� �ƴѰ�� Hide ����
    /// </summary>
    public void StartHide()
    {
        if (!Default_B)
            StartCoroutine(nameof(Hide));
    }
    /// <summary>
    /// ��Ƽ���� ���� �� ������ ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Hide()
    {
        while (material.color.a > 0)
        {
            while (GameManager.instance.isPause)
                yield return null;

            material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - (Time.deltaTime / Speed));
            yield return null;
        }
    }
    public void StartStartAppear()
    {
        if (!Default_B)
            StartCoroutine(nameof(Appear));
    }
    /// <summary>
    /// ��Ƽ���� ���� �� ������ ��Ÿ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Appear()
    {
        if (source.mute)
            source.mute = false;
        source.Play();

        while (material.color.a < 1)
        {
            if (GameManager.instance.isPause)
                yield return null;

            material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a + (Time.deltaTime / Speed));
            yield return null;
        }
    }
    public void StopCoroutines()
    {
        StopAllCoroutines();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HM.CheckNumberAppear(ColorNumber);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            HM.CheckNumberHide(ColorNumber);
    }
}
