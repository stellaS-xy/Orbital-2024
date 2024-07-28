using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("No Button component found on this GameObject.");
        }
    }

    private void OnButtonClick()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("sceneToLoad is not assigned.");
            return;
        }

        if (SceneTransitionManager.Instance == null)
        {
            Debug.LogError("SceneTransitionManager instance is not found.");
            return;
        }

        SceneTransitionManager.Instance.TransitionToScene(sceneToLoad);
    }
}
