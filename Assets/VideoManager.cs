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

    private void Start()
    {
           
        
    }

    private IEnumerator WaitForVideoEnd()
    {
        yield return new WaitUntil(() =>_videoPlayer.isPrepared);

        var time = _videoPlayer.frameCount / _videoPlayer.frameRate;

        Debug.Log("Video time is: " + time);

        yield return new WaitForSeconds(time - 5f);

        EmotionHuntersController.instance.ToECT();

        gameObject.SetActive(false);

    }

    public void ToVideo()
    {
        _nextButton.gameObject.SetActive(false);
        _expScreen.SetActive(false);
        _videoScreen.SetActive(true);

        StartCoroutine(WaitForVideoEnd());

    }


}
