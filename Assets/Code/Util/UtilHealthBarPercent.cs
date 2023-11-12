public static class UtilHealthBarPercent
{
    public static float getSizeOfHealthBar(int hitPoints, int hitPointsMax, float width = 200f) {
        return width / hitPointsMax * hitPoints;
    }
}
