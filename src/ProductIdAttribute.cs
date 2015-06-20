using System;

namespace BlueOnion
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class ProductIdAttribute : Attribute
    {
        public ProductIdAttribute(string productId)
        {
            ProductId = productId;
        }

        public string ProductId { get; }
    }
}