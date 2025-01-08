using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // scene valtas play button nyomasra
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
