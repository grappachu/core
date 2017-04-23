using NUnit.Framework;

namespace Grappachu.Core.Test.Media
{
    [TestFixture]
    class MimeTest
    {

        [TestCase("mp3", ExpectedResult = "audio/mpeg")]
        [TestCase(".mp3", ExpectedResult = "audio/mpeg")]
        [TestCase("MP3", ExpectedResult = "audio/mpeg")]
        [TestCase(".Mp3", ExpectedResult = "audio/mpeg")]
        public string Should_Get_Mime_Type_From_Extension_Ignoring_Case(string extension)
        {
            string result = Core.Media.Mime.GetByFileExtension(extension);
            return result;
        }

        [TestCase("", ExpectedResult = "application/octet-stream")]
        [TestCase("unkn0wn", ExpectedResult = "application/octet-stream")]
        public string Should_Have_A_Default_For_Unknown_Types(string extension)
        {
            // RFC 2046 states in section 4.5.1: 
            //  The "octet-stream" subtype is used to indicate that a body contains arbitrary binary data.
            string result = Core.Media.Mime.GetByFileExtension(extension);
            return result;
        }
    }
}
 
