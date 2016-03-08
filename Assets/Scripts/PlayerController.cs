using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public float shipSpeed;
    public float padding = 0.5f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float fireRate = 0.2f;
    public float health;
    public AudioClip Hit;
    public AudioClip Death;
    float xmin;
    float xmax;
    

	// Use this for initialization
	void Start () {
	   float distance = transform.position.z - Camera.main.transform.position.z;
       Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
       Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
       xmin = leftMost.x + padding;
       xmax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
        playerMovement();
        positionClamp(); 
        if(Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("playerFire",0.000001f, fireRate);
        }  
        if(Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("playerFire");
        }            
	}
    
    void playerMovement(){
        // Not really sure what to do here on my own, but lets get the IF statements out of the way
         if (Input.GetKey(KeyCode.LeftArrow)){
            //transform.position = new Vector3(this.transform.position.x - shipSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            transform.position += Vector3.left*shipSpeed*Time.deltaTime;  
            // Time.deltaTime makes the ship movement independent of the framerate. Need larger number to make ship move at decent speed
        } else if (Input.GetKey(KeyCode.RightArrow)){
            //transform.position = new Vector3(this.transform.position.x + shipSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            transform.position += Vector3.right*shipSpeed*Time.deltaTime;
        }
    }
    
    void positionClamp(){
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z); 
    }
    
    void playerFire(){
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,projectileSpeed,0); // new code for unity5 and up
    }
    
    void OnTriggerEnter2D(Collider2D collider){
        enemyProjectile missile = collider.gameObject.GetComponent<enemyProjectile>();
        if(missile){
            health -= missile.getDamage();
            AudioSource.PlayClipAtPoint(Hit, transform.position);
            missile.Hit();
            if(health <=0){
                AudioSource.PlayClipAtPoint(Death, transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}
