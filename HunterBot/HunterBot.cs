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

            // SMART MOVEMENT
            if (Energy > 50)
            {
                // Agresif tapi tetap aman
                Forward(140 * moveDirection);

                TurnRight(rnd.Next(20, 45));
            }
            else
            {
                // Smarter retreat
                Back(120);

                TurnLeft(60);
            }

            // Ganti arah sesekali agar tidak mudah diprediksi
            if (rnd.Next(0, 100) > 75)
            {
                moveDirection *= -1;
            }
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Adaptive greedy firepower
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

        // Jangan terlalu frontal
        if (Energy > 50)
        {
            Forward(60 * moveDirection);
        }
        else
        {
            Back(80);
        }

        // Zig-zag movement setelah scan
        TurnRight(rnd.Next(15, 40));
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

        // Jangan terlalu lama menempel
        Back(80);

        TurnRight(45);

        moveDirection *= -1;
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Anti bullet evasive movement
        moveDirection *= -1;

        TurnLeft(rnd.Next(30, 70));

        Forward(rnd.Next(80, 150));

        // Kadang reposition kecil
        if (Energy < 40)
        {
            Back(60);

            TurnRight(45);
        }
    }
}
