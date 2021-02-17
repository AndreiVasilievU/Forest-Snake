using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFood : MonoBehaviour
{
    public GameObject FoodPrefab;

    public void Start()
    {
        SpawnFood();
    }
    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("Food") == null)
        {
            SpawnFood();
        }
        else
        {
            return;
        }
    }

    public void SpawnFood()
    {
        GameObject go = Instantiate<GameObject>(FoodPrefab);
        go.transform.position = new Vector3(Random.Range(-18, 18), 0, Random.Range(-18, 18));
    }
}
