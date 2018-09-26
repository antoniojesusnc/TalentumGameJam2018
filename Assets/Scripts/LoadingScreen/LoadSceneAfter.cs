using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadSceneAfter : MonoBehaviour
{

    [SerializeField]
    float _time;

    [SerializeField]
    Image _image;

    [SerializeField]
    CanvasGroup _gameTitle;

    void Start()
    {
        _image.GetComponent<CanvasGroup>().alpha = 1;
        _gameTitle.alpha = 0;

        var sequence = LeanTween.sequence();
        sequence.append(
            LeanTween.alphaCanvas(_image.GetComponent<CanvasGroup>(), 0, _time)
        );

        sequence.append(
            LeanTween.alphaCanvas(_gameTitle, 1, _time)
        );

        Invoke("LoadScene", _time*2);
    }

    void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
