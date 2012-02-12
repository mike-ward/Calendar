// Copyright 2005 Blue Onion Software, All rights reserved.
//
using System;

namespace BlueOnion
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class ProductIdAttribute : System.Attribute
	{
        private readonly string productId;

		public ProductIdAttribute(string productId)
		{
            this.productId = productId;
		}

        public string ProductId
        {
            get { return this.productId; }
        }
	}
}
