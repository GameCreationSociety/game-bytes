using UnityEngine;
using UnityEngine.UI;

namespace Tempo
{
    public class GuitarManager : MonoBehaviour
    {
        public BeatMap beatMap;
        public GuitarNote guitarNotePrefab;
        public Fret[] frets;
        public SongManager songManager;
        public Text scoreText;
        public FeedbackRenderer feedbackRenderer;
        public Animator jumpingFrogs;
        public int currentFret;

        private int noteNum;
        private bool recentlyMoved;
        private bool recentlyPressed;
        private float lastBtnPress;

        private void processScore(int score)
        {
            if (score == -1)
                return;

            jumpingFrogs.Play("Jump");

            Score.player1Score += score;
            scoreText.text = (Mathf.Round(10000f * Score.player1Score / Score.player1MaxScore) / 100).ToString();
            if (score == 3)
            {
                feedbackRenderer.renderExcellent();
            }
            else if (score == 2)
            {
                feedbackRenderer.renderGood();
            }
            else if (score == 1)
            {
                feedbackRenderer.renderBad();
            }
            else if (score == 0)
            {
                feedbackRenderer.renderMiss();
                Score.player1Misses++;
            }
        }

        private void checkForNoteSpawn()
        {
            if (noteNum == beatMap.numNotesGuitar) return;

            if (beatMap.noteTimesGuitar[noteNum] - songManager.getCurrentSongTime() <= beatMap.approachTimeGuitar)
            {
                int location = beatMap.noteLocationsGuitar[noteNum];

                // spawn a note
                //GuitarNote guitarNote = Instantiate(guitarNotePrefab, frets[location].transform.position - new Vector3(0, 8.5f, 0), Quaternion.identity);
                GuitarNote guitarNote = ObjectPooler.Instance.SpawnFromPool("GuitarNote", frets[location].transform.position - new Vector3(0, 8.5f, 0), Quaternion.identity).GetComponent<GuitarNote>();
                // initialize note hit time and velocity
                guitarNote.initialize(beatMap.noteTimesGuitar[noteNum], beatMap.noteTypesGuitar[noteNum] == 1, beatMap.approachTimeGuitar);

                // send note to the corresponding fret
                frets[location].guitarNotes.Enqueue(guitarNote);

                // move onto the next note
                noteNum++;
            }
        }

        private void checkForInput()
        {
            if (Mathf.Abs(MinigameInputHelper.GetHorizontalAxis(1)) < 1)
            {
                recentlyMoved = false;
            }

            if (!recentlyMoved && MinigameInputHelper.GetHorizontalAxis(1) == 1)
            {
                frets[currentFret].deselectFret();
                currentFret = (currentFret + 1) % 4;
                frets[currentFret].selectFret();
                recentlyMoved = true;
            }

            else if (!recentlyMoved && MinigameInputHelper.GetHorizontalAxis(1) == -1)
            {
                frets[currentFret].deselectFret();
                currentFret = (currentFret + 3) % 4;
                frets[currentFret].selectFret();
                recentlyMoved = true;
            }

            if (recentlyPressed && (Time.time - lastBtnPress >= 0.03f))
            {
                // double press timer elapsed, single press is registered
                processScore(frets[currentFret].strumFret(false));
                recentlyPressed = false;
            }

            bool button1 = MinigameInputHelper.IsButton1Down(1);
            bool button2 = MinigameInputHelper.IsButton2Down(1);

            if (button1 || button2)
            {
                // both buttons pressed at exact same frame
                if (button1 && button2)
                {
                    processScore(frets[currentFret].strumFret(true));
                }

                // if no recent button press, set up double press timer
                else if (!recentlyPressed)
                {
                    recentlyPressed = true;
                    lastBtnPress = Time.time;
                }

                // there was recent button press, and another press came within double press timer
                else if (Time.time - lastBtnPress < 0.05f)
                {
                    processScore(frets[currentFret].strumFret(true));
                    recentlyPressed = false;
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            noteNum = 0;
            currentFret = 0;
            lastBtnPress = 0;
            for (int i = 0; i < frets.Length; i++)
            {
                frets[i].initialize(beatMap.excellentWindow, beatMap.goodWindow, beatMap.badWindow, songManager, feedbackRenderer);
            }
            frets[0].selectFret();
        }

        // Update is called once per frame
        void Update()
        {
            checkForNoteSpawn();
            checkForInput();
        }
    }
}
