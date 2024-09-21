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
        ShowBlackScreen.OnBlackout += LoadScene;
    }

    void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void OnDestroy()
    {
        EnterCar.OnEnterCar -= LoadScene;
        ShowBlackScreen.OnBlackout -= LoadScene;
    }
}
