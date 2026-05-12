using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class TolakAnginBot : Bot
{
    static void Main(string[] args)
    {
        new TolakAnginBot().Start();
    }

    TolakAnginBot() : base(BotInfo.FromFile("TolakAnginBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Blue;
        GunColor = Color.Navy;
        RadarColor = Color.Yellow;
        BulletColor = Color.LightPink;
        TracksColor = Color.Pink; 
        ScanColor = Color.Lightpurple;

        while (IsRunning)
        {
            TurnRadarRight(360);

            // Greedy movement berdasarkan energy
            if (Energy > 60)
            {
                Forward(150);
                TurnRight(20);
            }
            else if (Energy > 30)
            {
                Forward(80);
                TurnLeft(40);
            }
            else
            {
                Back(120);
                TurnRight(90);
            }
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Greedy firepower
        if (Energy > 70)
        {
            Fire(3);
        }
        else if (Energy > 40)
        {
            Fire(2);
        }
        else
        {
            Fire(1);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(100);
        TurnRight(90);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Fire(3);
        Back(50);
    }
}
