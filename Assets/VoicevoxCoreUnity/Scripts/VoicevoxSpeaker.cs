using System.Collections.Generic;

namespace VoicevoxCoreUnity
{
    [System.Serializable]
    public class VoicevoxSpeaker
    {
        public string name;
        public List<SpeakerStyle> styles;
        public string speaker_uuid;
        public string version;
    }

    [System.Serializable]
    public class SpeakerStyle
    {
        public string name;
        public int id;
    }
}