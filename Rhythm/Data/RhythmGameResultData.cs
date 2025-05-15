public class RhythmGameResultData
{
    /// <summary>
    /// 스테이지 결과 데이터. 
    /// </summary>

    public Result resultData;

    public class Result
    {
        public int perfectCount;
        public int greatCount;
        public int goodCount;
        public int badCount;
        public int missCount;

        public int normalNoteClearCount;
        public int longNoteClearCount;
        public int flickNoteClearCount;
        public int boosterNoteClearCount;

        public int maxComboCount;

        public double totalScore;

        public int grade;
    }


    public RhythmGameResultData(Result data)
    {
        resultData = data;
    }

    public void Clear()
    {
        resultData = new Result();
    }
}

