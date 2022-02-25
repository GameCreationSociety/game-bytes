using System.Collections.Generic;
using UnityEngine;

namespace Tempo
{
    public class Fret : MonoBehaviour
    {
        public float excellentWindow;
        public float goodWindow;
        public float badWindow;
        private SongManager songManager;
        private FeedbackRenderer feedbackRenderer;

        public Queue<GuitarNote> guitarNotes = new Queue<GuitarNote>();

        public void initialize(float excellentWindow, float goodWindow, float badWindow, SongManager songManager, FeedbackRenderer feedbackRenderer)
        {
            this.excellentWindow = excellentWindow;
            this.goodWindow = goodWindow;
            this.badWindow = badWindow;
            this.songManager = songManager;
            this.feedbackRenderer = feedbackRenderer;
        }

        private void checkForDespawnTiming()
        {
            if (guitarNotes.Count == 0)
                return;

            GuitarNote note = guitarNotes.Peek();
            if (songManager.getCurrentSongTime() - note.hitTime > badWindow)
            {
                guitarNotes.Dequeue();
                //Destroy(note.gameObject);
                note.gameObject.SetActive(false);
                feedbackRenderer.renderMiss();
                Score.player1Misses++;
            }
        }

        public void selectFret()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(200, 50, 50, 255);
        }

        public void deselectFret()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 184, 184, 255);
        }

        public int strumFret(bool chord)
        {
            if (guitarNotes.Count == 0) return -1;

            int score = 0;
            GuitarNote note = guitarNotes.Dequeue();
            if (Mathf.Abs(songManager.getCurrentSongTime() - note.hitTime) < excellentWindow) score = 3;
            else if (Mathf.Abs(songManager.getCurrentSongTime() - note.hitTime) < goodWindow) score = 2;
            else if (Mathf.Abs(songManager.getCurrentSongTime() - note.hitTime) < badWindow) score = 1;
            if (chord != note.isChord)
                score = 0;

            //Destroy(note.gameObject);
            note.gameObject.SetActive(false);
            return score;
        }

        // Update is called once per frame
        void Update()
        {
            checkForDespawnTiming();
        }
    }
}
