using System.Runtime.InteropServices;
using Elements.Core;
using ReSounding;

namespace Impressive;
public class SteamEyes
{
    public SteamLinkEye EyeLeft = new();
    public SteamLinkEye EyeRight = new();
    public SteamLinkEye EyeCombined => new()
    {
        Eyelid = MathX.Max(EyeLeft.Eyelid, EyeRight.Eyelid),
        EyeRotation = CombinedEyesDir
    };

    public floatQ CombinedEyesDir
    {
        get
        {
            if(EyeLeft.IsValid && EyeRight.IsValid && EyeLeft.IsTracking && EyeRight.IsTracking)
                _lastValidCombined = MathX.Slerp(EyeLeft.EyeRotation, EyeRight.EyeRotation, 0.5f);
            else if (EyeLeft.IsValid && EyeLeft.IsTracking)
                _lastValidCombined = EyeLeft.EyeRotation;
            else if (EyeRight.IsValid && EyeRight.IsTracking)
                _lastValidCombined = EyeRight.EyeRotation;

            return _lastValidCombined;
        }
    }

    private floatQ _lastValidCombined = floatQ.Identity;


    #region EyesDir

    public bool EyesReversed => Impressive.Config!.GetValue(Impressive.Eyes_Reversed_Config);

    // Left eye direction 
    [OSCMap("/avatar/parameters/FT/v2/EyeLeftX")]
    public float LeftEyeX { set => EyeLeft.SetDirectionFromXY(X: value); }

    [OSCMap("/avatar/parameters/FT/v2/EyeLeftY")]
    public float LeftEyeY { set => EyeLeft.SetDirectionFromXY(Y: EyesReversed ? -value : value); }

    

    // Right eye direction 
    [OSCMap("/avatar/parameters/FT/v2/EyeRightX")]
    public float RightEyeX { set => EyeRight.SetDirectionFromXY(X: value); }

    [OSCMap("/avatar/parameters/FT/v2/EyeRightY")]
    public float RightEyeY { set => EyeRight.SetDirectionFromXY(Y: EyesReversed ? -value : value); }

    

    #endregion

    #region Eyelids

    // Right eyes
    [OSCMap("/avatar/parameters/FT/v2/EyeLidRight")]
    public float RightEyeLid { set => EyeRight.Eyelid = value; }

    [OSCMap("/avatar/parameters/FT/v2/EyeSquintRight")]
    public float RightEyeLidExpandedSqueeze { set => EyeRight.ExpandedSqueeze = value; }


    // [OSCMap("/avatar/parameters/RightEyeSqueezeToggle")]
    // public int RightEyeSqueezeToggle { set => EyeRight.SqueezeToggle = value; } // Not used

    // [OSCMap("/avatar/parameters/RightEyeWidenToggle")]
    // public int RightEyeWidenToggle { set => EyeRight.WidenToggle = value; } // Not used



    // Left eyes
    [OSCMap("/avatar/parameters/FT/v2/EyeLidLeft")]
    public float LeftEyeLid { set => EyeLeft.Eyelid = value; }

    [OSCMap("/avatar/parameters/FT/v2/EyeSquintLeft")]
    public float LeftEyeLidExpandedSqueeze { set => EyeLeft.ExpandedSqueeze = value; }


    // [OSCMap("/avatar/parameters/LeftEyeSqueezeToggle")]
    // public int LeftEyeSqueezeToggle { set => EyeLeft.SqueezeToggle = value; } // Not used

    // [OSCMap("/avatar/parameters/LeftEyeWidenToggle")]
    // public int LeftEyeWidenToggle { set => EyeLeft.WidenToggle = value; } // Not used


    #endregion
}

public struct SteamLinkEye
{
    public readonly bool IsTracking => IsValid && Eyelid > 0.1f;

    public readonly bool IsValid => EyeDirection.Magnitude > 0f && MathX.IsValid(EyeDirection);

    public float3 EyeDirection
    {
        readonly get => EyeRotation * float3.Forward;
        set => EyeRotation = floatQ.LookRotation(EyeDirection);
    }

    public floatQ EyeRotation;

    private float DirX;
    private float DirY;

    public float Eyelid;

    public float ExpandedSqueeze;
    public int WidenToggle;
    public int SqueezeToggle;

    public void SetDirectionFromXY(float? X = null, float? Y = null)
    {
        DirX = X ?? DirX;
        DirY = Y ?? DirY;

        // Get the angles out of the eye look
        float xAng = MathX.Asin(DirX);
        float yAng = MathX.Asin(DirY);

        // Convert to cartesian coordinates
        EyeRotation = floatQ.Euler(yAng * MathX.Rad2Deg, xAng * MathX.Rad2Deg, 0f);
    }
}