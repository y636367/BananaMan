using UnityEngine;

public class Respawn_Manager : MonoBehaviour
{
    [SerializeField]
    public Vector3 Default_RespawnPoint;                                                              // 기본적인 리스폰 포인트

    [SerializeField]
    public Vector3 New_RespawnPoint;                                                                  // 새롭게 생성된 리스폰 포인트

    public bool Default;
    public float NowPointHeight;

    public string nowPointName;
    private void Awake()
    {
        Default = true;
    }
    /// <summary>
    /// 미리 설정한 리스폰 포인트로 플레이어 강제이동
    /// </summary>
    /// <param name="t_playerPosition"></param>
    public void Transmission_Player(Player t_player)
    {
        Vector3 RespawnPoint;

        if(Default)
            RespawnPoint = Default_RespawnPoint;
        else
            RespawnPoint = New_RespawnPoint;

        t_player._hipsBone.position = RespawnPoint;
        t_player.transform.position = RespawnPoint;
        NowPointHeight = RespawnPoint.y;
    }
    /// <summary>
    /// 새로운 리스폰 포인트 생성시 기존 포인트들 교체
    /// </summary>
    /// <param name="newPoint"></param>
    public void NewRespawnPoint(Vector3 newPoint)
    {
        if(!Default)
        {
            Default_RespawnPoint = New_RespawnPoint;
        }
        else
        {
            Default = false;
        }

        New_RespawnPoint = newPoint;
    }
}
