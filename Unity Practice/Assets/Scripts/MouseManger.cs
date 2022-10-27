using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MouseManger : MonoBehaviour
{
    // Know what objects are clickable 
    public LayerMask clickableLayer;
    // Swap Cursor per Object
    public Texture2D pointer; // Normal Pointer
    public Texture2D target;  // Cursor for clickable objects
    public Texture2D doorway; // Cursor for Doorways
    public Texture2D combat; // Cursor for combat actions
                             // Start is called before the first frame update
                             //void Start()
                             //{

    //}
    public EventVector3 onClickEnviornment;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,50,clickableLayer.value))
        {
            bool door = false;
            bool warrior = false;
            bool item = false;
            if(hit.collider.gameObject.tag =="Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    Transform doorway = hit.collider.gameObject.transform;

                    onClickEnviornment.Invoke(doorway.position);
                    Debug.Log("Door is clicked");
                }
                else if(item)
                {
                    Transform itemPs = hit.collider.gameObject.transform;

                    onClickEnviornment.Invoke(itemPs.position);
                    Debug.Log("Item is clicked");
                }
                else
                {
                    onClickEnviornment.Invoke(hit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }