using System;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEditor;
using VoicevoxCoreUnity;

public class VoiceVoxTest : MonoBehaviour
{
      public string text = "ほげほげ";
      public VoicevoxAccelerationMode mode = VoicevoxAccelerationMode.VOICEVOX_ACCELERATION_MODE_AUTO;
      public uint speakerId = 0;
      public AudioSource audioSource = null;


      [ContextMenu("Initialize")]
      public void Initialize()
      {
         VoicevoxUtils.Init();
         var option = Voicevox.MakeDefaultInitializeOptions();
         option.acceleration_mode = mode;
         var result = Voicevox.Initialize(option);
         Debug.Log(("Initialize", result.ToString()));
      }

      [ContextMenu("Finalize")]
      public void VoicevoxFinalize()
      {
         Voicevox.VoicevoxFinalize();
      }

      [ContextMenu("Version")]
      public void GetVersion()
      {
         Debug.Log(Voicevox.GetVersion());
      }

      [ContextMenu("Metas")]
      public void GetMetas()
      {
         Debug.Log(Voicevox.GetMetasJson());
      }
      
      [ContextMenu("device")]
      public void GetSupportedDevicesJson()
      {
         Debug.Log(Voicevox.GetSupportedDevicesJson());
      }

      [ContextMenu("TextToSpeech")]
      public async void TextToSpeech()
      {
         var option = Voicevox.MakeDefaultTtsOptions();
         await UniTask.SwitchToTaskPool();
         var result = Voicevox.TextToSpeech(text, speakerId, option);
         Debug.Log(("Tts", result.ToString()));
         await UniTask.Yield();
#if UNITY_EDITOR
         if (EditorApplication.isPlaying)
         {
            StartCoroutine("StreamPlayAudioFile");
         }
         else
         {
            AssetDatabase.Refresh();
         }
#else
         StartCoroutine("StreamPlayAudioFile");
#endif
      }

      [ContextMenu("AudioQuerySynthesis")]
      public void AudioQuerySynthesis()
      {
         var option = Voicevox.MakeDefaultAudioQueryOptions();
         var audioQueryResult = Voicevox.AudioQuery(text, speakerId, option);
         Debug.Log(("AudioQuery", audioQueryResult.resultCode.ToString()));
         
         var synthesisOptions = Voicevox.MakeDefaultSynthesisOptions();
         var result = Voicevox.Synthesis(audioQueryResult.audioQuery, speakerId, synthesisOptions);
         Debug.Log(("Synthesis", result.ToString()));
         
#if UNITY_EDITOR
         if (EditorApplication.isPlaying)
         {
            StartCoroutine("StreamPlayAudioFile");
         }
         else
         {
            AssetDatabase.Refresh();
         }
#else
         StartCoroutine("StreamPlayAudioFile");
#endif
      }

      [ContextMenu("Test")]
      public void Test()
      {
         Debug.Log(VoicevoxUtils.GetOpenJTalkDict());
      }

      IEnumerator StreamPlayAudioFile()
      {
         var path = VoicevoxUtils.GetWavFilePath();

         using (WWW www = new WWW("file:///" + path))
         {
               //読み込み完了まで待機
               yield return www;

               audioSource.clip = www.GetAudioClip(true, true);

               audioSource.Play();
         }
      }
}