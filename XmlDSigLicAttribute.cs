// Copyright 2005 Blue Onion Software, All rights reserved.
//
using System;

namespace BlueOnion
{
    [AttributeUsage(AttributeTargets.Class)]
	public sealed class XmlDSigLicAttribute : System.Attribute
	{
        private readonly string key = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPndiSGZRYTYvVWZIVkVFelhaYU5WUmhjdkgyUW16NVhyL2tTSzU3N25UdEI1U3Z1emU1RjRxNDZwL0UyS1o4MEpVQXVzY2x4ZHFwbEtuMWkwNll1VTNLdlN1ejRKMVZsVDFZbG1OYmhMY20rZTZhVnlneVcyU0tZd2Jka1JtZGlSNnRjK2tTU09tYitBclZhNURpVDRWbGNlR3VQdkIxZkJRZ01LRmFXd2hWaz08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
        private readonly string file = @"\Calendar.lic";

		public XmlDSigLicAttribute()
		{
		}

        public string Key
        {
            get { return this.key; }
        }

        public string File
        {
            get { return this.file; }
        }
	}
}
