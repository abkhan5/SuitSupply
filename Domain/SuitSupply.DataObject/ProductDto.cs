#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace SuitSupply.DataObject
{
    public class ProductDto
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ProductCode { get; set; }

        public string ProductName { get; set; }

        public IEnumerable<byte[]> ProductImages { get; set; }

        public byte[] RecordVersion { get; set; }


        public override string ToString()
        {
            var message= "Product Name = "+ProductName+" Product Code = "+ProductCode+" with ID "+Id;
            if (ProductImages!=null && ProductImages.Any())
            {
                message += " Total Photos " + ProductImages.Count();
            }

            return message;
        }
    }

}
