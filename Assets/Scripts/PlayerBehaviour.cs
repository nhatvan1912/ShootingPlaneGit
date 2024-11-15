using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Animations;

public class PlayerBehaviour : MonoBehaviour
{
    public PlaneData[] planeData;
    public Camera cam;
    public GameObject playerBullet, firePos1, firePos2, taptap;
    float reloadSpeed = 0.2f, reloading;
    public static int energyCount = 0;
    bool running = false, transfer = false;
    SpriteRenderer planeImg;
    Animator animator;

    void Start()
    {
        planeImg = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Debug.Log(animator);
        
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(running == false)
            {
                taptap.SetActive(false);
                running = true;
            }
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            if(reloading <= 0)
            {
                reloading = reloadSpeed;
                OnShooting();
            }
            else reloading -= Time.deltaTime;
        }
    }

    void OnShooting()
    {
        Debug.Log(energyCount);
        if(energyCount <= 1)
        {
            for(int i = -1; i <= 1; i+=2)
            {   
                GameObject bulletClone1 = ObjectPooling.instance.GetPoolObject();
                if (bulletClone1 != null)
                {
                    bulletClone1.transform.position = firePos1.transform.position + Vector3.right * (i*0.13f);
                    bulletClone1.transform.rotation = transform.rotation;
                    bulletClone1.SetActive(true);
                }
                GameObject bulletClone2 = ObjectPooling.instance.GetPoolObject();
                if (bulletClone2 != null)
                {
                    bulletClone2.transform.position = firePos2.transform.position + Vector3.right * (i*0.13f);
                    bulletClone2.transform.rotation = transform.rotation;
                    bulletClone2.SetActive(true);
                }
            }
        }
        else if(energyCount == 2 || energyCount == 3)
        {
            if(energyCount == 3) reloadSpeed = 0.1f;
            for(int i = -1; i <= 1; i+=1)
            {   
                GameObject bulletClone1 = ObjectPooling.instance.GetPoolObject();
                if (bulletClone1 != null)
                {
                    bulletClone1.transform.position = firePos1.transform.position;
                    if(bulletClone1.transform != null)
                        bulletClone1.transform.DOMove(firePos1.transform.position+new Vector3(i*0.25f, 1f, 0), 0.05f);
                    bulletClone1.transform.rotation = transform.rotation;
                    bulletClone1.SetActive(true);
                }
                GameObject bulletClone2 = ObjectPooling.instance.GetPoolObject();
                if (bulletClone2 != null)
                {
                    bulletClone2.transform.position = firePos2.transform.position;
                    if(bulletClone2.transform != null)
                        bulletClone2.transform.DOMove(firePos2.transform.position+new Vector3(i*0.25f, 1f, 0), 0.05f);
                    bulletClone2.transform.rotation = transform.rotation;
                    bulletClone2.SetActive(true);
                }
            }
        }
        else if(energyCount == 4 && transfer == false)
        {
            ChangePlane();
            transfer = true;
        }
    }
    void ChangePlane()
    {
        planeImg.sprite = planeData[1].planeImage;
        animator.runtimeAnimatorController = planeData[1].controller;
        if(animator != null)
        {
            animator.Play("Plane2_appear");
        }
    }
}
