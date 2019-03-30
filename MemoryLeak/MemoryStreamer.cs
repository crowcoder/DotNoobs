using System.IO;

namespace MemoryLeak
{
    internal class MemoryStreamer
    {
        internal void DoSomething()
        {
            byte[] bytes = new byte[1048576];
            MemoryStream ms = new MemoryStream(bytes);           
        }

        internal void DisposeSomething()
        {
            byte[] bytes = new byte[1048576];
            MemoryStream ms = new MemoryStream(bytes);
            ms.Dispose();
        }
    }
}
