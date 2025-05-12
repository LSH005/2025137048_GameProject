using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGame : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public float[] fruitSizes = { 0.5f, 0.7f, 0.9f,1.1f,1.3f,1.5f,1.7f,1.9f};

    //
    public GameObject currentFruit;
    public int currentFruitType;

    public float fruitStartHeight = 6f;

    public float gameWidth = 5f;

    public bool isGameOver = false;

    public Camera mainCamera;

    public float fruitTimer;

    public float gameHight;

    void Start()
    {
        mainCamera=Camera.main;
        SpawnNewFruit(); // 게임 시작 시 첫 과일 생성
        fruitTimer = -1019.1019f;
    }


    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (fruitTimer >= 0)
        {
            fruitTimer -= Time.deltaTime;
        }

        if (fruitTimer < 0&&fruitTimer>-2)
        {
            CheckGameOver();
            SpawnNewFruit();
            fruitTimer = -1019.1019f;
        }

        if (currentFruit != null)
        {
            Vector3 mousePosition = Input.mousePosition;  //마우스 위치를 따라 X좌표만 이동하기 위해 사용
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            Vector3 newPosition = currentFruit.transform.position;
            newPosition.x = worldPosition.x;

            float halfFruitSize = fruitSizes[currentFruitType] / 2;
            if (newPosition.x < -gameWidth / 2 + halfFruitSize)
            {
                newPosition.x = -gameWidth / 2 + halfFruitSize;
            }

            if (newPosition.x > gameWidth / 2 - halfFruitSize)
            {
                newPosition.x = gameWidth / 2 - halfFruitSize;
            }

            currentFruit.transform.position = newPosition;
        }

        if (Input.GetMouseButtonDown(0)&&fruitTimer==-1019.1019f)
        {
            DropFruit();
        }
    }

    public void MergeFruits(int fruitType, Vector3 position)
    {
        if (fruitType < fruitPrefabs.Length - 1)
        {
            GameObject newFruit=Instantiate(fruitPrefabs[fruitType+1],position,Quaternion.identity);
            newFruit.transform.localScale= new Vector3(fruitSizes[fruitType + 1], fruitSizes[fruitType+1],1.0f);
        }
    }

    void SpawnNewFruit()
    {
        if (!isGameOver)
        {
            currentFruitType=UnityEngine.Random.Range(0, 3);

            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = mainCamera.WorldToScreenPoint(mousePosition);

            Vector3 spawnPosition = new Vector3(worldPosition.x, fruitStartHeight, 0);

            float halfFruitSize = fruitSizes[currentFruitType] / 2;
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, -gameWidth / 2 + halfFruitSize, gameWidth / 2 - halfFruitSize);

            currentFruit = Instantiate(fruitPrefabs[currentFruitType], spawnPosition, Quaternion.identity);
            currentFruit.transform.localScale=new Vector3(fruitSizes[currentFruitType],fruitSizes[currentFruitType],1f);

            Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
            }
        }
    }

    void DropFruit()
    {
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.gravityScale = 1f;
            currentFruit=null;

            fruitTimer = 1.0f;
        }
    }

    void CheckGameOver()
    {
        Fruit[] allFruit=FindObjectsOfType<Fruit>();

        float gameOverHight = gameHight;

        for (int i = 0; i < allFruit.Length; i++)
        {
            Rigidbody2D rb = allFruit[i].GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.magnitude < 0.1f && allFruit[i].transform.position.y > gameOverHight)
            {
                isGameOver = true;
                Debug.Log("G A M E !");

                break;
            }
        }
    }
}
