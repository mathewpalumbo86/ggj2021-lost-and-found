using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class UIController : MonoBehaviour
{

    public int sceneBuildIndex;

    public GameObject menu;

    bool menuToggle = false;

    public InputActionReference menuButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        menuButton.action.started += ToggleMenu; //equivalent to GetKeyDown()
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) //only do this in the build scene, not the main menu
        {
            if (menuToggle)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            } 
        }
    }       

    public void LoadMainScene()
    {
        Debug.Log("sceneBuildIndex to load: " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        menuToggle = !menuToggle;
    }

    public void Quit()
    {
        Debug.Log("quitting");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_ANDROID
        Application.Quit();
#endif

#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

}
