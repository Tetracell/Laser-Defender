using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    
    
    private bool movingRight = true;
    //private bool movingLeft = false;
    public float speed = 5f;
    private float xmax;
    private float xmin;

    public AudioClip youWin;
    public float spawnDelay;
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;

    // Use this for initialization
    void Start () {
	   //The dreaded for loop
       float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
       Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
       Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
       xmax = rightEdge.x;
       xmin = leftEdge.x;
        // enemySpawn();       
       spawnUntilFull();
	}
	
    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }    
    
	// Update is called once per frame
	void Update () {
        if(movingRight){
            transform.position += new Vector3(speed * Time.deltaTime, 0);
            // or : Vector3.right * speed * Time.deltaTime
        } else {
            transform.position += new Vector3(-speed*Time.deltaTime,0);
        }
        float rightFormEdge = transform.position.x + (0.5f*width);
        float leftFormEdge = transform.position.x - (0.5f*width);
        if(leftFormEdge < xmin){
            movingRight = true;
        } else if (rightFormEdge > xmax){
            movingRight = false;
        }
        if (AllMembersDead()){
            Debug.Log("Empty Formation");
            spawnUntilFull();
            AudioSource.PlayClipAtPoint(youWin, transform.position); // The 'You win' noise.
        }
	}

    bool AllMembersDead() // checks to see if all enemies are dead
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    void enemySpawn()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void spawnUntilFull()
    {
        Transform freePosition = nextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (nextFreePosition())
        {
            Invoke("spawnUntilFull", spawnDelay);
        }
               
    }

    Transform nextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }  
        }
        return null;
    }
}
