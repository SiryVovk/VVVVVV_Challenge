using UnityEngine;
using UnityEngine.InputSystem;
using static Player;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    public Player Controls {get; private set;}

    private const string INPUT_BINDING = "InputBindings";

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Controls = new Player();

        if (PlayerPrefs.HasKey(INPUT_BINDING))
        {
            Controls.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString(INPUT_BINDING)
            );
        }
    }
}
