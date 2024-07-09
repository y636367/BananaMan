using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class ExitStage : MonoBehaviour
{
    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // �̺�Ʈ ������ ���� �ν��Ͻ� Ŭ���� ����
    public After_ Next_1;

    [SerializeField]
    private Camera Main;

    [SerializeField]
    private GameObject CameraMovingPoint;
    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private Image FadeImage;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float moveSpeed;
    private void Awake()
    {
        Main = Camera.main;
    }
    /// <summary>
    /// ī�޶� Player ���� ���� �� ��Ż�� ���� �̵� �� ȸ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator MovingCamera()
    {
        GameManager.instance.FinishStage();
        UICanvas.SetActive(false);

        while (true)
        {
            Main.transform.position = Vector3.Lerp(Main.transform.position, CameraMovingPoint.transform.position, moveSpeed * Time.deltaTime);
            Main.transform.rotation = Quaternion.Slerp(Main.transform.rotation, CameraMovingPoint.transform.rotation, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(Main.transform.position, CameraMovingPoint.transform.position) < 0.1f
                && Quaternion.Angle(Main.transform.rotation, CameraMovingPoint.transform.rotation) < 0.1f)
            {
                Main.transform.position = CameraMovingPoint.transform.position;
                Main.transform.rotation = CameraMovingPoint.transform.rotation;
                break;
            }
            yield return null;
        }

        StartCoroutine(nameof(FadeOut));
    }
    /// <summary>
    /// ȭ�� FadeOut �� �� ��ȯ
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        Next_1?.Invoke();

        while (FadeImage.color.a < 1)
        {
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeImage.color.a + (Time.deltaTime / Speed));
            yield return null;
        }

        SoundManager.Instance.Save_prview_SliderVale();

        Utils.Instance.StageNumber++;
        Utils.Instance.CombineStageName();
    }
}
