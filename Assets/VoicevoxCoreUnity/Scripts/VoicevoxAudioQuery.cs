using System.Collections.Generic;

namespace VoicevoxCoreUnity
{
    [System.Serializable]
    public class VoicevoxAudioQuery
    {
        public List<AccentPhrase> accent_phrases;
        public double speed_scale;
        public double pitch_scale;
        public double intonation_scale;
        public double volume_scale;
        public double pre_phoneme_length;
        public double post_phoneme_length;
        public int output_sampling_rate;
        public bool output_stereo;
        public string kana;
    }

    [System.Serializable]
    public class Mora
    {
        public string text;
        public string consonant;
        public double? consonant_length;
        public string vowel;
        public double vowel_length;
        public double pitch;
    }

    [System.Serializable]
    public class AccentPhrase
    {
        public List<Mora> moras;
        public int accent;
        public Mora pause_mora = null;
        public bool is_interrogative;
    }
}