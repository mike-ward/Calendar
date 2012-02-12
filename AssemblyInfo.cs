// Copyright 2005 Blue Onion Software, All rights reserved
//
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("Calendar")]
[assembly: AssemblyDescription("A fast, perpetual calendar")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Blue Onion Software")]
[assembly: AssemblyProduct("Calendar")]
[assembly: AssemblyCopyright("(C)Copyright 2008 Blue Onion Sofware, All rights reserved")]
[assembly: AssemblyTrademark("Calendar")]
[assembly: AssemblyCulture("")]

[assembly: System.CLSCompliant(true)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: IsolatedStorageFilePermission(SecurityAction.RequestMinimum, UserQuota=1048576)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, UnmanagedCode=false)]
[assembly: FileIOPermission(SecurityAction.RequestMinimum, Unrestricted=true)]

[assembly: BlueOnion.ProductId("300022405")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("2.2.2.0")]

[assembly: AssemblyFileVersionAttribute("2.2.2.0")]
