using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnterCar.OnEnterCar += LoadScene;
    }

    void LoadScene()
    {
        SceneManager.LoadScene(2);
    }

    private void OnDestroy()
    {
        EnterCar.OnEnterCar -= LoadScene;
    }
}
