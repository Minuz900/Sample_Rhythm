public partial class StageController : BaseController
{
    /// <summary>
    /// 게임내 Network와 관련된 부분을 관리하고 제어.
    /// </summary>

    public void OnRhythmRestart(Action<bool> callback)
    {
        /* 게임서버 Restart api를 요청 후 재시작 하는 로직. */
    }

    public void OnRhythmContinue(Action<bool> callback, int beforeCurrencyValue)
    {
        /* 게임서버 Continue api를 요청 후 이어하는 하는 로직. */
    }


    public void RhythmSubmit(Action<bool> callback)
    {
        /* 게임 클리어 후 게임서버 Submit api를 요청 후 다음 스텝으로 가는 로직. */
    }


    public void RhythmFail(Action<bool> callback)
    {
        /* 게임 클리어 실패 후 게임서버 Fail api를 요청 후 다음 스텝으로 가는 로직. */
    }


    public void RhythmCancel(Action<bool> callback)
    {
        /* 게임 중도 포기 후 게임서버 Cancel api를 요청 후 다음 스텝으로 가는 로직. */
    }
}