using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollect : MonoBehaviour
{
    private bool isMoving = false;
    private Tween moveTween;
    void Update()
    {
        if (!isMoving)
        {
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Plane"))
                {
                    isMoving = true;
                    moveTween = transform.DOMove(collider.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(() => CollectComplete());
                }
            }
        }
    }

    void CollectComplete()
    {
        isMoving = false;
        Destroy(gameObject);
        PlayerBehaviour.energyCount += 1;
    }
}
