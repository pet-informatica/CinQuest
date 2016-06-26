using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Minimap : MonoBehaviour {

    Text title;

	void Start () {
        title = GetComponentInChildren<Text>();
        title.text = SceneNamer.UI(SceneManager.GetActiveScene().name);
    }

    void OnLevelWasLoaded(int level)
    {
        if (SceneNamer.UI(SceneManager.GetActiveScene().name) != null)
            if(title != null)
                title.text = SceneNamer.UI(SceneManager.GetActiveScene().name);
    }
}
