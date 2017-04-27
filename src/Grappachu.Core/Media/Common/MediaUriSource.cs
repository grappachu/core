using System;

namespace Grappachu.Core.Media.Common
{
    /// <summary>
    ///     Defines a generic media content wich can be identified by a url
    /// </summary>
    public class MediaUriSource : IMediaSource
    {
        /// <summary>
        ///     Creates a new instance <see cref="MediaUriSource" /> from a path
        /// </summary>
        /// <param name="uriPath"></param>
        public MediaUriSource(string uriPath)
        {
            Uri = new Uri(uriPath, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        ///     Creates a new instance <see cref="MediaUriSource" /> from a uri
        /// </summary>
        /// <param name="uri"></param>
        public MediaUriSource(Uri uri)
        {
            Uri = uri;
        }

        /// <summary>
        ///     Gets the uri used to create this instance of <see cref="MediaUriSource" />
        /// </summary>
        public Uri Uri { get; }
    }
}