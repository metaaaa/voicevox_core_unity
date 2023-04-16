using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace VoicevoxCoreUnity
{
    public static unsafe class VoicevoxUtils
    {
        public static string BasePath = "";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init()
        {
#if UNITY_EDITOR
            BasePath = Directory.GetCurrentDirectory();
#else
            BasePath = Application.dataPath ;
#endif
        }

        public static string GetOpenJTalkDict()
        {
            return BasePath + VoicevoxConfig.openJTalkDictPath;
        }

        public static string StringFromByteArray(byte* byteArray)
        {
            return Marshal.PtrToStringUTF8(new IntPtr(byteArray));
        }

        public static string GetWavFilePath()
        {
            string path = BasePath + VoicevoxConfig.wavSavePath + VoicevoxConfig.wavFileName;
            return path;
        }

        public static void InitializeFolder(string directoryPath)
        {
            string path = BasePath + directoryPath;

            if (!Directory.Exists(path))
            {
                var directoryPathSplit = directoryPath.Split('/');
                path = BasePath;
                foreach (var item in directoryPathSplit)
                {
                    path += "/" + item;
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }
            }
        }
    }
}