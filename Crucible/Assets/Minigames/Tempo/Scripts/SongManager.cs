using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tempo
{
    public class SongManager : MonoBehaviour
    {
        public BeatMap beatMap;
        public Text guitarScore;
        public Text drumScore;
        private float startTime;
        private float endTime;
        private float initialPause;
        private float calibration;
        private AudioSource song;

        public float getCurrentSongTime()
        {
            return (float)AudioSettings.dspTime - startTime;
        }

        private void endScene()
        {
            SceneManager.LoadScene("Results_Scene");
        }

        // Start is called before the first frame update
        void Start()
        {
            initialPause = 3f;
            startTime = (float)AudioSettings.dspTime + initialPause;
            endTime = beatMap.songTime;
            //calibration = -0.27f; // higher value means later song play back (i.e. notes appear "earlier")
            calibration = SettingsManager.calibra;
            song = gameObject.GetComponent<AudioSource>();
            song.volume = SettingsManager.volume / 100.0f;
            song.clip = Resources.Load<AudioClip>(LevelState.songFilename);
            song.PlayScheduled(startTime + calibration);

            Score.player1Score = 0;
            Score.player2Score = 0;
            Score.player1Misses = 0;
            Score.player2Misses = 0;
            Score.player1MaxScore = beatMap.numNotesGuitar * 3;
            Score.player2MaxScore = beatMap.numNotesDrum * 3;
        }

        // Update is called once per frame
        void Update()
        {
            if (getCurrentSongTime() > endTime)
            {
                endScene();
            }
        }
    }
}
