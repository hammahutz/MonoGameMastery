using MonoGameMastery.GameEngine.Input;

namespace MonoGameMastery.VerticalShooter.Input;

public  class DevInputCommand : BaseInputCommand
{
    public class DevQuit : DevInputCommand { };
    public class DevShoot : DevInputCommand { };
}