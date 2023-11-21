public class ITickable
{
    public virtual void Tick()
    {
    }

    public void SetTickEvent(LevelBaseLoader levelBaseLoader)
    {
        levelBaseLoader.OnTickEvent += Tick;
    }
}
