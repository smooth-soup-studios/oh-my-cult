using System;
using UnityEngine;

/// <summary>
/// Screen shake controller.
/// <para />
/// There is only one instance of this class in the game (as it is a singleton). This means that the latest GameObject to which this script is attached will be the only one to control the screen shake.
/// </summary>
public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController Instance { get; private set; };

    private float? _shakeStart;

    private ScreenShakeOptions _options = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_shakeStart.HasValue)
        {
            ResetShake();
            return;
        }

        var elapsedTime = Time.time - _shakeStart.Value;

        if (elapsedTime > _options.Duration)
        {
            ResetShake();
            return;
        }

        var rad = elapsedTime * _options.Rps * 360 * Mathf.Deg2Rad;
        var amp = (1 - elapsedTime / _options.Duration) * _options.Amplitude;

        var x = Math.Cos(rad) * amp;
        var y = Math.Sin(rad) * amp;

        transform.localPosition = new Vector3(x, y, 0);
    }

    /// <summary>
    /// Start screen shake with default options.
    /// <para/>Duration: .5f
    /// <para/>Amplitude: .5f
    /// <para/>Rps: 15f
    /// </summary>
    public void StartShake()
    {
        _shakeStart = Time.time;
        _options = _defaultScreenShakeOptions;
    }

    /// <summary>
    /// Start screen shake with default amplitude and rps.
    /// <para/>Amplitude: .5f
    /// <para/>Rps: 15f
    /// </summary>
    public void StartShake(float duration)
    {
        _shakeStart = Time.time;
        _options = new ScreenShakeOptions()
        {
            Duration = duration
        };
    }

    /// <summary>
    /// Start screen shake with default rps.
    /// <para/>Rps: 15f
    /// </summary>
    public void StartShake(float duration, float amplitude)
    {
        _shakeStart = Time.time;
        _options = new ScreenShakeOptions()
        {
            Duration = duration,
            Amplitude = amplitude
        };
    }

    /// <summary>
    /// Start screen shake with custom options.
    /// </summary>
    public void StartShake(ScreenShakeOptions options)
    {
        _shakeStart = Time.time;
        _options = options;
    }

    /// <summary>
    /// Reset & stop screen shake.
    /// </summary>
    public void ResetShake()
    {
        _shakeStart = null;
        transform.localPosition = Vector3.zero;
    }

    private readonly ScreenShakeOptions _defaultScreenShakeOptions = new();
}

public class ScreenShakeOptions
{
    public ScreenShakeOptions()
    {
        Duration = .5f;
        Amplitude = .5f;
        Rps = 15f;
    }

    /// <summary>
    /// Duration in seconds
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    /// Shake amplitude radius in camera screen space
    /// </summary>
    public float Amplitude { get; set; }

    /// <summary>
    /// Rps: full rotations per second
    /// </summary>
    public float Rps { get; set; }
}