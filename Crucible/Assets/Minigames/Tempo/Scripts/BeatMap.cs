using UnityEngine;

namespace Tempo
{
    public class BeatMap : MonoBehaviour
    {
        public TextAsset beatMapFile;
        private BeatMapBlueprint beatMapBlueprint;
        public int BPM;
        public float songTime;
        public float approachTimeDrum;
        public float approachTimeGuitar;
        public int numNotesDrum;
        public int numNotesGuitar;
        public float excellentWindow;
        public float goodWindow;
        public float badWindow;
        public float[] noteTimesDrum;
        public float[] noteTimesGuitar;
        public int[] noteLocationsDrum;
        public int[] noteLocationsGuitar;
        public int[] noteTypesGuitar;

        private class BeatMapBlueprint
        {
            public int BPM;
            public float songTime;
            public float approachTimeDrum;
            public float approachTimeGuitar;
            public float excellentWindow;
            public float goodWindow;
            public float badWindow;
            public float guitarOffset;
            public float drumOffset;
            public float[] noteLengthsDrum;
            public int[] noteLocationsDrum;
            public float[] noteLengthsGuitar;
            public int[] noteLocationsGuitar;
            public int[] noteTypesGuitar;
        }

        private void initialize()
        {
            beatMapFile = Resources.Load<TextAsset>(LevelState.beatMapFilename);
            beatMapBlueprint = JsonUtility.FromJson<BeatMapBlueprint>(beatMapFile.text);
            BPM = beatMapBlueprint.BPM;
            songTime = beatMapBlueprint.songTime;
            approachTimeDrum = beatMapBlueprint.approachTimeDrum;
            approachTimeGuitar = beatMapBlueprint.approachTimeGuitar;
            numNotesDrum = beatMapBlueprint.noteLengthsDrum.Length;
            numNotesGuitar = beatMapBlueprint.noteLengthsGuitar.Length;
            excellentWindow = beatMapBlueprint.excellentWindow;
            goodWindow = beatMapBlueprint.goodWindow;
            badWindow = beatMapBlueprint.badWindow;
            noteLocationsDrum = beatMapBlueprint.noteLocationsDrum;
            noteLocationsGuitar = beatMapBlueprint.noteLocationsGuitar;
            noteTypesGuitar = beatMapBlueprint.noteTypesGuitar;

            // create noteTimesGuitar
            if (numNotesGuitar > 0)
            {
                noteTimesGuitar = new float[numNotesGuitar];
                noteTimesGuitar[0] = beatMapBlueprint.guitarOffset;
                float SPB = 60.0f / BPM;
                float runningTotal = noteTimesGuitar[0];
                for (int i = 1; i < numNotesGuitar; i++)
                {
                    runningTotal += SPB * beatMapBlueprint.noteLengthsGuitar[i - 1];
                    noteTimesGuitar[i] = runningTotal;
                }
            }

            // create noteTimesDrum
            if (numNotesDrum > 0)
            {
                noteTimesDrum = new float[numNotesDrum];
                noteTimesDrum[0] = beatMapBlueprint.drumOffset;
                float SPB = 60.0f / BPM;
                float runningTotal = noteTimesDrum[0];
                for (int i = 1; i < numNotesDrum; i++)
                {
                    runningTotal += SPB * beatMapBlueprint.noteLengthsDrum[i - 1];
                    noteTimesDrum[i] = runningTotal;
                }
            }
        }

        void Awake()
        {
            initialize();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
