using UnityEngine;
using System.Collections.Generic;
using System;

public class Respawn_Manager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [Space(10f)]
    [SerializeField]
    public Vector3 Default_RespawnPoint;                                                              // 기본적인 리스폰 포인트

    [SerializeField]
    public Vector3 New_RespawnPoint;                                                                  // 새롭게 생성된 리스폰 포인트

    [SerializeField]
    public List<Respawn> respawns;

    public bool Default;
    public float NowPointHeight;

    public string nowPointName;
    public int nowPointNumber;

    private void Awake()
    {
        Default = true;

        foreach(Transform child in transform)
        {
            Respawn t_Respawn = child.GetComponent<Respawn>();

            if(t_Respawn != null)
            {
                respawns.Add(t_Respawn);
            }
        }
    }
    /// <summary>
    /// 미리 설정한 리스폰 포인트로 플레이어 강제이동
    /// </summary>
    /// <param name="t_playerPosition"></param>
    public void Transmission_Player()
    {
        Vector3 RespawnPoint;

        if (Default)
            RespawnPoint = Default_RespawnPoint;
        else
        {
            RespawnPoint = New_RespawnPoint;

            for(int i = 0; i < nowPointNumber; i++)
            {
                respawns[i].Flag = true;

                if (respawns[i].Lamp != null)
                    respawns[i].Lamp.SetActive(true);
            }
        }

        player._hipsBone.position = RespawnPoint;
        player.transform.position = RespawnPoint;
        NowPointHeight = RespawnPoint.y;
    }
    /// <summary>
    /// 새로운 리스폰 포인트 생성시 기존 포인트들 교체
    /// </summary>
    /// <param name="newPoint"></param>
    public void NewRespawnPoint(Vector3 newPoint)
    {
        Default_RespawnPoint = New_RespawnPoint;

        New_RespawnPoint = newPoint;
    }
}
