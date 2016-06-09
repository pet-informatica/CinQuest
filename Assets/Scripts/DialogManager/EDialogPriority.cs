/// <summary>
/// Developed by: Higor (hcmb)
/// Enum of possible priorities for a dialog speaker. 
/// Speakers with higher priority will cancel the dialog of lesser ones when they try to speak.
/// If the priority is the same or lower, it must wait.
/// </summary>
public enum EDialogPriority {
    Maximum,
    High,
    Medium,
    Low,
    Minimum,
}
