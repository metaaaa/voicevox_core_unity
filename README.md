# voicevox_core_unity

## 概要
Unityでvoicevox_engineを介さずにvoicevox_coreを直接叩くやつです<br>
voicevox_core 0.14.3 で動作するように作成されています<br>
CPUモードでのみ動作を確認しています<br>

## install
### UPM
window -> PackageManager -> add package from git url
```
https://github.com/metaaaa/voicevox_core_unity.git?path=Assets/VoicevoxCoreUnity
```

### voicevox_core Download
https://github.com/VOICEVOX/voicevox_core#windows-%E3%81%AE%E5%A0%B4%E5%90%88

PowerShellを開いて Assets/Plugins/ に移動して
```PowerShell
Invoke-WebRequest https://github.com/VOICEVOX/voicevox_core/releases/latest/download/download-windows-x64.exe -OutFile ./download.exe
./download.exe -v 0.14.3
```

## Build
open_jtalk_dicとmodelをVoicevoxConfigで指定されているpathに配置すれば動く(あんまり検証してない)
![image](https://user-images.githubusercontent.com/56059182/232301774-fdd28e69-c05a-4586-9027-1b0065c716a5.png)
