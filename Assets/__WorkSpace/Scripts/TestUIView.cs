using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TestUIView : MonoBehaviour
{
    [SerializeField] private Button initializeBtn = null;
    [SerializeField] private Button finalizeBtn = null;
    [SerializeField] private Button versionBtn = null;
    [SerializeField] private Button metasBtn = null;
    [SerializeField] private Button deviceBtn = null;
    [SerializeField] private Button ttsBtn = null;
    [SerializeField] private Button audioQuerySynthesisBtn = null;
    [SerializeField] private Button testBtn = null;

    public IObservable<Unit> InitializeBtnObservable => initializeBtn.OnClickAsObservable();
    public IObservable<Unit> FinalizeBtnObservable => finalizeBtn.OnClickAsObservable();
    public IObservable<Unit> VersionBtnObservable => versionBtn.OnClickAsObservable();
    public IObservable<Unit> MetasBtnObservable => metasBtn.OnClickAsObservable();
    public IObservable<Unit> DeviceBtnObservable => deviceBtn.OnClickAsObservable();
    public IObservable<Unit> TtsBtnObservable => ttsBtn.OnClickAsObservable();
    public IObservable<Unit> AudioQuerySynthesisBtnObservable => audioQuerySynthesisBtn.OnClickAsObservable();
    public IObservable<Unit> TestBtnObservable => testBtn.OnClickAsObservable();

    void Start()
    {
    }
}