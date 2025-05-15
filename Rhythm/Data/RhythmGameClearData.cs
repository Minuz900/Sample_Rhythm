public class RhythmGameClearData
{
    /// <summary>
    /// 스테이지 클리어 데이터. 
    /// </summary>

    public Clear rhythmClear;

    public class Clear
    {
        public int musicCode;
        public int grade;
        public int highRecord;
    }

    public RhythmGameClearData(Clear data)
    {
        rhythmClear = data;
    }
}

