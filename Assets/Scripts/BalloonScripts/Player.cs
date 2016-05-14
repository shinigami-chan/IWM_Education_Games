public class Player
{
    private int playerId;
    private string playerName = "Spieler";
    private int points = 0;

    public Player() { }
    public Player(string name)
    {
        playerName = name;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int receivedPoints)
    {
        points += receivedPoints;
    }
}
