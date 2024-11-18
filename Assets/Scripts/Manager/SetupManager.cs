using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class SetupManager : MonoBehaviour
{
    public GameObject BolaToa, BoMuoiDo, BoMuoiXanh, BoMuoiXanhla, BoOngVang, BoOngXanh, BoOngXanhla, RuoiGiamVang, RuoiOngDo, RuoiOngXanhla, Boss;
    public GameObject[] enemiesF1, enemiesF2;
    GameObject plane;
    Transform Phase1, Phase2, Phase3;
    float movespeed = 5f;
    public static SetupManager instance;
    [SerializeField] float timeDurationF2=10f, moveDistanceF2=30f;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Phase1 = transform.GetChild(0);
        Phase2 = transform.GetChild(1);
        Phase3 = transform.GetChild(2);
        enemiesF1 = new GameObject[Phase1.childCount];
        enemiesF1 = Phase1.GetComponentsInChildren<Transform>()
                         .Where(t => t != Phase1) 
                         .Select(t => t.gameObject) 
                         .ToArray();
        enemiesF2 = new GameObject[] {BoMuoiDo, BoMuoiXanh, BoMuoiXanhla, BoOngVang, BoOngXanh, BoOngXanhla, RuoiGiamVang, RuoiOngDo, RuoiOngXanhla, BolaToa};
        plane = GameObject.FindWithTag("Plane");
        FirstWave();
    }
    public void FirstWave()
    {
        StartCoroutine(ArrangeEnemy(plane, new Vector3(0f, -2.5f, 0f)));
        for(int i=0; i<enemiesF1.Length; i++)
        {
            Vector3 pos = enemiesF1[i].transform.position - new Vector3(0, 6, 0);
            StartCoroutine(ArrangeEnemy(enemiesF1[i], pos));
        }
    }
    IEnumerator ArrangeEnemy(GameObject enemy, Vector3 pos)
    {
        while (enemy.transform.position != pos)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, pos, Time.deltaTime * movespeed);
            yield return null;
        }
    }

    GameObject enemy;
    public void SecondWave()
    {
        for(int i=0; i<enemiesF2.Length; i++)
        {
            if(i == enemiesF2.Length-1)
            {
                for(int j=-1; j<=1; j++)
                {
                    enemy = Instantiate(enemiesF2[i], gameObject.transform.position + 0.6f*new Vector3(2.3f*j, i*5 - 1.5f, -0.1f), Quaternion.identity, Phase2);
                    MoveEnemyDown(enemy);
                    if(j!=0)
                    {
                        enemy = Instantiate(enemiesF2[i], gameObject.transform.position + 0.6f*new Vector3(1.4f*j, i*5 + 0.5f, -0.1f), Quaternion.identity, Phase2);
                        MoveEnemyDown(enemy);
                    }
                }
            }
            else
            {
                for(int j=-2; j<=2; j++)
                {
                    for(int k=-3; k<=3; k++)
                    {
                        enemy = Instantiate(enemiesF2[i], gameObject.transform.position + 0.6f*new Vector3(k*1.3f, j + i*5, -0.1f), Quaternion.identity, Phase2);
                        MoveEnemyDown(enemy);
                    }
                }
            }
        }
        Invoke("ThirdWave", timeDurationF2);
    }
    
    void MoveEnemyDown(GameObject enemy)
    {
        if(enemy != null)
            enemy.transform.DOMoveY(enemy.transform.position.y - moveDistanceF2, timeDurationF2).SetEase(Ease.Linear);
    }

    void ThirdWave()
    {
        PlayerBehaviour.phase3Comming = true;
        GameObject bossClone = Instantiate(Boss, transform.position + Vector3.back*0.1f, Quaternion.identity, Phase3);
        bossClone.transform.DOMoveY(0.6f, 0.5f).SetEase(Ease.Linear);
    }
}
