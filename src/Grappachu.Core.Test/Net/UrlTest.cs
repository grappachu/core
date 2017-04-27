using Grappachu.Core.Preview.Net;
using NUnit.Framework;

namespace Grappachu.Core.Test.Net
{
    [TestFixture]
    class UrlTest
    {


        [TestCase("", "", ExpectedResult = "")]
        [TestCase("", "/url/Part/another/Part/", ExpectedResult = "/url/Part/another/Part/")]
        [TestCase("/url/Part/another/Part/", "", ExpectedResult = "/url/Part/another/Part/")]
        [TestCase("/url/Part", "another/Part/", ExpectedResult = "/url/Part/another/Part/")]
        [TestCase("/url/Part", "/another/Part/", ExpectedResult = "/url/Part/another/Part/")]
        [TestCase("/url/Part/", "another/Part/", ExpectedResult = "/url/Part/another/Part/")]
        [TestCase("/url/Part/", "/another/Part/", ExpectedResult = "/url/Part/another/Part/")]
        public string Should_Combine_Url_Portions(string part1, string part2)
        {
            string result = Url.Combine(part1, part2);
            return result;
        }

        [TestCase("http://localhost", "", ExpectedResult = "http://localhost/")]
        [TestCase("ftp://192.168.0.1", "", ExpectedResult = "ftp://192.168.0.1/")]
        [TestCase("https://www.testwebsite862493.com", "", ExpectedResult = "https://www.testwebsite862493.com/")]
        public string Should_Add_Slashes_For_Roots(string part1, string part2)
        {
            string result = Url.Combine(part1, part2);
            return result;
        }


        [TestCase("http://localhost/Test", "", ExpectedResult = "http://localhost/Test")]
        [TestCase("http://localhost/TestPage.aspx", "", ExpectedResult = "http://localhost/TestPage.aspx")]
        [TestCase("http://192.168.0.1/TestPage.php", "", ExpectedResult = "http://192.168.0.1/TestPage.php")]
        [TestCase("http://www.testwebsite862493.com/TestPage.htm", "", ExpectedResult = "http://www.testwebsite862493.com/TestPage.htm")]
        public string Should_Not_Add_Slashes_For_Pages(string part1, string part2)
        {
            string result = Url.Combine(part1, part2);
            return result;
        }

        [TestCase("http://localhost/Test", "", ExpectedResult = "http://localhost/Test")]
        [TestCase("http://localhost/Test/", "", ExpectedResult = "http://localhost/Test/")]
        [TestCase("http://www.testwebsite862493.com/TestPage.htm?p=1", "", ExpectedResult = "http://www.testwebsite862493.com/TestPage.htm?p=1")]
        public string Should_Preserve_Well_Formed_Urls(string part1, string part2)
        {
            string result = Url.Combine(part1, part2);
            return result;
        }

        [TestCase("http://www.testwebsite00001.com", "my/Test/rOOt/Page.aspx", ExpectedResult = "http://www.testwebsite00001.com/my/Test/rOOt/Page.aspx")]
        [TestCase("http://www.testwebsite00002.com/", "my/Test/rOOt/Page.aspx", ExpectedResult = "http://www.testwebsite00002.com/my/Test/rOOt/Page.aspx")]
        [TestCase("http://www.testwebsite00003.com", "/my/Test/rOOt/Page.aspx", ExpectedResult = "http://www.testwebsite00003.com/my/Test/rOOt/Page.aspx")]
        [TestCase("http://www.testwebsite00004.com/", "/my/Test/rOOt/Page.aspx", ExpectedResult = "http://www.testwebsite00004.com/my/Test/rOOt/Page.aspx")]
        [TestCase("http://www.testwebsite00005.com/my/", "/Test/rOOt/Page.aspx?q=23,6", ExpectedResult = "http://www.testwebsite00005.com/my/Test/rOOt/Page.aspx?q=23,6")]
        [TestCase("http://www.testwebsite00006.com/my/Test", "rOOt/Page.aspx?q=23,6", ExpectedResult = "http://www.testwebsite00006.com/my/Test/rOOt/Page.aspx?q=23,6")]
        [TestCase("http://www.testwebsite00007.com/my/Test/rOOt", "/Page.aspx?q=23,6", ExpectedResult = "http://www.testwebsite00007.com/my/Test/rOOt/Page.aspx?q=23,6")]
        [TestCase("http://www.testwebsite00008.com/my/Test/rOOt/", "Page.aspx?q=23,6", ExpectedResult = "http://www.testwebsite00008.com/my/Test/rOOt/Page.aspx?q=23,6")]
        public string Should_Combine_Root_And_Path_Handling_Slashes(string part1, string part2)
        {
            string result = Url.Combine(part1, part2);
            return result;
        }

        [TestCase("http://www.testwebsite00001.com/Test.aspx", "?query=true", ExpectedResult = "http://www.testwebsite00001.com/Test.aspx?query=true")]
        [TestCase("http://www.testwebsite00002.com/Test.aspx?", "query=true", ExpectedResult = "http://www.testwebsite00002.com/Test.aspx?query=true")]
        [TestCase("http://www.testwebsite00003.com/Test.aspx?", "?query=true", ExpectedResult = "http://www.testwebsite00003.com/Test.aspx?query=true")]
        [TestCase("http://www.testwebsite00004.com/Folder/", "?query=true", ExpectedResult = "http://www.testwebsite00004.com/Folder/?query=true")]
        [TestCase("http://www.testwebsite00005.com/Page", "?query=true", ExpectedResult = "http://www.testwebsite00005.com/Page?query=true")]
        public string Should_Combine_Path_And_Query(string part1, string part2)
        {
            string result = Url.Combine(part1, part2);
            return result;
        }



    

    }
}
 
