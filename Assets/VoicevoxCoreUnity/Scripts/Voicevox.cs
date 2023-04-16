using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace VoicevoxCoreUnity
{
    public static unsafe class Voicevox
    {
        public static VoicevoxResultCode Initialize(VoicevoxInitializeOptions options)
        {
            string dict = VoicevoxUtils.GetOpenJTalkDict();
            if (!Directory.Exists(dict))
            {
                Debug.LogError("GetOpenJTalkDict not found : " + dict);
                return VoicevoxResultCode.VOICEVOX_RESULT_UNINITIALIZED_STATUS_ERROR;
            }

            byte[] dictBytes = Encoding.UTF8.GetBytes(dict);
            fixed (byte* pDictByte = dictBytes)
            {
                options.open_jtalk_dict_dir = pDictByte;
                var result = VoicevoxCoreApi.voicevox_initialize(options);
                
                return result;
            }
        }

        public static VoicevoxInitializeOptions MakeDefaultInitializeOptions()
        {
            return VoicevoxCoreApi.voicevox_make_default_initialize_options();
        }

        public static string GetVersion()
        {
            return VoicevoxUtils.StringFromByteArray(VoicevoxCoreApi.voicevox_get_version());
        }

        public static VoicevoxResultCode LoadModel(uint speakerId)
        {
            var result = VoicevoxCoreApi.voicevox_load_model(speakerId);
            return result;
        }

        public static bool IsGpuMode()
        {
            return VoicevoxCoreApi.voicevox_is_gpu_mode();
        }

        public static bool IsModelLoaded(uint speakerId)
        {
            return VoicevoxCoreApi.voicevox_is_model_loaded(speakerId);
        }

        public static void VoicevoxFinalize()
        {
            VoicevoxCoreApi.voicevox_finalize();
        }

        public static List<VoicevoxSpeaker> GetMetasJson()
        {
            var jsonString = VoicevoxUtils.StringFromByteArray(VoicevoxCoreApi.voicevox_get_metas_json());
            var speakers = JsonConvert.DeserializeObject<List<VoicevoxSpeaker>>(jsonString);
            return speakers;
        }

        public static string GetSupportedDevicesJson()
        {
            return VoicevoxUtils.StringFromByteArray(VoicevoxCoreApi.voicevox_get_supported_devices_json());
        }

        //Todo
        // public static string voicevox_predict_duration()
        // {
        //     VoiceVoxCoreApi.voicevox_predict_duration();
        //     return "Todo";
        // }

        //Todo
        // public static string voicevox_predict_duration_data_free()
        // {
        //     VoiceVoxCoreApi.voicevox_predict_duration_data_free();
        //     return "Todo";
        // }

        //Todo
        // public static string voicevox_predict_intonation()
        // {
        //     VoiceVoxCoreApi.voicevox_predict_intonation();
        //     return "Todo";
        // }

        //Todo
        // public static string voicevox_predict_intonation_data_free()
        // {
        //     VoiceVoxCoreApi.voicevox_predict_intonation_data_free();
        //     return "Todo";
        // }

        //Todo
        // public static string voicevox_decode()
        // {
        //     VoiceVoxCoreApi.voicevox_decode();
        //     return "Todo";
        // }

        //Todo
        // public static string voicevox_decode_data_free()
        // {
        //     VoiceVoxCoreApi.voicevox_decode_data_free();
        //     return "Todo";
        // }

        public static VoicevoxAudioQueryOptions MakeDefaultAudioQueryOptions()
        {
            return VoicevoxCoreApi.voicevox_make_default_audio_query_options();
        }

        public static (VoicevoxResultCode resultCode, VoicevoxAudioQuery audioQuery) AudioQuery(string text,
            uint speakerId, VoicevoxAudioQueryOptions options)
        {
            if (!IsModelLoaded(speakerId))
            {
                LoadModel(speakerId);
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            // Todo
            //fixed(byte* pTextByte = textBytes) だとメモリ配置によって文字列が変化するのでStackに余分にメモリ確保して格納
            fixed (byte* stackTextBytesPtr =
                       stackalloc byte[textBytes.Length + VoicevoxCoreConstants.UTFByteArrayExcess])
            {
                for (int i = 0; i < textBytes.Length; i++)
                {
                    stackTextBytesPtr[i] = textBytes[i];
                }

                byte* outputJsonPtr;

                var result = VoicevoxCoreApi.voicevox_audio_query(
                    stackTextBytesPtr,
                    speakerId,
                    options,
                    &outputJsonPtr
                );

                var audioQueryJson = VoicevoxUtils.StringFromByteArray(outputJsonPtr);

                AudioQueryJsonFree(outputJsonPtr);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };


                return (result, JsonConvert.DeserializeObject<VoicevoxAudioQuery>(audioQueryJson, settings));
            }
        }

        public static VoicevoxSynthesisOptions MakeDefaultSynthesisOptions()
        {
            return VoicevoxCoreApi.voicevox_make_default_synthesis_options();
        }


        public static VoicevoxResultCode Synthesis(VoicevoxAudioQuery voicevoxAudioQuery, uint speakerId,
            VoicevoxSynthesisOptions option)
        {
            if (!IsModelLoaded(speakerId))
            {
                LoadModel(speakerId);
            }

            var aqJson = JsonConvert.SerializeObject(voicevoxAudioQuery);
            byte[] textBytes = Encoding.UTF8.GetBytes(aqJson);

            // Todo
            //fixed(byte* pTextByte = textBytes) だとメモリ配置によって文字列が変化するのでStackに余分にメモリ確保して格納
            fixed (byte* stackTextBytesPtr =
                       stackalloc byte[textBytes.Length + VoicevoxCoreConstants.UTFByteArrayExcess])
            {
                for (int i = 0; i < textBytes.Length; i++)
                {
                    stackTextBytesPtr[i] = textBytes[i];
                }

                var path = VoicevoxUtils.BasePath + VoicevoxConfig.wavSavePath;
                nuint outputWavLength = 0;
                byte* outputWavPtr;

                VoicevoxResultCode result = VoicevoxCoreApi.voicevox_synthesis(
                    // pTextByte,
                    stackTextBytesPtr,
                    speakerId,
                    option,
                    &outputWavLength,
                    &outputWavPtr
                );
                
                byte[] outputWav = new byte[outputWavLength];
                Marshal.Copy((IntPtr)outputWavPtr, outputWav, 0, (int)outputWavLength);


                if (!Directory.Exists(path))
                {
                    VoicevoxUtils.InitializeFolder(VoicevoxConfig.wavSavePath);
                }

                path += VoicevoxConfig.wavFileName;

                File.WriteAllBytes(path, outputWav);
                VoicevoxCoreApi.voicevox_wav_free(outputWavPtr);

                return result;
            }
        }

        public static VoicevoxTtsOptions MakeDefaultTtsOptions()
        {
            return VoicevoxCoreApi.voicevox_make_default_tts_options();
        }

        public static VoicevoxResultCode TextToSpeech(string text, uint speakerId, VoicevoxTtsOptions options)
        {
            if (!IsModelLoaded(speakerId))
            {
                LoadModel(speakerId);
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            var path = VoicevoxUtils.BasePath + VoicevoxConfig.wavSavePath;

            // Todo
            //fixed(byte* pTextByte = textBytes) だとメモリ配置によって文字列が変化するのでStackに余分にメモリ確保して格納
            fixed (byte* stackTextBytesPtr =
                       stackalloc byte[textBytes.Length + VoicevoxCoreConstants.UTFByteArrayExcess])
            {
                for (int i = 0; i < textBytes.Length; i++)
                {
                    stackTextBytesPtr[i] = textBytes[i];
                }

                nuint outputWavLength = 0;
                byte* outputWavPtr;

                VoicevoxResultCode result = VoicevoxCoreApi.voicevox_tts(
                    stackTextBytesPtr,
                    speakerId,
                    options,
                    &outputWavLength,
                    &outputWavPtr
                );
                
                byte[] outputWav = new byte[outputWavLength];
                Marshal.Copy((IntPtr)outputWavPtr, outputWav, 0, (int)outputWavLength);


                if (!Directory.Exists(path))
                {
                    VoicevoxUtils.InitializeFolder(VoicevoxConfig.wavSavePath);
                }

                path += VoicevoxConfig.wavFileName;

                File.WriteAllBytes(path, outputWav);
                VoicevoxCoreApi.voicevox_wav_free(outputWavPtr);
                return result;
            }
        }
        
        public static void AudioQueryJsonFree(byte* audioQueryJson)
        {
            VoicevoxCoreApi.voicevox_audio_query_json_free(audioQueryJson);
        }

        // Todo
        public static void WavFree(byte* wav)
        {
            VoicevoxCoreApi.voicevox_wav_free(wav);
        }
        
    }
}