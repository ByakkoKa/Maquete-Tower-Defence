using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OpenScene : MonoBehaviour
{

    public void OpenSceneMap()
    {
        SceneManager.LoadScene("BaseMap");
    }

}
