using System.ComponentModel;

public enum ESkillType
{
    [Description("NORMAL")]
    NORMAL = 1,
    [Description("HOOK")]
    HOOK = 2,
    [Description("STUN")]
    STUN = 3,
    [Description("HOOK_STUN")]
    HOOK_STUN = 4,
}
