using System.Collections.Generic;
using UnityEngine;

public class RobotPoolManager : MonoBehaviour
{
    [Tooltip("오브젝트 풀링")]
    [SerializeField]
    private GameObject Ball;
    private List<GameObject> ball_Left = new List<GameObject>();
    private List<GameObject> ball_Right = new List<GameObject>();

    [SerializeField]
    private GameObject BallParent;

    public static RobotPoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 총알 풀링
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public GameObject Get(int num)
    {
        GameObject select = null;

        if (num == 0)
        {
            foreach (var t_ball in ball_Left)
            {
                if (!t_ball.activeSelf)
                {
                    select = t_ball;
                    select.SetActive(true);
                    break;
                }
            }
            if (!select)
            {
                select = Instantiate(Ball, BallParent.transform);

                ball_Left.Add(select);
            }
        }
        else
        {
            foreach (var t_ball in ball_Right)
            {
                if (!t_ball.activeSelf)
                {
                    select = t_ball;
                    select.SetActive(true);
                    break;
                }
            }
            if (!select)
            {
                select = Instantiate(Ball, BallParent.transform);

                ball_Right.Add(select);
            }
        }
        return select;
    }
}
