using UnityEngine;

public class wheelController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isSpinning = false;
    private float timer = 0f;
    private float currentRotation = 0f;

    void Start()
    {
        // 自動取得輪盤物件上的 Audio Source 組件
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 點擊滑鼠左鍵（或點擊手機螢幕）且輪盤不在旋轉時觸發
        if (Input.GetMouseButtonDown(0) && !isSpinning)
        {
            isSpinning = true;
            timer = 0f;

            if (audioSource != null) audioSource.Play(); // 播放音效
        }

        if (isSpinning)
        {
            timer += Time.deltaTime;

            if (timer < 2f)
            {
                
                float t = timer / 2f;

                // 平滑的加速與減速曲線公式 (SmoothStep)
                float progress = t * t * (3f - 2f * t);

                // 7200度代表兩秒內總共轉20圈 (可隨意調大或調小)
                currentRotation = progress * 7200f;

                // 直接套用旋轉角度到 Z 軸
                transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            }
            else
            {
                // 時間到（滿兩秒），強制結束
                isSpinning = false;
                if (audioSource != null) audioSource.Stop(); // 停止音效
            }
        }
    }
}