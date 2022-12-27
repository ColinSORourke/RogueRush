using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool dragMode = false;
    public bool snapped = false;
    public Transform snappedTo;

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Entered another collider");
        if (other.gameObject.layer == LayerMask.NameToLayer("TowerTiles")){
            Debug.Log("Trying to snap");
            snapped = true;
            var otherPos = other.gameObject.transform.position;
            this.transform.position = new Vector3(otherPos.x, otherPos.y, this.transform.position.z);
            snappedTo = other.gameObject.transform;
            this.occupy(true);
        }
    }

    public void occupy(bool b){
        snappedTo.GetComponent<BoxCollider2D>().enabled = !b;
    }

    virtual public void play(){
        //To Be Overwritten by Children
    }
}
