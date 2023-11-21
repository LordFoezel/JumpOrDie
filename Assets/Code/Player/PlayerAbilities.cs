public class PlayerAbilities
{
    public int id;
    public string filename;
    public int jumpingHeight;
    public int moveSpeed;
    private int hitPoints;
    public int hitPointsMax;

    public int HitPoints { get => hitPoints; set => hitPoints = value; }
}
