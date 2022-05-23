using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawn : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    void Start()
    {
       StartCoroutine(SelectSpawn());       
    }

    IEnumerator SelectSpawn()
    {
        while (true)
        {
            float range = Random.Range(1, 50);

            if (range == 1)
            {
                
                spawn1.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else if (range == 2)
            {
                spawn2.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else if (range == 3)
            {
                spawn3.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else if (range == 4)
            {
                spawn4.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return new WaitForSeconds(5);
            }

            yield return null;
        }

    }
}
