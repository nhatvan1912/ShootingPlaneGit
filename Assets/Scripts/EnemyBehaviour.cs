using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int hp=5;
    public GameObject item1, item2, item3, item4, itemPlane, circleItem;
    private GameObject[] items;
    public static int numShoot = 0, numDeaths = 0;
    public GameObject exploPrefab;
    bool isDied = false;

    void Start()
    {
        items = new GameObject[] {item1, item2, item3, item4};
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Plane"))
        {
            DestroyEnemy();
        }
        else
        {
            hp -= PlayerBehaviour.power;
            if(hp <= 0 && isDied == false)
            {
                isDied = true;
                numShoot++;
                if(numShoot == 1|| numShoot == 5 || numShoot == 10)  
                {
                    int rand = Random.Range(0, items.Length);
                    SpawnItem(items[rand]);
                }
                if(numShoot == 15)
                    SpawnItem(itemPlane);
                DestroyEnemy();
            }
        }
    }
    void DestroyEnemy()
    {
        AudioManager.instance.Play("enemy_die");
        numDeaths++;
        if(numDeaths == SetupManager.instance.enemiesF1.Length)
        {
            SetupManager.instance.SecondWave();
        } 
        GameObject exploClone = Instantiate(exploPrefab, transform.position, Quaternion.identity);
        Destroy(exploClone, 0.5f);
        Destroy(gameObject);
    }
    void SpawnItem(GameObject item)
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-5,5)*0.15f, Random.Range(-5,5)*0.15f, 0);

        GameObject itemClone = Instantiate(item, transform.position, Quaternion.identity);
        if(itemClone != null)
            itemClone.transform.DOMove(spawnPosition, 0.3f).SetEase(Ease.Linear);

        if(item.name != "ItemPlane")
        {
            GameObject circleClone = Instantiate(circleItem, itemClone.transform.position, Quaternion.identity, itemClone.transform);
            if(circleClone != null)
                circleClone.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }
    
    void Update()
    {
        if(!IsVisibleToCamera())
        {
            Destroy(gameObject);
        }
    }
    private bool IsVisibleToCamera()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.y >= 0;
    }
}
