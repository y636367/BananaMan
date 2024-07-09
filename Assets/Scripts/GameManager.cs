using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]
    public class After_ : UnityEvent { };                                                       // �̺�Ʈ ������ ���� �ν��Ͻ� Ŭ���� ����
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

    #region �̱���
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
                    if (Input.GetKeyDown(Utils.Instance.binding.Bindings[Action.Option]))                                   // Option ȣ��
                    {
                        SP.Window_S();
                        Pause();
                    }
                }
                catch (KeyNotFoundException e) { }

                if (InGame)
                {
                    if (Input.GetKeyDown(Utils.Instance.binding.Bindings[Action.ForcedDeath])
                        && !Player.instance.playerDeath)                                                                    // ���� ������
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
    /// �Ͻ����� �� �޴� ȣ��
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
    /// ���� ���(���������� �̵�)
    /// </summary>
    public void ForcedDeath()
    {
        isForcedDeath = true;
        Player.instance.ForcedDeath();
    }
    /// <summary>
    /// �������� Ŭ����
    /// </summary>
    public void FinishStage()
    {
        Player.instance.StageFinish();
        isStart = false;
    }
    /// <summary>
    /// ���ε� �� Ű�� �� �Է°���
    /// </summary>
    private void CheckInputKey()
    {
        InputKey = false;

        foreach (Action action in Enum.GetValues(typeof(Action)))                                                   // ������ Enum�� �� ��������(Enum.GetValues(typeof(�̸�)))
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
    /// InGame���� �÷��̾ ���۰��� �ൿ�� �Ͽ����� ����Ȯ�� ��, ���� �ð� ���� ������ Ȯ�ε��� �ʾҴٸ� Idle �ִϸ��̼����� ��ȯ
    /// </summary>
    private void ControlCheck()
    {
        if(!InputKey&&!CameraisMoving)                                                                      // ī�޶�����Ӱ� ���� �����Է��� �����ٸ�
        {
            if (!NowBoard)
            {
                count += Time.deltaTime;

                if (count >= HoldTime)                                                                      // ������ �ð� �̻� �Է��� �����ٸ�
                {
                    NowBoard = true;                                                                        // �÷��� ���� �� ��ȯ �� Player�� ���� ��ȯ
                    count = 0;
                    Player.instance.PlayerBoard();
                }
            }
        }
        else                                                                                                // ���� Ȯ�� �� �ʱ�ȭ
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
    /// FadeIn �Ϸ� �� ���� �����ϰ� �� ���Ǻ��� �� �� �ε�� ���� �ʱ�ȭ
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
