public class TouchEffectController : BaseController
{
    /// <summary>
    /// 롱노트를 터치 했을때 생성되어 플레이 되는 이펙트. 
    /// </summary>

    // StageController 에서 사용하는 오브젝트 풀 객체.
    private IObjectPool<TouchEffectController> effectManagedPool;

    [SerializeField]
    private ParticleSystemCallback particleSystemCallback;

    [SerializeField]
    private ParticleSystem effectParticle;


    private bool isAutioRelease = false;

    public delegate void ParticleSystemStopCallback();
    public ParticleSystemStopCallback particleSystemStopCallback = null;

    public void SetManagedPool(IObjectPool<TouchEffectController> pool)
    {
        effectManagedPool = pool;

        particleSystemCallback.SetCallback(this);
        particleSystemStopCallback = OnParticleSystemCallback;
    }

    public void PlayEffectPlarticle(float x)
    {
        this.effectPos.x = x;
        this.transform.localPosition = effectPos;
    }


    public void SetAutoRelease()
    {
        isAutioRelease = true;
    }


    public void OnParticleSystemCallback()
    {
        if (isAutioRelease)
        {
            isAutioRelease = false;
        }
    }

    public void InitEffect()
    {
        this.transform.localPosition = initPos;
    }

    public void DestroyEffect()
    {
        this.effectManagedPool.Release(this);
    }
}


