﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{

    [SerializeField] GameObject LetterO;
    [SerializeField] GameObject LetterM;
    [SerializeField] GameObject LetterG;
    [SerializeField] GameObject LettersNe;
    [SerializeField] GameObject LettersAn;
    [SerializeField] GameObject LettersAmes;
    [SerializeField] GameObject Logo;

    private CameraSizeController _cameraController;
    // Start is called before the first frame update
    void Start()
    {
        _cameraController = Camera.main.GetComponent<CameraSizeController>();
        StartMainLettersAnimation();
    }

    void StartMainLettersAnimation()
    {
        float speed = 1.5f;

        LeanTween.moveY(LetterO, 2.5f, speed).setEaseOutCirc();
        LeanTween.moveY(LetterM, 2.5f, speed).setEaseOutCirc().setDelay(0.3f);
        LeanTween.moveY(LetterG, 2.5f, speed).setEaseOutCirc().setDelay(0.6f).setOnComplete(ShowRestLetters);
    }

    void ShowRestLetters()
    {
        LeanTween.alpha(LettersNe, 1f, 1f).setEaseInCirc();
        LeanTween.alpha(LettersAn, 1f, 1f).setEaseInCirc();
        LeanTween.alpha(LettersAmes, 1f, 1f).setEaseInCirc().setOnComplete(ShowLogo);
    }

    void ShowLogo()
    {
        Vector2 scale = new Vector2(0.7f, 0.7f);
        LeanTween.alpha(Logo, 1f, 1f).setEaseInCirc();
        LeanTween.scale(Logo, scale, 1f).setEaseInCirc().setOnComplete(() => _cameraController.ShakeCamera());

        StartCoroutine(ShowGame());
    }

    IEnumerator ShowGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
