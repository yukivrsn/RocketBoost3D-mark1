using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float delayTime = 3f;
    bool isControllable = true;

    [Header("Component References")]
    private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip sfxCrash;
    [SerializeField] private AudioClip sfxFinish;

    [Header("VFX")]
    [SerializeField] private ParticleSystem vfxCollision;
    [SerializeField] private ParticleSystem vfxFinsish;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextLevel();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame) { 
        
        isControllable = false;
        
        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // so -> it will run once and theat time it would be ocntroalbe so it would skip and use bool once bool is done now value changed so and won't happen
        if (!isControllable) // controlabele nahi haa
        {
            return;
        }

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
                StartFinishSequence();
                break;

            default:
                Debug.Log("Explode");
                StartCrashSequence();
                break;
        } 
    }

    private void StartCrashSequence()
    {
       // Debug.Log("Playing explosion");
        vfxCollision.Play();
        GetComponent<PlayerRocketMovement>().enabled = false;
        isControllable = false;
       audioSource.Stop();
        audioSource.PlayOneShot(sfxCrash);
        Invoke("SceneReload", delayTime);

  

        //  GetComponent<PlayerRocketMovement>().gameObject.SetActive( false );  | This disables the entire GameObject that PlayerRocketMovement is attached to.
    }
    private void StartFinishSequence()
    {
        isControllable = false ;
        vfxFinsish.Play();
        audioSource.PlayOneShot(sfxFinish);
        GetComponent<PlayerRocketMovement>().enabled = false;
        //  GetComponent<PlayerRocketMovement>().gameObject.SetActive( false );  | This disables the entire GameObject that PlayerRocketMovement is attached to.
  
        Invoke("NextLevel", delayTime);


        
    }



    // ----------- FXN CORE ------------ //
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
