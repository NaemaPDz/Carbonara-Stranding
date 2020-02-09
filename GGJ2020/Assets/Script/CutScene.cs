using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    public RawImage image;
    public VideoClip videoToPlay;
    public string after_end;
    public bool can_skip;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && can_skip)
        {
            videoPlayer.Stop();
            Destroy(this.gameObject);
            GoToScene();
        }
    }

    IEnumerator playVideo()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.VideoClip;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        image.texture = videoPlayer.texture;

        videoPlayer.Play();

        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
        GoToScene();

    }

    void GoToScene()
    {
        SceneManager.LoadScene(after_end);
    }

}
