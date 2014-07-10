using System;

namespace Jails.Extensions.Roslyn
{
    public sealed class CSharpSource
    {
        private readonly string _sourceCode;

        public CSharpSource(string sourceCode)
        {
            if (sourceCode == null) throw new ArgumentNullException("sourceCode");
            _sourceCode = sourceCode;
        }

        public string SourceCode
        {
            get { return _sourceCode; }
        }
    }
}
