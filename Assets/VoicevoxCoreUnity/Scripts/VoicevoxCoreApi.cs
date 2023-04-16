using System.Runtime.InteropServices;

namespace VoicevoxCoreUnity
{
    public static unsafe class VoicevoxCoreApi 
    {
        const string __DllName = "voicevox_core";

        [DllImport(__DllName, EntryPoint = "voicevox_make_default_initialize_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxInitializeOptions voicevox_make_default_initialize_options();

        [DllImport(__DllName, EntryPoint = "voicevox_initialize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_initialize(VoicevoxInitializeOptions options);

        [DllImport(__DllName, EntryPoint = "voicevox_get_version", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_get_version();

        [DllImport(__DllName, EntryPoint = "voicevox_load_model", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_load_model(uint speaker_id);

        [DllImport(__DllName, EntryPoint = "voicevox_is_gpu_mode", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool voicevox_is_gpu_mode();

        [DllImport(__DllName, EntryPoint = "voicevox_is_model_loaded", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool voicevox_is_model_loaded(uint speaker_id);

        [DllImport(__DllName, EntryPoint = "voicevox_finalize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_finalize();

        [DllImport(__DllName, EntryPoint = "voicevox_get_metas_json", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_get_metas_json();

        [DllImport(__DllName, EntryPoint = "voicevox_get_supported_devices_json", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_get_supported_devices_json();

        [DllImport(__DllName, EntryPoint = "voicevox_predict_duration", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_predict_duration(nuint length, long* phoneme_vector, uint speaker_id, nuint* output_predict_duration_data_length, float** output_predict_duration_data);

        [DllImport(__DllName, EntryPoint = "voicevox_predict_duration_data_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_predict_duration_data_free(float* predict_duration_data);

        [DllImport(__DllName, EntryPoint = "voicevox_predict_intonation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_predict_intonation(nuint length, long* vowel_phoneme_vector, long* consonant_phoneme_vector, long* start_accent_vector, long* end_accent_vector, long* start_accent_phrase_vector, long* end_accent_phrase_vector, uint speaker_id, nuint* output_predict_intonation_data_length, float** output_predict_intonation_data);

        [DllImport(__DllName, EntryPoint = "voicevox_predict_intonation_data_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_predict_intonation_data_free(float* predict_intonation_data);

        [DllImport(__DllName, EntryPoint = "voicevox_decode", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_decode(nuint length, nuint phoneme_size, float* f0, float* phoneme_vector, uint speaker_id, nuint* output_decode_data_length, float** output_decode_data);

        [DllImport(__DllName, EntryPoint = "voicevox_decode_data_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_decode_data_free(float* decode_data);

        [DllImport(__DllName, EntryPoint = "voicevox_make_default_audio_query_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxAudioQueryOptions voicevox_make_default_audio_query_options();

        [DllImport(__DllName, EntryPoint = "voicevox_audio_query", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_audio_query(byte* text, uint speaker_id, VoicevoxAudioQueryOptions options, byte** output_audio_query_json);

        [DllImport(__DllName, EntryPoint = "voicevox_make_default_synthesis_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxSynthesisOptions voicevox_make_default_synthesis_options();

        [DllImport(__DllName, EntryPoint = "voicevox_synthesis", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_synthesis(byte* audio_query_json, uint speaker_id, VoicevoxSynthesisOptions options, nuint* output_wav_length, byte** output_wav);

        [DllImport(__DllName, EntryPoint = "voicevox_make_default_tts_options", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxTtsOptions voicevox_make_default_tts_options();

        [DllImport(__DllName, EntryPoint = "voicevox_tts", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern VoicevoxResultCode voicevox_tts(byte* text, uint speaker_id, VoicevoxTtsOptions options, nuint* output_wav_length, byte** output_wav);

        [DllImport(__DllName, EntryPoint = "voicevox_audio_query_json_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_audio_query_json_free(byte* audio_query_json);

        [DllImport(__DllName, EntryPoint = "voicevox_wav_free", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void voicevox_wav_free(byte* wav);

        [DllImport(__DllName, EntryPoint = "voicevox_error_result_to_message", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte* voicevox_error_result_to_message(VoicevoxResultCode result_code);
    }
}
