using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                NextLevel();
                break;

            default:
                Debug.Log("Explode");
                SceneReload();
                break;
        } 
    }
     private void SceneReload()
    {
        //var currentScene = SceneManager.GetActiveScene(); // this stores string
        var currentScene = SceneManager.GetActiveScene().buildIndex; // this stores int
        SceneManager.LoadScene(currentScene);
        
    }

    private void NextLevel()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        var nextLevel = currentScene + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings - 1) {

            nextLevel = 0;
        }

        SceneManager.LoadScene(nextLevel);
    }

    
}
