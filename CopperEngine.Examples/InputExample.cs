using CopperEngine.Info;

namespace CopperEngine.Examples;

public class InputExample : EmptyApplication
{
    protected override void Load()
    {
        // Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Down, () => Log.Info("Down"));
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Pressed, () => Log.Info("Pressed  | Space"));
        Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Released, () => Log.Info("Released | Space"));
        // Input.RegisterInput(KeyboardButton.Space, ButtonPressType.Up, () => Log.Info("Up"));
        
        Input.RegisterInput(MouseButton.Left, ButtonPressType.Pressed, () => Log.Info("Pressed  | Left Mouse"));
        Input.RegisterInput(MouseButton.Left, ButtonPressType.Released, () => Log.Info("Released | Left Mouse"));
        
        Input.RegisterInput(new[]{MouseButton.Left, MouseButton.Right}, ButtonPressType.Pressed, () => Log.Info("Pressed  | Left & Right Mouse"));
        Input.RegisterInput(new[]{MouseButton.Left, MouseButton.Right}, ButtonPressType.Released, () => Log.Info("Released | Left & Right Mouse"));
    }
}