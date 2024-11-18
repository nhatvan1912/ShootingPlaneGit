using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BossAttack : MonoBehaviour
{
    #region Inspector
    [SpineAnimation]
    public string idleAnimationName;

    [SpineAnimation]
    public string attack1AnimationName;

    [SpineAnimation]
    public string attack2AnimationName;

    public float waitAttack = 2.5f;

    public float moveDownDistance = 1f; 
    public float moveSpeed = 2f;      
    #endregion

    SkeletonAnimation bossAnimation;
    public Spine.AnimationState spineBossState;
    public Spine.Skeleton skeleton;

    private Vector3 originalPosition;

    void Start()
    {
        bossAnimation = GetComponent<SkeletonAnimation>();
        spineBossState = bossAnimation.AnimationState;
        skeleton = bossAnimation.Skeleton;

        originalPosition = new Vector3(0f, 0.6f, 0f); 

        StartCoroutine(DoFirstAttack());
    }

    IEnumerator DoFirstAttack()
    {
        spineBossState.SetAnimation(0, idleAnimationName, true);
        yield return new WaitForSeconds(waitAttack);

        yield return StartCoroutine(PerformAttack(attack1AnimationName));

        StartCoroutine(DoAttackRoutine());
    }

    IEnumerator DoAttackRoutine()
    {
        while (true)
        {
            spineBossState.SetAnimation(0, idleAnimationName, true);
            yield return new WaitForSeconds(waitAttack);

            yield return StartCoroutine(PerformAttack(attack2AnimationName));
            yield return new WaitForSeconds(waitAttack);

            yield return StartCoroutine(PerformAttack(attack1AnimationName));
            yield return new WaitForSeconds(1.3f);
        }
    }

    IEnumerator PerformAttack(string attackAnimationName)
    {
        float animationDuration = spineBossState.SetAnimation(0, attackAnimationName, false).Animation.Duration;
        StartCoroutine(MoveBossDown());

        yield return new WaitForSeconds(animationDuration);

        StartCoroutine(MoveBossUp());
        spineBossState.SetAnimation(0, idleAnimationName, true);
    }

    IEnumerator MoveBossDown()
    {
        Vector3 targetPosition = originalPosition - new Vector3(0, moveDownDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveBossUp()
    {
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
