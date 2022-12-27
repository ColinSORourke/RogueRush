using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragger : MonoBehaviour
{
    public GameObject towerPrefab;

    public GameObject cardSelected = null;
    public GameObject objSelected = null;
    public Draggable objDrag;

    public float snapStrength = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            //check if object is clicked
            this.CheckHitObject();
        }
        if(Input.GetMouseButton(0) && objSelected != null){
            //drag an object
            if (!objDrag.snapped){
                this.DragObject();
            } else {
                Vector3 diffVector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 10.0F)) - objSelected.transform.position;
                if (diffVector.magnitude > snapStrength){
                    objDrag.snapped = false;
                    objDrag.occupy(false);
                }
            }
        }
        if(Input.GetMouseButtonUp(0) && objSelected != null){
            //drop an object
            this.DropObject();
        }
    }

    void CheckHitObject(){
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if(hit2d.collider != null){
            GameObject objectHit = hit2d.transform.gameObject;
            if (objectHit.GetComponent<HandTower>() != null){
                int canBePlayed = objectHit.GetComponent<HandTower>().canBePlayed();
                if (canBePlayed == 1){
                    cardSelected = objectHit;
                    Vector3 InstantiatePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 10.0F));
                    objSelected = Instantiate(towerPrefab, InstantiatePosition, Quaternion.identity);
                    var objData = objSelected.GetComponent<LiveTower>();
                    objDrag = objSelected.GetComponent<Draggable>();
                    objData.myTower = objectHit.GetComponent<HandTower>().myCard;
                    objData.dragMode = true;
                    objData.displayCard();
                    objData.displayRange(true);
                } else {
                    // Throw costs too much error
                }
            }
        }
    }

    void DragObject(){
        objSelected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 10.0F));
    }

    void DropObject(){
        
        if (!objDrag.snapped){
            GameObject.Destroy(objSelected);
        } else {
            cardSelected.GetComponent<HandTower>().play();
            objDrag.play();
            objSelected.transform.SetParent(this.transform.Find("ActiveTowers"));
            GameObject.Destroy(cardSelected);
        }
        objSelected = null;
        cardSelected = null;
    }
}
