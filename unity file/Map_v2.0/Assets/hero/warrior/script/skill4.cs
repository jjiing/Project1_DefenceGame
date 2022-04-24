using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill4 : Warrior
{

   void Start()
    {
        Debug.Log("±√±ÿ±‚ Ω√¿€");
        Invoke("DestroyCircle", 3f); 
    }


    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {

           // collision.GetComponent<Monster>().monterHP -= warriorUltAtk;
        }
    }

    private void DestroyCircle()
    {
        gameObject.SetActive(false);
    }
}
