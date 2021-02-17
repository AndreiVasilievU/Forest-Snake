using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlane : MonoBehaviour
{
    public List<GameObject> Planes;
    public Text scoreGT;
    public string text;
    public int max=1;

    public void Start()
    {
        scoreGT = GetComponent<Text>();
    }
    public void Update()
    {
        SpawningPlane();
    }

    public void SpawningPlane()
    {
        text = scoreGT.text;
        text = text.Substring(11);
        int count = int.Parse(text);
        
        if(count > max)
        {
            max += 2;
            var Plane = Instantiate(Planes[Random.Range(0, 5)]);
            Plane.transform.position = new Vector3(Random.Range(-17,17),0,Random.Range(-17,17));
            float ndx = Random.Range(2, 5);
            Plane.transform.localScale *= ndx;
        }
    }
}
