public partial class StageController : BaseController
{
    /// <summary>
    /// 게임내 AutoPlay 와 관련된 부분을 관리하고 제어.
    /// </summary>

    #region Private Variables

    private bool autoPlay = false;

    Dictionary<uint, AutoPlayInfo> Dic = new Dictionary<uint, AutoPlayInfo>();

    #endregion


    #region Class

    public class AutoPlayInfo
    {
        public TouchEffectController effect;
        public bool isStart;
        public bool isFinish;
        public bool isEffectRelease;

        public AutoPlayInfo(TouchEffectController effect)
        {
            this.effect = effect;
            this.isStart = false;
            this.isFinish = false;
        }
    }

    #endregion


    #region Public Methods

    public bool SetAutoPlayToggle()
    {
        this.autoPlay = !this.autoPlay;

        return this.autoPlay;
    }

    /* 생략 */

    #endregion


    #region Private Methods

    private void AutoCheck()
    {
        /* 오토 플레이 처리 로직 */
    }

    private bool IsCheckAutoLong()
    {
        return TouchObjectPool.CountInactive == touchEffectCreateCount;
    }

    /* 생략 */

    #endregion

}


