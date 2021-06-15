using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Start()
    {
        _nextButton.gameObject.SetActive(false);
        //
        StartCoroutine(WaitForVideoEnd());
    }

    private IEnumerator WaitForVideoEnd()
    {
        yield return new WaitForSeconds(1f);

        var time = _videoPlayer.frameCount / _videoPlayer.frameRate;

        Debug.Log("Video time is: " + time);

        yield return new WaitForSeconds(time);

        EmotionHuntersController.instance.ToECT();

    }


}
