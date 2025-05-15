public class InGameLoadingData
{
    /// <summary>
    /// 인게임에 사용되는 미리 로드되는 데이터.
    /// </summary>

    // 노트들이 사용하는 Sprite 들.
    public Sprite[] nodeSprites = null;

    // 음원.
    public List<AudioClip> audioClips = new List<AudioClip>();

    // 모든 Knots.
    public Dictionary<string, List<Knot>> knots = new Dictionary<string, List<Knot>>();

    // 모든 Lines.
    public List<Line> lines = new List<Line>();

    // 정보.
    public List<Tempo> tempos = new List<Tempo>();

    // 정렬된 Knots.
    public List<Knot> sortedKnots = new List<Knot>();

    // 영상.
    public List<VideoClip> boardVideoClips = new List<VideoClip>();
    public List<VideoClip> backgroundVideoClip = new List<VideoClip>();

    // 이미지.
    public Texture2D backgroundImage;

    // 에셋으로 변경이 자주 일어나는 값들.
    public GamePlayVariables variables = new GamePlayVariables();

    public class GamePlayVariables
    {
        public int initialHP = 1000;

        //노트별 점수
        public int normalNoteHitScore = 0;
        public int longNoteHitScore = 0;
        public int flickNoteHitScore = 0;
        public int boosterNoteHitScore = 0;

        //노트별 피버 점수
        public int normalNoteHitFeverScore = 0;
        public int longNoteHitFeverScore = 0;
        public int flickNoteHitFeverScore = 0;
        public int boosterNoteHitFeverScore = 0;

        // 노트별 미스 차감 점수.
        public int normalNoteMissScore = 0;
        public int longNoteMissScore = 0;
        public int flickNoteMissScore = 0;
        public int boosterNoteMissScore = 0;

        // 판정등급 스코어 비율
        public double perfectSocreRatio = 1d;
        public double greatSocreRatio = 1d;
        public double goodSocreRatio = 1d;
        public double badSocreRatio = 1d;
        public double missSocreRatio = 1d;

        // 판정등급 스코어 비율
        public double perfectDecreaseHPRatio = 1d;
        public double greatDecreaseHPRatio = 1d;
        public double goodDecreaseHPRatio = 1d;
        public double badDerceaseHPRatio = 1d;
        public double missDecreaseHPRatio = 1d;

        public bool perfectCombo = true;
        public bool greatCombo = true;
        public bool goodCombo = false;
        public bool badCombo = false;
        public bool missCombo = false;

        /* 생략 */
    }


    public void Clear()
    {
        this.nodeSprites = null;

        this.audioClips.Clear();
        this.knots.Clear();
        this.lines.Clear();
        this.tempos.Clear();
        this.sortedKnots.Clear();

        this.boardVideoClips.Clear();
        this.backgroundVideoClip.Clear();

        this.backgroundImage = null;

        this.variables = new GamePlayVariables();
    }
}
