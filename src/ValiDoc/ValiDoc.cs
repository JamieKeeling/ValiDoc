using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace ValiDoc.Core
{
    public class ValiDoc
    {
        public IEnumerable<string> Rules(object validator)
        {
            return Enumerable.Empty<string>();
        }
    }
}
