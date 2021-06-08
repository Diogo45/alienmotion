using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSelect : MonoBehaviour
{
    [SerializeField] private string _videoName;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, _videoName);
        _videoPlayer.Play();
    }


}
