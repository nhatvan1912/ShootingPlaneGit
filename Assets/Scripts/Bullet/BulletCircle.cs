using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletCircle : MonoBehaviour
{
    [SerializeField] private float speed=15;
    // void OnEnable()
    // {
    //     transform.DOScale(4f, 1f).SetEase(Ease.Linear).OnComplete(() => {transform.localScale = Vector3.one;});
    //     // Debug.Log("Enabled");
    // }
    // void OnDisable()
    // {
    //     transform.localScale = Vector3.one;
    // }
    private void Update()
    {
        transform.Translate(Vector2.up*speed*Time.deltaTime);
        if (!IsVisibleToCamera())
        {
            ObjectPooling.instance.CoolObject(gameObject, PoolObjectType.BulletCircle);
        }
    }

    private bool IsVisibleToCamera()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            ObjectPooling.instance.CoolObject(gameObject, PoolObjectType.BulletCircle);
    }
}
