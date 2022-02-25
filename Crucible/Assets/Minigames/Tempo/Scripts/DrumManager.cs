using UnityEngine;
using UnityEngine.UI;

namespace Tempo
{
    public class DrumManager : MonoBehaviour
    {
        public BeatMap beatMap;
        public DrumNote drumNotePrefab;
        public Drum[] drums;
        public SongManager songManager;
        public Text scoreText;
        public FeedbackRenderer feedbackRenderer;
        public FrogDrummer frogDrummer;
        public int currentDrum;

        private int noteNum;

        private void processScore(int score)
        {
            if (score == -1)
                return;

            Score.player2Score += score;
            scoreText.text = (Mathf.Round(10000f * Score.player2Score / Score.player2MaxScore) / 100).ToString();
            if (score == 3)
            {
                feedbackRenderer.renderExcellent();
            } else if (score == 2)
            {
                feedbackRenderer.renderGood();
            } else if (score == 1)
            {
                feedbackRenderer.renderBad();
            } else if (score == 0)
            {
                feedbackRenderer.renderMiss();
                Score.player2Misses++;
            }
        }

        private void checkForNoteSpawn()
        {
            if (noteNum == beatMap.numNotesDrum) return;

            if (beatMap.noteTimesDrum[noteNum] - songManager.getCurrentSongTime() <= beatMap.approachTimeDrum)
            {
                int location = beatMap.noteLocationsDrum[noteNum];

                // spawn a note
                //DrumNote drumNote = Instantiate(drumNotePrefab, drums[location].transform.position, Quaternion.identity);
                DrumNote drumNote = ObjectPooler.Instance.SpawnFromPool("DrumNote", drums[location].transform.position, Quaternion.identity).GetComponent<DrumNote>();

                // initialize note hit time and shrink rate
                drumNote.initialize(beatMap.noteTimesDrum[noteNum], beatMap.approachTimeDrum);

                // send note to the corresponding drum
                drums[location].drumNotes.Enqueue(drumNote);

                // move onto the next note
                noteNum++;
            }
        }

        private void checkForInput()
        {
            float vertical = MinigameInputHelper.GetVerticalAxis(2);
            float horizontal = MinigameInputHelper.GetHorizontalAxis(2);

            int switchTo = -1;
            if (vertical == 1f && horizontal == 0f)
                switchTo = 0;
            else if (vertical == 1f && horizontal == 1f)
                switchTo = 1;
            else if (vertical == 0f && horizontal == 1f)
                switchTo = 2;
            else if (vertical == -1f && horizontal == 1f)
                switchTo = 3;
            else if (vertical == -1f && horizontal == 0f)
                switchTo = 4;
            else if (vertical == -1f && horizontal == -1f)
                switchTo = 5;
            else if (vertical == 0f && horizontal == -1f)
                switchTo = 6;
            else if (vertical == 1f && horizontal == -1f)
                switchTo = 7;

            if (switchTo == -1)
            {
                if (currentDrum != -1)
                {
                    drums[currentDrum].deselectDrum();
                    frogDrummer.selectSprite(8);
                }
                currentDrum = -1;
            }
            else if (switchTo != currentDrum)
            {
                if (currentDrum != -1)
                    drums[currentDrum].deselectDrum();
                currentDrum = switchTo;
                drums[switchTo].selectDrum();
                frogDrummer.selectSprite(switchTo);
            }

            if (MinigameInputHelper.IsButton1Down(2) || MinigameInputHelper.IsButton2Down(2))
            {
                if (currentDrum != -1)
                    processScore(drums[currentDrum].hitDrum());
            }
        }


        void Start()
        {
            noteNum = 0;
            currentDrum = 0;
            for (int i = 0; i < drums.Length; i++)
            {
                drums[i].initialize(beatMap.excellentWindow, beatMap.goodWindow, beatMap.badWindow, songManager, feedbackRenderer);
            }
        }

    

        // Update is called once per frame
        void Update()
        {
            checkForNoteSpawn();
            checkForInput();
        }
    }
}
