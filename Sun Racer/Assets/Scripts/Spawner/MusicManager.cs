using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentMusicBar = 0;
        public int currentBeat = 0;
        public float tempo = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    public TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    [FMODUnity.EventRef] public string musicPath;
    FMOD.Studio.EventInstance musicEvent;

    FMOD.Studio.EVENT_CALLBACK beatCallback;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        timelineInfo = new TimelineInfo();
        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(musicPath);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        musicEvent.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicEvent.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicEvent.start();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        musicEvent.setUserData(IntPtr.Zero);
        musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicEvent.release();
        timelineHandle.Free();
    }

    void OnGUI()
    {
        GUILayout.Box(string.Format("tempo = {0}, Beat = {1}", timelineInfo.tempo, timelineInfo.currentBeat));
    }

    public FMOD.Studio.EventInstance getMusicEvent()
    {
        return musicEvent;
    }

    public void pauseMusic()
    {
        if (gm.getPaused())
        {
            musicEvent.setPaused(true);
            //GetComponent<AudioSource>().Pause();
        }
        else
        {
            musicEvent.setPaused(false);
           // GetComponent<AudioSource>().UnPause();
        }
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                        timelineInfo.tempo = parameter.tempo;
                        timelineInfo.currentMusicBar = parameter.bar;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}
