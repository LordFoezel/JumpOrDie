public class ITickable
{
    public virtual void Tick()
    {
    }

    public void SetTickEvent(LevelsBase levelsbase)
    {
        levelsbase.OnUpdateEvent += Tick;
    }
}
