using System.Xml.Serialization;

namespace Apps.XTRF.Classic.Models.Entities;

[XmlRoot("loginResponse")]
public class LoginDto
{
    [XmlElement("jsessionid")]
    public string JSessionId { get; set; }

    public string JsessionCookie { get; set; }
}