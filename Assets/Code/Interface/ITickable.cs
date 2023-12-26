public class ITickable
{
    public void TickBase()
    {
        if(GameManager.ActualGameState == UtilEnum.GameState.Pause) return;
        Tick();
    }

    public virtual void Tick()
    {
    }

    public void SetTickEvent(LevelsBase levelsbase)
    {
        levelsbase.OnUpdateEvent += TickBase;
    }
}
