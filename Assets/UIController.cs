using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject redCube;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedCubeToggle()
    {
        if (redCube.activeInHierarchy)
        {
            Debug.Log("is active turn it off");
            redCube.SetActive(false);
        }
    }

}
