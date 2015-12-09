using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler current;
	public GameObject pooledObject;
	public int startingPooledAmount;
	public bool allowedToGrow;

	List<GameObject> pooledObjects;

	void Awake() {
		if (current == null) {
			DontDestroyOnLoad(gameObject);
			current = this;
		} else if (current != this) {
			Destroy(gameObject);
		}
	}

	void Start () {
		pooledObjects = new List<GameObject>();

		for(int i = 0; i < startingPooledAmount; i++) {
			GameObject obj = Instantiate(pooledObject);
			obj.transform.SetParent(gameObject.transform);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject getPooledObject() {
		for(int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeSelf) return pooledObjects[i];
		}

		if(allowedToGrow) {
			GameObject obj = Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}
}
