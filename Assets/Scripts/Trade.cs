using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Trade : MonoBehaviour {

	public int identityNumber;
	Merchant merchant;
	SphereCollider sphereCollider;
	bool isHighlighted = false;
	
	/*Relationship with a trading source, things to save:
		How many times they have been stolen from
		How many times they have been short changed
		*/

	// Use this for initialization
	void Start () {
		//pulls a random 
		merchant = Persistant.persist.merchants[identityNumber];
		sphereCollider = GetComponent<SphereCollider>();
	}

	void OnTriggerEnter(Collider other) {
		isHighlighted = true;
	}

	void OnTriggerExit(Collider other) {
		isHighlighted = false;
		merchant.inven.endTrade();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0) && isHighlighted && Inventory.getState != State.TRADING){
			merchant.beginTrade();
		}
	}
}
