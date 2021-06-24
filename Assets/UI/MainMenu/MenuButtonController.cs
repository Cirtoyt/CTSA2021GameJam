using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public enum onCLick {None,Transition,CloseGame,OpenScene};
    public onCLick clickResult;

    public string SceneName;
    public GameObject CurrentScreen;
    public GameObject NextScreen;

    private void Start()
    {
        if(clickResult == onCLick.Transition)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(TransitionPress);
        }
        else if (clickResult == onCLick.CloseGame)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(CloseGamePress);
        }
        else if (clickResult == onCLick.OpenScene)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(OpenScenePress);
        }
    }
    public void TransitionPress()
    {
        CurrentScreen.GetComponent<Canvas>().sortingOrder = -1;
        NextScreen.GetComponent<Canvas>().sortingOrder = 0;
    }
    public void CloseGamePress()
    {
        Application.Quit();
    }
    public void OpenScenePress()
    {
        SceneManager.LoadScene(SceneName);
    }
}
