using UnityEngine;
using System.Collections;

public class basicEnemy : MonoBehaviour {
    
    public float hitPoints = 150f;   
    public AudioClip explosion;
    public AudioClip notDestroyed;
    public GameObject enemyProjectile;
    public float projectileSpeed = 10;
    public float shotsPerSecond; // set in inspector
    
    void OnTriggerEnter2D(Collider2D col){      
        playerProjectile laser = col.gameObject.GetComponent<playerProjectile>();
        if(laser){
            hitPoints -= laser.getDamage();
            laser.Hit();
            if (hitPoints <= 0){
                AudioSource.PlayClipAtPoint(explosion, transform.position);
                Destroy(this.gameObject);
            } else {
                AudioSource.PlayClipAtPoint(notDestroyed, transform.position); // simple hit sound when enemy is not destroyed
            }
        }
        
    }
    
    void Update(){
        float probabilty = Time.deltaTime * shotsPerSecond;
        if(Random.value < probabilty){
            enemyFire();
        }
        //enemyFire();
    }
    
    public void enemyFire(){
        GameObject enemyBeam = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        enemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-projectileSpeed,0); // new code for unity5 and up
    }
}
