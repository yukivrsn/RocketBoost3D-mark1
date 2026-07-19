using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float delayTime = 3f;
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log($"Friendly Obj: {collision.gameObject.name}");
                break;
            case "Fuel":
                Debug.Log($"Fuel Obj");
                break;
            case "Finish":
                Debug.Log("Level Complete");
                FinishSequence();
                break;

            default:
                Debug.Log("Explode");
                StartCrashSequence();
                break;
        } 
    }

    private void StartCrashSequence()
    {
      //  GetComponent<PlayerRocketMovement>().gameObject.SetActive( false );  | This disables the entire GameObject that PlayerRocketMovement is attached to.
        GetComponent<PlayerRocketMovement>().enabled = false;
        Invoke("SceneReload", delayTime);

    }
    private void FinishSequence()
    {
      //  GetComponent<PlayerRocketMovement>().gameObject.SetActive( false );  | This disables the entire GameObject that PlayerRocketMovement is attached to.
        GetComponent<PlayerRocketMovement>().enabled = false;
        Invoke("NextLevel", delayTime);



    }


     private void SceneReload()
    {
        //var currentScene = SceneManager.GetActiveScene(); // this stores string
        var currentScene = SceneManager.GetActiveScene().buildIndex; // this stores int
        SceneManager.LoadScene(currentScene);
        
    }

    private void NextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextLevel; 
        if (currentScene == SceneManager.sceneCountInBuildSettings - 1) {

            nextLevel = 0;
        }
        else {  nextLevel = currentScene + 1; }
            SceneManager.LoadScene(nextLevel);
    }

    
}
