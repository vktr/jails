using System;

namespace Jails
{
    [Serializable]
    public class InMemoryAssembly
    {
        private readonly byte[] _data;

        public InMemoryAssembly(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            _data = data;
        }

        public byte[] Data
        {
            get { return _data; }
        }
    }
}
