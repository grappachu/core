using System.Collections.Generic;

namespace Grappachu.Core.Media
{
    /// <summary>
    /// Definisce dei metodi di utilità per la manipolazione dei tipi Mime
    /// </summary>
    public class Mime
    {
        private const string DEFAULT_KEY = " ";
        private static Dictionary<string, string> _dictionary;

        
        private static Dictionary<string, string> Dictionary
        {
            get
            {
                if (_dictionary == null)
                {
                    _dictionary = new Dictionary<string, string>();
                    OnAddTypes();
                }
                return _dictionary;
            }
        }

        /// <summary>
        /// Aggiunge tutte le voci al dizionario
        /// </summary>
        /// <remarks>
        /// Riferimento: http://www.w3schools.com/media/media_mimeref.asp
        /// Tipi aggiunti: flv, xml
        /// Aggiornato al 23 Dec 2009
        /// </remarks>
        private static void OnAddTypes()
        {
            _dictionary.Add(DEFAULT_KEY, "application/octet-stream");
            _dictionary.Add("323", "text/h323");
            _dictionary.Add("acx", "application/internet-property-stream");
            _dictionary.Add("ai", "application/postscript");
            _dictionary.Add("aif", "audio/x-aiff");
            _dictionary.Add("aifc", "audio/x-aiff");
            _dictionary.Add("aiff", "audio/x-aiff");
            _dictionary.Add("asf", "video/x-ms-asf");
            _dictionary.Add("asr", "video/x-ms-asf");
            _dictionary.Add("asx", "video/x-ms-asf");
            _dictionary.Add("au", "audio/basic");
            _dictionary.Add("avi", "video/x-msvideo");
            _dictionary.Add("axs", "application/olescript");
            _dictionary.Add("bas", "text/plain");
            _dictionary.Add("bcpio", "application/x-bcpio");
            _dictionary.Add("bin", "application/octet-stream");
            _dictionary.Add("bmp", "image/bmp");
            _dictionary.Add("c", "text/plain");
            _dictionary.Add("cat", "application/vnd.ms-pkiseccat");
            _dictionary.Add("cdf", "application/x-cdf");
            _dictionary.Add("cer", "application/x-x509-ca-cert");
            _dictionary.Add("class", "application/octet-stream");
            _dictionary.Add("clp", "application/x-msclip");
            _dictionary.Add("cmx", "image/x-cmx");
            _dictionary.Add("cod", "image/cis-cod");
            _dictionary.Add("cpio", "application/x-cpio");
            _dictionary.Add("crd", "application/x-mscardfile");
            _dictionary.Add("crl", "application/pkix-crl");
            _dictionary.Add("crt", "application/x-x509-ca-cert");
            _dictionary.Add("csh", "application/x-csh");
            _dictionary.Add("css", "text/css");
            _dictionary.Add("dcr", "application/x-director");
            _dictionary.Add("der", "application/x-x509-ca-cert");
            _dictionary.Add("dir", "application/x-director");
            _dictionary.Add("dll", "application/x-msdownload");
            _dictionary.Add("dms", "application/octet-stream");
            _dictionary.Add("doc", "application/msword");
            _dictionary.Add("dot", "application/msword");
            _dictionary.Add("dvi", "application/x-dvi");
            _dictionary.Add("dxr", "application/x-director");
            _dictionary.Add("eps", "application/postscript");
            _dictionary.Add("etx", "text/x-setext");
            _dictionary.Add("evy", "application/envoy");
            _dictionary.Add("exe", "application/octet-stream");
            _dictionary.Add("fif", "application/fractals");
            _dictionary.Add("flr", "x-world/x-vrml");
            _dictionary.Add("flv", "video/x-flv");
            _dictionary.Add("gif", "image/gif");
            _dictionary.Add("gtar", "application/x-gtar");
            _dictionary.Add("gz", "application/x-gzip");
            _dictionary.Add("h", "text/plain");
            _dictionary.Add("hdf", "application/x-hdf");
            _dictionary.Add("hlp", "application/winhlp");
            _dictionary.Add("hqx", "application/mac-binhex40");
            _dictionary.Add("hta", "application/hta");
            _dictionary.Add("htc", "text/x-component");
            _dictionary.Add("htm", "text/html");
            _dictionary.Add("html", "text/html");
            _dictionary.Add("htt", "text/webviewhtml");
            _dictionary.Add("ico", "image/x-icon");
            _dictionary.Add("ief", "image/ief");
            _dictionary.Add("iii", "application/x-iphone");
            _dictionary.Add("ins", "application/x-internet-signup");
            _dictionary.Add("isp", "application/x-internet-signup");
            _dictionary.Add("jfif", "image/pipeg");
            _dictionary.Add("jpe", "image/jpeg");
            _dictionary.Add("jpeg", "image/jpeg");
            _dictionary.Add("jpg", "image/jpeg");
            _dictionary.Add("js", "application/x-javascript");
            _dictionary.Add("latex", "application/x-latex");
            _dictionary.Add("lha", "application/octet-stream");
            _dictionary.Add("lsf", "video/x-la-asf");
            _dictionary.Add("lsx", "video/x-la-asf");
            _dictionary.Add("lzh", "application/octet-stream");
            _dictionary.Add("m13", "application/x-msmediaview");
            _dictionary.Add("m14", "application/x-msmediaview");
            _dictionary.Add("m3u", "audio/x-mpegurl");
            _dictionary.Add("man", "application/x-troff-man");
            _dictionary.Add("mdb", "application/x-msaccess");
            _dictionary.Add("me", "application/x-troff-me");
            _dictionary.Add("mht", "message/rfc822");
            _dictionary.Add("mhtml", "message/rfc822");
            _dictionary.Add("mid", "audio/mid");
            _dictionary.Add("mny", "application/x-msmoney");
            _dictionary.Add("mov", "video/quicktime");
            _dictionary.Add("movie", "video/x-sgi-movie");
            _dictionary.Add("mp2", "video/mpeg");
            _dictionary.Add("mp3", "audio/mpeg");
            _dictionary.Add("mpa", "video/mpeg");
            _dictionary.Add("mpe", "video/mpeg");
            _dictionary.Add("mpeg", "video/mpeg");
            _dictionary.Add("mpg", "video/mpeg");
            _dictionary.Add("mpp", "application/vnd.ms-project");
            _dictionary.Add("mpv2", "video/mpeg");
            _dictionary.Add("ms", "application/x-troff-ms");
            _dictionary.Add("mvb", "application/x-msmediaview");
            _dictionary.Add("nws", "message/rfc822");
            _dictionary.Add("oda", "application/oda");
            _dictionary.Add("p10", "application/pkcs10");
            _dictionary.Add("p12", "application/x-pkcs12");
            _dictionary.Add("p7b", "application/x-pkcs7-certificates");
            _dictionary.Add("p7c", "application/x-pkcs7-mime");
            _dictionary.Add("p7m", "application/x-pkcs7-mime");
            _dictionary.Add("p7r", "application/x-pkcs7-certreqresp");
            _dictionary.Add("p7s", "application/x-pkcs7-signature");
            _dictionary.Add("pbm", "image/x-portable-bitmap");
            _dictionary.Add("pdf", "application/pdf");
            _dictionary.Add("pfx", "application/x-pkcs12");
            _dictionary.Add("pgm", "image/x-portable-graymap");
            _dictionary.Add("pko", "application/ynd.ms-pkipko");
            _dictionary.Add("pma", "application/x-perfmon");
            _dictionary.Add("pmc", "application/x-perfmon");
            _dictionary.Add("pml", "application/x-perfmon");
            _dictionary.Add("pmr", "application/x-perfmon");
            _dictionary.Add("pmw", "application/x-perfmon");
            _dictionary.Add("pnm", "image/x-portable-anymap");
            _dictionary.Add("pot,", "application/vnd.ms-powerpoint");
            _dictionary.Add("ppm", "image/x-portable-pixmap");
            _dictionary.Add("pps", "application/vnd.ms-powerpoint");
            _dictionary.Add("ppt", "application/vnd.ms-powerpoint");
            _dictionary.Add("prf", "application/pics-rules");
            _dictionary.Add("ps", "application/postscript");
            _dictionary.Add("pub", "application/x-mspublisher");
            _dictionary.Add("qt", "video/quicktime");
            _dictionary.Add("ra", "audio/x-pn-realaudio");
            _dictionary.Add("ram", "audio/x-pn-realaudio");
            _dictionary.Add("ras", "image/x-cmu-raster");
            _dictionary.Add("rgb", "image/x-rgb");
            _dictionary.Add("rmi", "audio/mid");
            _dictionary.Add("roff", "application/x-troff");
            _dictionary.Add("rtf", "application/rtf");
            _dictionary.Add("rtx", "text/richtext");
            _dictionary.Add("scd", "application/x-msschedule");
            _dictionary.Add("sct", "text/scriptlet");
            _dictionary.Add("setpay", "application/set-payment-initiation");
            _dictionary.Add("setreg", "application/set-registration-initiation");
            _dictionary.Add("sh", "application/x-sh");
            _dictionary.Add("shar", "application/x-shar");
            _dictionary.Add("sit", "application/x-stuffit");
            _dictionary.Add("snd", "audio/basic");
            _dictionary.Add("spc", "application/x-pkcs7-certificates");
            _dictionary.Add("spl", "application/futuresplash");
            _dictionary.Add("src", "application/x-wais-source");
            _dictionary.Add("sst", "application/vnd.ms-pkicertstore");
            _dictionary.Add("stl", "application/vnd.ms-pkistl");
            _dictionary.Add("stm", "text/html");
            _dictionary.Add("svg", "image/svg+xml");
            _dictionary.Add("sv4cpio", "application/x-sv4cpio");
            _dictionary.Add("sv4crc", "application/x-sv4crc");
            _dictionary.Add("swf", "application/x-shockwave-flash");
            _dictionary.Add("t", "application/x-troff");
            _dictionary.Add("tar", "application/x-tar");
            _dictionary.Add("tcl", "application/x-tcl");
            _dictionary.Add("tex", "application/x-tex");
            _dictionary.Add("texi", "application/x-texinfo");
            _dictionary.Add("texinfo", "application/x-texinfo");
            _dictionary.Add("tgz", "application/x-compressed");
            _dictionary.Add("tif", "image/tiff");
            _dictionary.Add("tiff", "image/tiff");
            _dictionary.Add("tr", "application/x-troff");
            _dictionary.Add("trm", "application/x-msterminal");
            _dictionary.Add("tsv", "text/tab-separated-values");
            _dictionary.Add("txt", "text/plain");
            _dictionary.Add("uls", "text/iuls");
            _dictionary.Add("ustar", "application/x-ustar");
            _dictionary.Add("vcf", "text/x-vcard");
            _dictionary.Add("vrml", "x-world/x-vrml");
            _dictionary.Add("wav", "audio/x-wav");
            _dictionary.Add("wcm", "application/vnd.ms-works");
            _dictionary.Add("wdb", "application/vnd.ms-works");
            _dictionary.Add("wks", "application/vnd.ms-works");
            _dictionary.Add("wmf", "application/x-msmetafile");
            _dictionary.Add("wps", "application/vnd.ms-works");
            _dictionary.Add("wri", "application/x-mswrite");
            _dictionary.Add("wrl", "x-world/x-vrml");
            _dictionary.Add("wrz", "x-world/x-vrml");
            _dictionary.Add("xaf", "x-world/x-vrml");
            _dictionary.Add("xbm", "image/x-xbitmap");
            _dictionary.Add("xla", "application/vnd.ms-excel");
            _dictionary.Add("xlc", "application/vnd.ms-excel");
            _dictionary.Add("xlm", "application/vnd.ms-excel");
            _dictionary.Add("xls", "application/vnd.ms-excel");
            _dictionary.Add("xlt", "application/vnd.ms-excel");
            _dictionary.Add("xlw", "application/vnd.ms-excel");
            _dictionary.Add("xof", "x-world/x-vrml");
            _dictionary.Add("xpm", "image/x-xpixmap");
            _dictionary.Add("xwd", "image/x-xwindowdump");
            _dictionary.Add("xml", "text/xml");
            _dictionary.Add("z", "application/x-compress");
            _dictionary.Add("zip", "application/zip");
        }

        /// <summary>
        /// Ottiene il tipo mime associato a un estensione di file.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetByFileExtension(string extension)
        {
            string key = (extension.StartsWith(".") ? extension.Substring(1) : extension).ToLower();
            if (Dictionary.ContainsKey(key))
            {
                return _dictionary[key];
            }
            return _dictionary[DEFAULT_KEY];
        }
    }
}