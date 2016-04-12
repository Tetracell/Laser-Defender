using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

    public float gizmoRadius; // In case I want to change the size of the enemies again.

    public void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
    
    
}
