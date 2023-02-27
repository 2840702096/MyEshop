using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Core.Generators
{
    public class CodeGenerator
    {
        public static string UniqCodeGenerator()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
