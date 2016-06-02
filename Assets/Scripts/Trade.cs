using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Trade : MonoBehaviour {

	Merchant merchant;
	SphereCollider sphereCollider;
	bool isHighlighted = false;

	/*Relationship with a trading source, things to save:
		How many times they have been stolen from
		How many times they have been short changed
		*/

	// Use this for initialization
	void OnLevelWasLoaded(int level) {
		//pulls a random 
		//IMPORTANT!!-----------------all the merchants must be blocked together in the scene manager
		merchant = Persistant.persist.merchants[level - 4];
		merchant.changeOutSecrets();
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
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
