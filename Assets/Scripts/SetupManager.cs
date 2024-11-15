using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    public GameObject BolaToa, BoMuoiDo, BoMuoiXanh, BoMuoiXanhla, BoOngVang, BoOngXanh, BoOngXanhla, RuoiGiamVang, RuoiOngDo, RuoiOngXanhla;
    GameObject[] enemiesF1;
    GameObject plane;
    float movespeed = 5f;
    private static int wave = 1;
    void Start()
    {
        enemiesF1 = GameObject.FindGameObjectsWithTag("Enemy");
        plane = GameObject.FindWithTag("Plane");
        FirstWave();
    }
    void FirstWave()
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
}
