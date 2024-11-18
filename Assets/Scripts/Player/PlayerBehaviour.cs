using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBehaviour : MonoBehaviour
{
    public PlaneData[] planeData;
    public Camera cam;
    public GameObject playerBullet, firePos1, firePos2, firePos3, taptap;
    private float reloadSpeed = 0.2f, reloading;
    public static int energyCount = 0, power;
    private bool running, transfer;
    public static bool phase3Comming;
    private SpriteRenderer planeImg;
    public Animator animator, nhaydo;
    Vector3 originalPos;

    void Start()
    {
        running = transfer = phase3Comming = false;
        originalPos = new Vector3(0,-2f,0);
        power = 1;
        planeImg = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(phase3Comming == false)
        {
            if (Input.GetMouseButton(0))
            {
                ItemCollect.canFall = true;
                if (!running)
                {
                    taptap.SetActive(false);
                    running = true;
                    AudioManager.instance.Play("bg_play");
                }
                MovePlayer();
                HandleShooting();
            }
            else{
                ItemCollect.canFall = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, Time.deltaTime*5f);
        }
    }

    void MovePlayer()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
    }

    void HandleShooting()
    {
        if (reloading <= 0)
        {
            reloading = reloadSpeed;
            OnShooting();
        }
        else
        {
            reloading -= Time.deltaTime;
        }
    }

    void OnShooting()
    {
        AudioManager.instance.Play("boss_die");
        switch (energyCount)
        {
            case <= 1:
                for (int i = -1; i <= 1; i += 2)
                {
                    SpawnBullets(firePos1, new Vector3(i*0.13f, 1f, 0f),0.05f);
                    SpawnBullets(firePos2, new Vector3(i*0.13f, 1f, 0f),0.05f);
                }
                break;

            case 2:
            case 3:
                if (energyCount == 3) reloadSpeed = 0.1f;
                for (int i = -1; i <= 1; i += 1)
                {
                    SpawnBullets(firePos1, new Vector3(i * 0.25f, 1.5f, 0), 0.05f);
                    SpawnBullets(firePos2, new Vector3(i * 0.25f, 1.5f, 0), 0.05f);
                }
                break;

            case 4:
                if(!transfer)
                {
                    ChangePlane();
                    transfer = true;
                    power = 3;
                    reloadSpeed = 0.08f;
                }
                SpawnBulletsCircle(firePos1);
                SpawnBulletsCircle(firePos2);
                for (int i = -3; i <= 3; i++)
                {
                    SpawnBullets(firePos3, new Vector3(i * 0.25f, 3f, 0), 0.1f);
                }
                break;
        }
    }
    void SpawnBullets(GameObject firePos, Vector3 bulletPos, float duration)
    {
        GameObject bulletClone = ObjectPooling.instance.GetPoolObject(PoolObjectType.Bullet);
        if (bulletClone != null)
        {
            bulletClone.transform.position = firePos.transform.position;
            if (bulletClone.transform != null)
                bulletClone.transform.DOMove(firePos.transform.position + bulletPos, duration).SetEase(Ease.Linear);
            bulletClone.transform.rotation = transform.rotation;
            bulletClone.SetActive(true);
        }
    }

    void SpawnBulletsCircle(GameObject firePos)
    {
        GameObject bulletClone = ObjectPooling.instance.GetPoolObject(PoolObjectType.BulletCircle);
        if (bulletClone != null)
        {
            bulletClone.transform.position = firePos.transform.position;
            bulletClone.transform.rotation = transform.rotation;
            bulletClone.SetActive(true);
        }
    }

    void ChangePlane()
    {
        planeImg.sprite = planeData[1].planeImage;
        animator.runtimeAnimatorController = planeData[1].controller;
        if (animator != null)
        {
            animator.Play("Plane2_appear");
            AudioManager.instance.Play("win");
        }
    }
    bool canTurnRed = true;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && canTurnRed == true)
        {
            nhaydo.Play("nhaydo");
            if(transfer)
            {
                animator.Play("Plane2_losshealth");
            }
            else
                animator.Play("Plane1_losshealth");
            canTurnRed = false;
            Invoke("TurnRed", 2f);
        }
    }
    void TurnRed()
    {
        canTurnRed = true;
    }

}
