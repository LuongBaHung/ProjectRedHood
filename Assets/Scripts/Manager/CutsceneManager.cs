using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public static CutsceneManager Instance;

    private void Awake()
    {
        Replay();
    }


    //public void PlayTimeline()
    //{
    //    if (playableDirector != null)
    //    {
    //        Debug.Log("PlayableDirector is ready to play");
    //        playableDirector.Stop();
    //        playableDirector.time = 0;
    //        playableDirector.RebuildGraph();
    //        playableDirector.Play();
    //        Debug.Log("Timeline is playing: " + playableDirector.state);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("PlayableDirector is not assigned!");
    //    }
    //}

    public void Replay()
    {
        if (playableDirector != null)
        {
            Debug.Log("PlayableDirector is ready to play");
            ResetTimeline();
            //playableDirector.Evaluate();
            playableDirector.Play();
            Debug.Log("Timeline is playing: " + playableDirector.state);
        }
    }

    public void ResetTimeline()
    {
        playableDirector.Stop();
        playableDirector.time = 0;
        playableDirector.RebuildGraph();
    }
}
