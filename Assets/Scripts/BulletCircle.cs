using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletCircle : MonoBehaviour
{
    [SerializeField] private float speed=15;
    private void Update()
    {
        transform.Translate(Vector2.up*speed*Time.deltaTime);
        transform.DOScale(new Vector3(5, 5, 5), 2f);
        if (!IsVisibleToCamera())
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsVisibleToCamera()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
            gameObject.SetActive(false);
    }
}
