using UnityEngine;
using System.Collections;

public class Projectile_Shredder : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col){
        Destroy(col.gameObject);        
    }
}
