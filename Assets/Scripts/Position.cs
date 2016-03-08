using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, .5f); // the .5f is roughly the size of the enemy ship sprite
    }
    
    
}
