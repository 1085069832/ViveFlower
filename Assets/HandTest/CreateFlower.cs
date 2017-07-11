using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 重新生长花
/// </summary>
public class CreateFlower : MonoBehaviour
{
    [SerializeField] GameObject flower;
    bool isTimer;
    bool isStartInstance;
    [SerializeField] float instanceTime;
    FlowerGrower fg;
    Vector3 flowerPos;
    [SerializeField] float dieTime = 5f;

    private void Update()
    {
        if (isTimer)
        {
            instanceTime += Time.deltaTime;
            if (instanceTime > dieTime)
            {
                fg = GameObject.FindGameObjectWithTag("Flower").GetComponent<FlowerGrower>();
                flowerPos = fg.transform.position;
                fg.Die();
            }
        }

        if (fg && fg.IsDead())
        {
            GameObject[] currentFlowers = GameObject.FindGameObjectsWithTag("Flower");
            foreach (GameObject flower in currentFlowers)
            {
                Destroy(flower);
            }
            instanceTime = 0;
            isTimer = false;
            Instantiate(flower, flowerPos, Quaternion.identity);
        }
    }

    public void InstanceFlower()
    {
        isTimer = true;
    }
}
