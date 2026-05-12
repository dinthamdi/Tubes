using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class KacauuwBot : Bot
{
    private int moveDirection = 1;

    static void Main(string[] args)
    {
        new KacauuwBot().Start();
    }

    KacauuwBot() : base(BotInfo.FromFile("KacauuwBot.json")) { }

    public override void Run()
    {
        // Warna bot
        BodyColor = Color.DarkRed;
        TurretColor = Color.Red;
        RadarColor = Color.OrangeRed;
        BulletColor = Color.Yellow;
        ScanColor = Color.Orange;
        TracksColor = Color.Black;
        GunColor = Color.DarkOrange;

        // Radar independen
        AdjustGunForBodyTurn = true;
        AdjustRadarForGunTurn = true;

        while (IsRunning)
        {
            // Radar muter terus biar agresif scan
            TurnRadarRight(360);

            // Movement cepat dan random
            Forward(200 * moveDirection);

            TurnRight(35);

            // Zigzag agresif
            if (Energy < 40)
            {
                moveDirection *= -1;
            }
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Agresif: langsung rush musuh
        Forward(120);

        // Tembakan besar
        if (Energy > 60)
        {
            Fire(3);
        }
        else if (Energy > 25)
        {
            Fire(2);
        }
        else
        {
            Fire(1);
        }

        // Gerakan acak biar susah ditembak
        TurnRight(20);
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Dodge cepat saat kena peluru
        moveDirection *= -1;

        TurnRight(90);
        Forward(150);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        // Mental dari dinding
        Back(120);
        TurnRight(90);

        moveDirection *= -1;
    }

    public override void OnHitBot(HitBotEvent e)
    {
        // Brutal jarak dekat
        Fire(3);

        Forward(80);
        TurnRight(30);
    }
}

