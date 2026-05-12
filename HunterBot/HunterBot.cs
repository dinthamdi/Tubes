using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class HunterBot : Bot
{
    private int moveDirection = 1;
    private Random rnd = new Random();

    static void Main(string[] args)
    {
        new HunterBot().Start();
    }

    HunterBot() : base(BotInfo.FromFile("HunterBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Red;
        GunColor = Color.DarkRed;
        RadarColor = Color.Orange;
        BulletColor = Color.Yellow;
        TracksColor = Color.Green;
        ScanColor = Color.LightGreen;

        while (IsRunning)
        {
            // Radar selalu scan
            TurnRadarRight(360);

            // Movement agresif tapi tidak barbar
            Forward(150 * moveDirection);

            // Random turn supaya tidak gampang ditembak
            TurnRight(rnd.Next(20, 60));

            // Kadang ganti arah
            if (rnd.Next(0, 100) > 70)
            {
                moveDirection *= -1;
            }
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Fire adaptif
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

        // Rush musuh sedikit
        Forward(80 * moveDirection);

        // Gerakan zigzag setelah scan
        TurnRight(rnd.Next(15, 45));
    }

    public override void OnHitWall(HitWallEvent e)
    {
        // Anti-wall stuck
        moveDirection *= -1;

        Back(120);
        TurnRight(90);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        // Close combat
        Fire(3);

        // Jangan terlalu lama nempel
        Back(80);

        TurnRight(45);
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Evasive movement
        moveDirection *= -1;

        TurnLeft(rnd.Next(30, 70));

        Forward(100);
    }
}

