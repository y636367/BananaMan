using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum SceneName { Stage0, Stage1, Stage2, Stage3, }
public class Utils : MonoBehaviour
{
    [SerializeField]
    public int StageNumber;
    [HideInInspector]
    public int sceneCount;

    private string StageName;
    [SerializeField]
    public KeyBinding binding;
    #region �̱���
    public static Utils Instance;

    public GameObject PrograssBar;
    public Text PrograssText;
    public SceneStart nowScene;

    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;                                          // ���� ����â�� ���� ��ϵ� ���� ���� Ȯ��
    }
    #endregion
    /// <summary>
    /// ���� ���� ���� �� �̸� Ȯ��
    /// </summary>
    /// <returns></returns>
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// �� ���� �Լ�
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName = "")
    {
        if (sceneName == "")                                                                          // �Ű� ���� ���� ����ִٸ� ���� ���� ���� ���� �����
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else { sceneName = sceneName.ToLower(); }                                                     // �ƴ϶�� �Ű����� ���� �̸����� ���� ������ �̵�
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
    /// <summary>
    /// �� Scene�� �̸� ������ ���Ͻ�Ű�� �ڿ� �ٴ� ���ڸ� �����Ͽ� �� �̵�
    /// </summary>
    public void CombineStageName()
    {
        if (StageNumber == sceneCount)
        {
            StageNumber = 0;                                                                         // �������� ��ȣ �ʱ�ȭ(���� ȣ�� �� ó�� ȭ������ �̵�)

            StopCoroutine(GameManager.instance.GameTImer());
            GameManager.instance.ClearC.gameObject.SetActive(true);
        }
        else
        {
            StageName = string.Format("Stage" + StageNumber);

            LoadScene(StageName);
        }
    }
    /// <summary>
    /// ���� �Լ�
    /// </summary>
    public void Exit_Program()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
