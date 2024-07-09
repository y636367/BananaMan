using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // 이벤트 적용을 위한 인스턴스 클래스 생성
    public After_ Option_On;
    public After_ Option_Close;
    public After_ Next_3;

    [Space(10f)]
    public Respawn_Manager rm;
    public EffectPool effectpool;
    public ClearCanvas ClearC;

    [HideInInspector]
    public SoundPack SP;

    [Space(10f)]
    [SerializeField]
    private float HoldTime;
    [SerializeField]
    private float count = 0;
    //[HideInInspector]
    public int totalDeath = 0;
    //[HideInInspector]
    public float totalPlayTime;

    [Space(10f)]
    public bool isStart;
    public bool isPause;
    public bool InGame;
    public bool isForcedDeath;
    public bool DamageOn;

    [Space(10f)]
    public bool CameraisMoving;
    public bool InputKey;
    public bool NowBoard;
    public bool isBinding;

    [Space(10f)]
    public bool TimeScale;
    public float TImeScale_Value;

    [HideInInspector]
    public bool isClear;

    //[HideInInspector]
    public bool isSceneLoad;

    #region 싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    private void Start()
    {
        InGame = false;
        DamageOn = false;
    }
    private void Update()
    {
        if (!isStart)
            return;

        if (!isSceneLoad)
        {
            if (!isBinding)
            {
                try
                {
                    if (Input.GetKeyDown(Utils.Instance.binding.Bindings[Action.Option]))                                   // Option 호출
                    {
                        SP.Window_S();
                        Pause();
                    }
                }
                catch (KeyNotFoundException e) { }

                if (InGame)
                {
                    if (Input.GetKeyDown(Utils.Instance.binding.Bindings[Action.ForcedDeath])
                        && !Player.instance.playerDeath)                                                                    // 강제 리스폰
                    {
                        ForcedDeath();
                    }

                    CheckInputKey();
                    ControlCheck();
                }
            }
            if (TimeScale)
                Test();
        }
    }
    /// <summary>
    /// 일시정지 및 메뉴 호출
    /// </summary>
    public void Pause()
    {
        if (!isPause)
        {
            isPause = true;

            if (InGame)
            {
                Player.instance.isPause();
                SoundManager.Instance.Pause_Sfx();
                CameraController.instance.Cursor_UnLocking_Show();
            }
            Option_On?.Invoke();
        }
        else
        {
            isPause = false;

            if (InGame)
            {
                Player.instance.isResume();
                SoundManager.Instance.UnPause_Sfx();
                CameraController.instance.Cursor_Locking_Hide();
            }
            Option_Close?.Invoke();
        }
    }
    /// <summary>
    /// 강제 사망(리스폰으로 이동)
    /// </summary>
    public void ForcedDeath()
    {
        isForcedDeath = true;
        Player.instance.ForcedDeath();
    }
    /// <summary>
    /// 스테이지 클리어
    /// </summary>
    public void FinishStage()
    {
        Player.instance.StageFinish();
        isStart = false;
    }
    /// <summary>
    /// 바인딩 된 키들 중 입력감지
    /// </summary>
    private void CheckInputKey()
    {
        InputKey = false;

        foreach (Action action in Enum.GetValues(typeof(Action)))                                                   // 선언한 Enum의 값 가져오기(Enum.GetValues(typeof(이름)))
        {
            if (action == Action.None)
                continue;

            if (Input.GetKey(Utils.Instance.binding.Bindings[action]))
            {
                InputKey = true;
                break;
            }
        }
    }
    /// <summary>
    /// InGame에서 플레이어가 조작관련 행동을 하였는지 여부확인 후, 일정 시간 동안 조작이 확인되지 않았다면 Idle 애니메이션으로 전환
    /// </summary>
    private void ControlCheck()
    {
        if(!InputKey&&!CameraisMoving)                                                                      // 카메라움직임과 관련 조작입력이 없었다면
        {
            if (!NowBoard)
            {
                count += Time.deltaTime;

                if (count >= HoldTime)                                                                      // 정해진 시간 이상 입력이 없었다면
                {
                    NowBoard = true;                                                                        // 플래그 변수 값 전환 및 Player의 상태 전환
                    count = 0;
                    Player.instance.PlayerBoard();
                }
            }
        }
        else                                                                                                // 조작 확인 시 초기화
        {
            count = 0;
            NowBoard = false;
            Player.instance.PlayerNomal();
        }
    }
    public void Test()
    {
        Time.timeScale = TImeScale_Value;
    }
    /// <summary>
    /// FadeIn 완료 후 조작 가능하게 끔 조건변수 값 씬 로드시 마다 초기화
    /// </summary>
    private void OnEnable()
    {
        isStart = false;
        DamageOn = false;
    }
    public IEnumerator GameTImer()
    {
        while (true)
        {
            if (isPause)
                yield return null;

            totalPlayTime += Time.deltaTime;
            yield return null;
        }
    }
}
