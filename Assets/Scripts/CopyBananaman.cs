using System.Collections;
using UnityEngine;

public class CopyBananaman : MonoBehaviour
{
    Animator animator;

    public float animationNumber;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        RandomDance();

        StartCoroutine(nameof(AnimatorPause));
    }
    /// <summary>
    /// 각 캐릭터별 애니메이션 재생
    /// </summary>
    private void RandomDance()
    {
        animator.SetFloat("Dance", animationNumber);
    }
    /// <summary>
    /// 애니메이션 일시정지 및 재생 구현
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimatorPause()
    {
        while (true)
        {
            if (GameManager.instance.isPause)
                animator.speed = 0;
            else
                animator.speed = 1;

            yield return null;
        }
    }
}
