public class Player : PlayerBase
{
    public Player(int id)
    {
        this.id = id;
        filename = "Prefaps/Player/PlayerWizard";
        jumpingHeight = 20;
        moveSpeed = 10;
        hitPoints = 3;
        hitPointsMax = 3;
        InitPlayer();
    }
}
