using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class SniperBot : Bot
{
    static void Main(string[] args)
    {
        new SniperBot().Start();
    }

    SniperBot() : base(BotInfo.FromFile("SniperBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Blue;
        GunColor = Color.DarkBlue;
        RadarColor = Color.Cyan;
        BulletColor = Color.LightBlue;
        TracksColor = Color.Silver;
        ScanColor = Color.Aqua;

        while (IsRunning)
        {
            // Radar muter terus untuk scan musuh
            TurnRadarRight(360);

            // Movement sniper (jaga jarak)
            Back(50);
            TurnRight(30);
            Forward(80);
            TurnLeft(30);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Fire ringan agar cepat dan akurat
        Fire(1);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(100);
        TurnRight(90);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Back(50);
        Fire(2);
    }
}

