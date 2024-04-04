using Elements.Core;
using ReSounding;

namespace Impressive;

public class SteamFace
{
    public float CheekPuffSuckL => CheekPuffL - CheekSuckL;
    public float CheekPuffSuckR => CheekPuffR - CheekSuckR;
    public float3 JawPos => new(JawRight - JawLeft, -JawDown, JawForward);
    
    public float SmileFrownLeft => SmileL - FrownL;
    public float SmileFrownRight => SmileR - FrownR;

    public float LipBottomOverturn => (LipFunnelBottomL + LipFunnelBottomR) * 0.5f;
    public float LipTopOverturn => (LipFunnelTopL + LipFunnelTopR) * 0.5f;

    public float LipSuckTop => (LipSuckTopL + LipSuckTopR) * 0.5f;
    public float LipSuckbottom => (LipSuckBottomL + LipSuckBottomR) * 0.5f;


    // Cheeks
    [OSCMap("/avatar/parameters/FT/v2/CheekPuffLeft")]
    public float CheekPuffL;

    [OSCMap("/avatar/parameters/FT/v2/CheekSuckLeft")]
    public float CheekSuckL;

    [OSCMap("/avatar/parameters/FT/v2/CheekPuffRight")]
    public float CheekPuffR;

    [OSCMap("/avatar/parameters/FT/v2/CheekSuckRight")]
    public float CheekSuckR;



    // Jaw
    [OSCMap("/avatar/parameters/FT/v2/JawLeft")]
    public float JawLeft;

    [OSCMap("/avatar/parameters/FT/v2/JawRight")]
    public float JawRight;

    [OSCMap("/avatar/parameters/FT/v2/JawOpen")]
    public float JawDown;

    [OSCMap("/avatar/parameters/FT/v2/JawForward")]
    public float JawForward;


    // Mouth Left/Right
    [OSCMap("/avatar/parameters/FT/v2/MouthPressLeft")]
    public float MouthLeft;

    [OSCMap("/avatar/parameters/FT/v2/MouthPressRight")]
    public float MouthRight;


    // Lips
    [OSCMap("/avatar/parameters/FT/v2/MouthClosed")]
    public float LipsToward; // Whether the lips are closed when the jaw is down

    [OSCMap("/avatar/parameters/FT/v2/LipPuckerLowerLeft")]
    public float LipPuckerL;

    [OSCMap("/avatar/parameters/FT/v2/LipPuckerLowerRight")]
    public float LipPuckerR;

    [OSCMap("/avatar/parameters/FT/v2/MouthLowerDownLeft")]
    public float LowerLeftLip;

    [OSCMap("/avatar/parameters/FT/v2/MouthLowerDownRight")]
    public float LowerRightLip;

    [OSCMap("/avatar/parameters/FT/v2/MouthUpperUpLeft")]
    public float RaiseLeftLip;

    [OSCMap("/avatar/parameters/FT/v2/MouthUpperUpRight")]
    public float RaiseRightLip;

    [OSCMap("/avatar/parameters/FT/v2/LipFunnelLowerLeft")]
    public float LipFunnelBottomL;

    [OSCMap("/avatar/parameters/FT/v2/LipFunnelLowerRight")]
    public float LipFunnelBottomR;

    [OSCMap("/avatar/parameters/FT/v2/LipFunnelUpperLeft")]
    public float LipFunnelTopL;

    [OSCMap("/avatar/parameters/FT/v2/LipFunnelUpperRight")]
    public float LipFunnelTopR;

    [OSCMap("/avatar/parameters/FT/v2/LipSuckLowerLeft")]
    public float LipSuckBottomL;

    [OSCMap("/avatar/parameters/FT/v2/LipSuckLowerRight")]
    public float LipSuckBottomR;

    [OSCMap("/avatar/parameters/FT/v2/LipSuckUpperLeft")]
    public float LipSuckTopL;

    [OSCMap("/avatar/parameters/FT/v2/LipSuckUpperRight")]
    public float LipSuckTopR;


    //Smiling/Frowning
    [OSCMap("/avatar/parameters/FT/v2/MouthCornerPullLeft")]
    public float SmileL;

    [OSCMap("/avatar/parameters/FT/v2/MouthCornerPullRight")]
    public float SmileR;

    [OSCMap("/avatar/parameters/FT/v2/MouthFrownLeft")]
    public float FrownL;

    [OSCMap("/avatar/parameters/FT/v2/MouthFrownRight")]
    public float FrownR;



    // Tongue


    /* 
    [OSCMap("/avatar/parameters/FT/v2/ToungeTipInterdental")]
    [OSCMap("/avatar/parameters/FT/v2/FrontDorsalPalate")]
    [OSCMap("/avatar/parameters/FT/v2/FrontDorsalPalate")]
    [OSCMap("/avatar/parameters/FT/v2/MidDorsalPalate")]
    [OSCMap("/avatar/parameters/FT/v2/BackDorsalVelar")]
    [OSCMap("/avatar/parameters/FT/v2/ToungeRetreat")]
    [OSCMap("/avatar/parameters/FT/v2/ToungeTipAlveolar")] 
    */

    // Seems to only support tongue out properly right now.
    [OSCMap("/avatar/parameters/FT/v2/TongueOut")]
    public float TongueOut;
    

}