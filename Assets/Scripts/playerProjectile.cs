using UnityEngine;
using System.Collections;

public class playerProjectile : MonoBehaviour {
    
    public float damage = 100f;
    
    public void Hit(){             // void means this function isn't returning anything
        Destroy(gameObject);
    }
    
    public float getDamage(){
        return damage;
    }
    
/*
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.GetComponent<basicEnemy>()){
            Destroy(col.gameObject);  
            Destroy(gameObject); // This is the code that will destroy the laser on collision. this.gameObject is implied
        }
               
    }
*/


}
