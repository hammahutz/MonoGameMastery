const int WIDTH = 1280;
const int HEIGHT = 720;

using var game = new MonoGameMastery.GameEngine.MainGame(WIDTH, HEIGHT, new MonoGameMastery.VerticalShooter.States.DevState());
game.Run();
