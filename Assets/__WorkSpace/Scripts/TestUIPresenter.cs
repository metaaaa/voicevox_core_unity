using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TestUIPresenter : MonoBehaviour
{
    [SerializeField] private VoiceVoxTest voicevox = null;

    // Start is called before the first frame update
    void Start()
    {
        var view = GetComponent<TestUIView>();

        view.InitializeBtnObservable.Subscribe(_ => voicevox.Initialize()).AddTo(this);
        view.FinalizeBtnObservable.Subscribe(_ => voicevox.VoicevoxFinalize()).AddTo(this);
        view.VersionBtnObservable.Subscribe(_ => voicevox.GetVersion()).AddTo(this);
        view.MetasBtnObservable.Subscribe(_ => voicevox.GetMetas()).AddTo(this);
        view.DeviceBtnObservable.Subscribe(_ => voicevox.GetSupportedDevicesJson()).AddTo(this);
        view.TtsBtnObservable.Subscribe(_ => voicevox.TextToSpeech()).AddTo(this);
        view.AudioQuerySynthesisBtnObservable.Subscribe(_ => voicevox.AudioQuerySynthesis()).AddTo(this);
        view.TestBtnObservable.Subscribe(_ => voicevox.Test()).AddTo(this);
    }
}