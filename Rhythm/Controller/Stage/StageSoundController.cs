public partial class StageController : BaseController
{
    /// <summary>
    /// 게임내 Sound 와 관련된 부분을 관리하고 제어.
    /// </summary>

    #region SerializeField

    // 음악 플레이 되도록 하는 소스
    [SerializeField]
    private AudioSource musicAudioSource;

    /* 생략 */

    #endregion


    #region Public Methods

    public IEnumerator MusicFadeOut()
    {
        currentTime = 0;
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(normalVolume, minVolume, currentTime);

            yield return null;
        }
        musicAudioSource.volume = minVolume;
    }

    public IEnumerator MusicFadeIn()
    {
        currentTime = 0;
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(minVolume, normalVolume, currentTime);

            yield return null;
        }
        musicAudioSource.volume = normalVolume;
    }

    /* 생략 */

    #endregion


    #region Private Methods

    private void InitSoundController()
    {

    }

    /* 생략 */

    #endregion

}

