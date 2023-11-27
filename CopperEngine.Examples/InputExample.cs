using CopperEngine.Info;
using CopperEngine.Logs;

namespace CopperEngine.Examples;

public class InputExample : EmptyApplication
{
    protected override void Load()
    {
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Down, () => Log.Info("Down"));
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Pressed, () => Log.Info("Pressed"));
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Released, () => Log.Info("Released"));
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Up, () => Log.Info("Up"));
    }
}