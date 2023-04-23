namespace VoicevoxCoreUnity
{
    public static class VoicevoxConfig
    {
#if UNITY_EDITOR
        public static string wavSavePath = "/Assets/voicevox_wav/";
        public static string openJTalkDictPath = "/Assets/Plugins/voicevox_core/open_jtalk_dic_utf_8-1.11/";
#else
    public static string wavSavePath = "/voicevox_wav/";
    public static string openJTalkDictPath = "/open_jtalk_dic_utf_8-1.11/";
#endif
        public static string wavFileName = "output.wav";
    }
}
