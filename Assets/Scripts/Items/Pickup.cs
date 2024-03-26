using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool _pickup = false;
    public GameObject Item;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)){
            _pickup = true;
        }else{
            _pickup = false;
        }
	}
    

    void OnTriggerStay2D(Collider2D other) 
    {
        if(_pickup && (other.gameObject.tag != transform.parent.gameObject.tag)){
            other.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
            other.transform.GetChild(0).transform.parent = transform.parent;
            transform.parent.GetChild(1).localPosition = new Vector3(0, 0, 0);
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.parent.GetChild(1).rotation = Quaternion.Slerp(transform.rotation, target, 1);

            transform.GetComponent<Collider2D>().isTrigger = false;
            transform.parent = other.gameObject.transform;
            transform.localPosition = new Vector3(2.62f, 0.66f, 0);
            target = Quaternion.Euler(0, 0, -35);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);

        }
           //Item.gameObject.SetActive (false);
	}

    // void GameObjectSwitch(Collider2D other)
    // {
    //     GameObject temp = other.gameObject;
    //     other.gameObject = Item.gameObject;
    //     Item.gameObject = temp;
    // }
}
