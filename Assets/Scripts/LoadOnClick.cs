using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class LoadOnClick : MonoBehaviour {

	public int levelToLoad;
	
	public void load(){
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;

		if(sceneIndex == 0) {
			SceneManager.LoadScene(1);//office
		} else if(levelToLoad > 0) {
			SceneManager.LoadScene(levelToLoad);
		} else {
			SceneManager.LoadScene(2); //Worldmap
		}
	}
}