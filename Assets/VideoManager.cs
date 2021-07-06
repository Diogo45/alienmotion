using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject _videoScreen;
    [SerializeField] private GameObject _expScreen;

    private bool isPlaying = false;
    private float time;

    private void Start()
    {
        
        //_nextButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_videoPlayer.isPrepared && !isPlaying)
        {
            _nextButton.gameObject.SetActive(false);
            //StartCoroutine(WaitForVideoEnd());

            time = _videoPlayer.frameCount / _videoPlayer.frameRate;

            isPlaying = true;
        }

        if (isPlaying)
        {
            time -= Time.unscaledDeltaTime;
            //Debug.LogWarning("VIDEO TIME: " + time);

            if(time <= 0)
            {
                EmotionHuntersController.instance.ToECT();

                gameObject.SetActive(false);

            }

        }

    }

    private IEnumerator WaitForVideoEnd()
    {
        

        Debug.Log("Video time is: " + time);

        yield return new WaitForSecondsRealtime(time);

      
    }

    public void ToVideo()
    {
        _nextButton.gameObject.SetActive(false);
        _expScreen.SetActive(false);
        _videoScreen.SetActive(true);

        StartCoroutine(WaitForVideoEnd());

    }


}
