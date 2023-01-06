using System.Linq;

const int WIDTH = 1280;
const int HEIGHT = 720;

MonoGameMastery.GameEngine.States.BaseGameState setup = new MonoGameMastery.VerticalShooter.States.SplashState();

args?.ToList().ForEach(x =>
    {
        switch (x)
        {
            case "d":
                setup = new MonoGameMastery.VerticalShooter.States.DevState();
                break;

            case "q":
                setup = new MonoGameMastery.VerticalShooter.States.GameplayState();
                break;
        }
    });

using var game = new MonoGameMastery.GameEngine.MainGame(WIDTH, HEIGHT, setup);
game.Run();