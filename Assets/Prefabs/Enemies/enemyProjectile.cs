using UnityEngine;
using System.Collections;

public class enemyProjectile : MonoBehaviour {

    public float damage = 75f;
    public float speed;
    
    public void Hit(){             // void means this function isn't returning anything
        Destroy(gameObject);
    }
    
    public float getDamage(){
        return damage;
    }

}
