using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ランダム値を使用した揺れ
/// </summary>
public class ShakeByRandom : MonoBehaviour
{
    /// <summary>
    /// 揺れ情報
    /// </summary>
    private struct ShakeInfo
    {
        public ShakeInfo(float duration, float strength, float vibrato)
        {
            Duration = duration;
            Strength = strength;
            Vibrato = vibrato;
        }
        public float Duration { get; } // 時間
        public float Strength { get; } // 揺れの強さ
        public float Vibrato { get; }  // どのくらい振動するか
    }
    private ShakeInfo _shakeInfo;

    private Vector3 _initPosition; // 初期位置
    private bool _isDoShake;       // 揺れ実行中か？
    private float _totalShakeTime; // 揺れ経過時間

    private void Start()
    {
        // 初期位置を保持
        _initPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (!_isDoShake) return;

        // 揺れ位置情報更新
        gameObject.transform.position = UpdateShakePosition(
            gameObject.transform.position,
            _shakeInfo,
            _totalShakeTime,
            _initPosition);

        // duration分の時間が経過したら揺らすのを止める
        _totalShakeTime += Time.deltaTime;
        if (_totalShakeTime >= _shakeInfo.Duration)
        {
            _isDoShake = false;
            _totalShakeTime = 0.0f;
            // 初期位置に戻す
            gameObject.transform.position = _initPosition;
        }
    }

    /// <summary>
    /// 更新後の揺れ位置を取得
    /// </summary>
    /// <param name="currentPosition">現在の位置</param>
    /// <param name="shakeInfo">揺れ情報</param>
    /// <param name="totalTime">経過時間</param>
    /// <param name="initPosition">初期位置</param>
    /// <returns>更新後の揺れ位置</returns>>
    private Vector3 UpdateShakePosition(Vector3 currentPosition, ShakeInfo shakeInfo, float totalTime, Vector3 initPosition)
    {
        // -strength ~ strength の値で揺れの強さを取得
        var strength = shakeInfo.Strength;
        var randomX = Random.Range(-1.0f * strength, strength);
        var randomY = Random.Range(-1.0f * strength, strength);

        // 現在の位置に加える
        var position = currentPosition;
        position.x += randomX;
        position.y += randomY;

        // 初期位置-vibrato ~ 初期位置+vibrato の間に収める
        var vibrato = shakeInfo.Vibrato;
        var ratio = 1.0f - totalTime / shakeInfo.Duration;
        vibrato *= ratio; // フェードアウトさせるため、経過時間により揺れの量を減衰
        position.x = Mathf.Clamp(position.x, initPosition.x - vibrato, initPosition.x + vibrato);
        position.y = Mathf.Clamp(position.y, initPosition.y - vibrato, initPosition.y + vibrato);
        return position;
    }

    /// <summary>
    /// 揺れ開始
    /// </summary>
    /// <param name="duration">時間</param>
    /// <param name="strength">揺れの強さ</param>
    /// <param name="vibrato">どのくらい振動するか</param>
    public void StartShake(float duration, float strength, float vibrato)
    {
        // 揺れ情報を設定して開始
        _shakeInfo = new ShakeInfo(duration, strength, vibrato);
        _isDoShake = true;
        _totalShakeTime = 0.0f;
    }
}
