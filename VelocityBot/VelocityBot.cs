using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class VelocityBot : Bot
{
    private int moveDirection = 1;

    static void Main(string[] args)
    {
        new VelocityBot().Start();
    }

    VelocityBot() : base(BotInfo.FromFile("VelocityBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Purple;
        GunColor = Color.DarkViolet;
        RadarColor = Color.Magenta;
        BulletColor = Color.DeepPink;
        TracksColor = Color.White;

        while (IsRunning)
        {
            // Radar muter terus
            TurnRadarRight(360);

            // Movement cepat dan random
            Forward(150 * moveDirection);

            TurnRight(40);

            // Ganti arah biar susah ditembak
            moveDirection *= -1;
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Fire berdasarkan energy
        if (Energy > 60)
        {
            Fire(3);
        }
        else if (Energy > 30)
        {
            Fire(2);
        }
        else
        {
            Fire(1);
        }

        // Gerak cepat setelah scan
        TurnRight(20);
        Forward(100 * moveDirection);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        // Pantul dari dinding
        moveDirection *= -1;

        Back(120);
        TurnRight(90);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        // Serang dekat
        Fire(3);

        TurnRight(45);
        Back(80);
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Evasive movement
        moveDirection *= -1;

        TurnLeft(60);
        Forward(120);
    }
}

