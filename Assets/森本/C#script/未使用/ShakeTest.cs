using UnityEngine;

/// <summary>
/// 揺れテスト用
/// </summary>
public class ShakeTest : MonoBehaviour
{
    [SerializeField] private ShakeByRandom shakeByRandom;

    /// <summary>
    /// 短い揺れテスト
    /// </summary>
    public void PushButton1()
    {
        var duration = 1.0f;
        var strength = 30.0f;
        var vibrato = 30.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }

    /// <summary>
    /// 長い揺れテスト
    /// </summary>
    public void PushButton2()
    {
        var duration = 3.0f;
        var strength = 30.0f;
        var vibrato = 30.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }

    /// <summary>
    /// 早い振動テスト
    /// </summary>
    public void PushButton3()
    {
        var duration = 3.0f;
        var strength = 30.0f;
        var vibrato = 100.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }

    /// <summary>
    /// 強い揺れテスト
    /// </summary>
    public void PushButton4()
    {
        var duration = 3.0f;
        var strength = 100.0f;
        var vibrato = 30.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }
}
