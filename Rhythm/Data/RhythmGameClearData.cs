public class RhythmGameClearData
{
    /// <summary>
    /// �������� Ŭ���� ������. 
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

