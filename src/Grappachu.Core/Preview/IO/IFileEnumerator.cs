using System.Collections.Generic;
using System.IO;

namespace Grappachu.Core.Preview.IO
{
    /// <summary>
    /// Rappresenta un enumeratore di file
    /// </summary>
    public interface IFileEnumerator : IEnumerable<FileInfo>
    {
    }
}