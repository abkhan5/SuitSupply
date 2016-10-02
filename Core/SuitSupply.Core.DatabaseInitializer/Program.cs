using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.Domain.Product.ReadModel.Implementation;

namespace SuitSupply.Core.DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            new InitializeSuitSupply();
        }
    }
}
