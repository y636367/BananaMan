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
    [SerializeField]
    public TransformData cleardata;
    #region 싱글톤
    public static Utils Instance;

    public GameObject PrograssBar;
    public Text PrograssText;
    public SceneStart nowScene;

    private void Awake()
    {
        if (Instance == null)
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
        sceneCount = SceneManager.sceneCountInBuildSettings;                                          // 빌드 설정창에 현재 등록된 씬의 개수 확인
    }
    #endregion
    /// <summary>
    /// 현재 실행 중인 씬 이름 확인
    /// </summary>
    /// <returns></returns>
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// 씬 변경 함수
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName = "")
    {
        if (sceneName == "")                                                                          // 매개 변수 값이 비어있다면 현재 실행 중인 씬을 재실행
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else { sceneName = sceneName.ToLower(); }                                                     // 아니라면 매개변수 값을 이름으로 갖는 씬으로 이동
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
    /// <summary>
    /// 각 Scene의 이름 형식을 통일시키고 뒤에 붙는 숫자만 변경하여 씬 이동
    /// </summary>
    public void CombineStageName()
    {
        if (StageNumber == sceneCount)
        {
            StageNumber = 0;                                                                         // 스테이지 번호 초기화(다음 호출 시 처음 화면으로 이동)

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
    /// 종료 함수
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