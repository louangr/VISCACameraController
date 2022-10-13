using System.Runtime.Serialization;

namespace VISCACameraController.Models
{
    [DataContract]
    public class FocusMode
    {
        [DataMember(Name = "focusMode")]
        public FocusModes Mode { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }
    }
}
