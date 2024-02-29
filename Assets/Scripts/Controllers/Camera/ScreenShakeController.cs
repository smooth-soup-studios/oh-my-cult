using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour {
    public static ScreenShakeController Instance;

    private DateTime? _shakeStart;

    private ScreenShakeOptions _options = new();

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Shake");
            StartShake();
        }
        // END DEBUG

        if (!_shakeStart.HasValue) {
            ResetShake();
            return;
        }

        var elapsedMs = (DateTime.Now - _shakeStart.Value).TotalMilliseconds;

        if (elapsedMs > _options.Duration) {
            ResetShake();
            return;
        }

        var rad = elapsedMs / 1000 * _options.Rps * 360 * Mathf.Deg2Rad;
        var amp = (1 - elapsedMs / _options.Duration) * _options.Amplitude;

        var x = Math.Cos(rad) * amp;
        var y = Math.Sin(rad) * amp;

        transform.localPosition = new Vector3((float)x, (float)y, 0);
    }

    public void StartShake() {
        _shakeStart = DateTime.Now;
        _options.Duration = 500;
    }

    public void StartShake(int duration) {
        _shakeStart = DateTime.Now;
        _options.Duration = duration;
    }

    public void StartShake(int duration, int amplitude) {
        _shakeStart = DateTime.Now;
        _options.Duration = duration;
        _options.Amplitude = amplitude;
    }

    public void StartShake(ScreenShakeOptions options) {
        _shakeStart = DateTime.Now;
        _options = options;
    }

    public void ResetShake() {
        _shakeStart = null;
        transform.localPosition = Vector3.zero;
    }
}

public class ScreenShakeOptions {
    public ScreenShakeOptions() {
        Duration = 1000;
        Amplitude = .5;
        Rps = 15;
    }

    public int Duration { get; set; }
    public double Amplitude { get; set; }
    public double Rps { get; set; }
}