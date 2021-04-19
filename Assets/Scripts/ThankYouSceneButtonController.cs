using UnityEngine;

public class ThankYouSceneButtonController : MonoBehaviour
{
    private SoundController _soundController;
    private void Start()
    {
        //_soundController = GameObject.FindWithTag("SoundController").GetComponent<SoundController>();
    }
    public void GoToHomeScene()
    {
        //_soundController.StopBackgroundMusic();
        ScenesController.ShowHomeLevel();
    }
}
