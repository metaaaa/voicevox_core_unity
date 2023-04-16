using System.Runtime.InteropServices;

namespace VoicevoxCoreUnity
{
    public class VoicevoxCoreConstants
    {
        public const int UTFByteArrayExcess = 16;
    }

    public unsafe struct VoicevoxInitializeOptions
    {
        public VoicevoxAccelerationMode acceleration_mode;
        public ushort cpu_num_threads;
        [MarshalAs(UnmanagedType.U1)] public bool load_all_models;
        public byte* open_jtalk_dict_dir;
    }

    public enum VoicevoxAccelerationMode : int
    {
        VOICEVOX_ACCELERATION_MODE_AUTO = 0,
        VOICEVOX_ACCELERATION_MODE_CPU = 1,
        VOICEVOX_ACCELERATION_MODE_GPU = 2,
    }

    public enum VoicevoxResultCode : int
    {
        VOICEVOX_RESULT_OK = 0,
        VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR = 1,
        VOICEVOX_RESULT_LOAD_MODEL_ERROR = 2,
        VOICEVOX_RESULT_GET_SUPPORTED_DEVICES_ERROR = 3,
        VOICEVOX_RESULT_GPU_SUPPORT_ERROR = 4,
        VOICEVOX_RESULT_LOAD_METAS_ERROR = 5,
        VOICEVOX_RESULT_UNINITIALIZED_STATUS_ERROR = 6,
        VOICEVOX_RESULT_INVALID_SPEAKER_ID_ERROR = 7,
        VOICEVOX_RESULT_INVALID_MODEL_INDEX_ERROR = 8,
        VOICEVOX_RESULT_INFERENCE_ERROR = 9,
        VOICEVOX_RESULT_EXTRACT_FULL_CONTEXT_LABEL_ERROR = 10,
        VOICEVOX_RESULT_INVALID_UTF8_INPUT_ERROR = 11,
        VOICEVOX_RESULT_PARSE_KANA_ERROR = 12,
        VOICEVOX_RESULT_INVALID_AUDIO_QUERY_ERROR = 13,
    }

    public struct VoicevoxAudioQueryOptions
    {
        [MarshalAs(UnmanagedType.U1)] public bool kana;
    }

    public struct VoicevoxSynthesisOptions
    {
        [MarshalAs(UnmanagedType.U1)] public bool enable_interrogative_upspeak;
    }

    public struct VoicevoxTtsOptions
    {
        [MarshalAs(UnmanagedType.U1)] public bool kana;
        [MarshalAs(UnmanagedType.U1)] public bool enable_interrogative_upspeak;
    }
}