using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int hp=5;
    public GameObject item1, item2, item3, item4, circleItem;
    public GameObject[] items;
    public static int num = 0;
    public GameObject exploPrefab;

    void Start()
    {
        items = new GameObject[] {item1, item2, item3, item4};
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        hp--;
        if(hp==0)
        {
            num++;
            if(num == 1 || num == 5 || num == 10 || num == 15)
                SpawnItem();
            GameObject exploClone = Instantiate(exploPrefab, transform.position, Quaternion.identity);
            Destroy(exploClone, 0.5f);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Plane"))
        {
            GameObject exploClone = Instantiate(exploPrefab, transform.position, Quaternion.identity);
            Destroy(exploClone, 0.5f);
            Destroy(gameObject);
        }
    }
    void SpawnItem()
    {
        int rand = Random.Range(0, items.Length);
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-5,5)*0.15f, Random.Range(-5,5)*0.15f, 0);

        GameObject itemClone = Instantiate(items[rand], transform.position, Quaternion.identity);
        if(itemClone != null)
            itemClone.transform.DOMove(spawnPosition, 0.3f).SetEase(Ease.Linear);
        GameObject circleClone = Instantiate(circleItem, itemClone.transform.position, Quaternion.identity, itemClone.transform);
        if(circleClone != null)
            circleClone.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
    
}
