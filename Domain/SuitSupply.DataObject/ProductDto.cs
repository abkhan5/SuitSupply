#region Namespace
using System;
using System.Collections.Generic;

#endregion
namespace SuitSupply.DataObject
{
    public class ProductDto
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProductName { get; set; }

        public IEnumerable<byte[]> ProductImages { get; set; }


    }

}
