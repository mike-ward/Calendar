using System;

namespace BlueOnion
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class XmlDSigLicAttribute : Attribute
    {
        public string Key { get; } = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPndiSGZRYTYvVWZIVkVFelhaYU5WUmhjdkgyUW16NVhyL2tTSzU3N25UdEI1U3Z1emU1RjRxNDZwL0UyS1o4MEpVQXVzY2x4ZHFwbEtuMWkwNll1VTNLdlN1ejRKMVZsVDFZbG1OYmhMY20rZTZhVnlneVcyU0tZd2Jka1JtZGlSNnRjK2tTU09tYitBclZhNURpVDRWbGNlR3VQdkIxZkJRZ01LRmFXd2hWaz08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";

        public string File { get; } = @"\Calendar.lic";
    }
}