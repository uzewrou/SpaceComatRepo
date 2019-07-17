using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] GameObject deathFX;
	[SerializeField] float levelLoadDelay = 3f;


	void Start(){
 		 deathFX.SetActive(false);
	}
	private void OnTriggerEnter(Collider other){
		StartDeathSequence();
    }



    private void StartDeathSequence()
    {
        deathFX.SetActive(true);
        SendMessage("OnPlayerDeath");
        Invoke("ReloadScene", levelLoadDelay); //string referenced
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(1);
	}
}