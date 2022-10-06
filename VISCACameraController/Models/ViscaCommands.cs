using System.Runtime.Serialization;

namespace VISCACameraController.Models
{
    [DataContract]
    public class ViscaCommands
    {
        [DataMember(Name = "switchOnCamera")]
        public string SwitchOnCamera { get; set; }

        [DataMember(Name = "switchOffCamera")]
        public string SwitchOffCamera { get; set; }

        [DataMember(Name = "goToPreset1")]
        public string GoToPreset1 { get; set; }

        [DataMember(Name = "goToPreset2")]
        public string GoToPreset2 { get; set; }

        [DataMember(Name = "goToPreset3")]
        public string GoToPreset3 { get; set; }

        [DataMember(Name = "goToPreset4")]
        public string GoToPreset4 { get; set; }

        [DataMember(Name = "goToPreset5")]
        public string GoToPreset5 { get; set; }

        [DataMember(Name = "goToPreset6")]
        public string GoToPreset6 { get; set; }

        [DataMember(Name = "goToPreset7")]
        public string GoToPreset7 { get; set; }

        [DataMember(Name = "goToPreset8")]
        public string GoToPreset8 { get; set; }

        [DataMember(Name = "stopTiltPanMove")]
        public string StopTiltPanMove { get; set; }

        [DataMember(Name = "topTiltMove")]
        public string TopTiltMove { get; set; }

        [DataMember(Name = "bottomTiltMove")]
        public string BottomTiltMove { get; set; }

        [DataMember(Name = "rightPanMove")]
        public string RightPanMove { get; set; }

        [DataMember(Name = "leftPanMove")]
        public string LeftPanMove { get; set; }

        [DataMember(Name = "stopFocusMove")]
        public string StopFocusMove { get; set; }

        [DataMember(Name = "setAutoFocus")]
        public string SetAutoFocus { get; set; }

        [DataMember(Name = "setManualFocus")]
        public string SetManualFocus { get; set; }

        [DataMember(Name = "focusFurther")]
        public string FocusFurther { get; set; }

        [DataMember(Name = "focusNearer")]
        public string FocusNearer { get; set; }

        [DataMember(Name = "stopZoomMove")]
        public string StopZoomMove { get; set; }

        [DataMember(Name = "zoomIn")]
        public string ZoomIn { get; set; }

        [DataMember(Name = "zoomOut")]
        public string ZoomOut { get; set; }
    }
}
