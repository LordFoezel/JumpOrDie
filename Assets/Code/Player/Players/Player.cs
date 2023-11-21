public class Player : PlayerBase
{
    public Player(int id)
    {
        this.id = id;
        filename = "Prefaps/Player/PlayerWizard";
        jumpingHeight = 20;
        moveSpeed = 10;
        HitPoints = 5;
        hitPointsMax = 5;
        InitPlayer();
    }
}
