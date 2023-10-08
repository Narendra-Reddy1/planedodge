using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region SINGLETON
    public static InputManager Instance { get; private set; }
    #endregion SINGLETON

    #region Varibales
    [SerializeField] private VariableJoystick _joystick;

    public Vector2 InputVector => _joystick.Direction;
    #endregion Varibales

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    #endregion Unity Methods

    #region Public Methods
    #endregion Public Methods

    #region Private Methods
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks
}
