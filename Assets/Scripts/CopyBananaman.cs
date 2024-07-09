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
    /// �� ĳ���ͺ� �ִϸ��̼� ���
    /// </summary>
    private void RandomDance()
    {
        animator.SetFloat("Dance", animationNumber);
    }
    /// <summary>
    /// �ִϸ��̼� �Ͻ����� �� ��� ����
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
