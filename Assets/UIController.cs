using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public int sceneBuildIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }       

    public void LoadMainScene()
    {
        Debug.Log("sceneBuildIndex to load: " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex);
    }


    public void Quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }

}
