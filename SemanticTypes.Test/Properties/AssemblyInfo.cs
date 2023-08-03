using System;
using System.Reflection;
using System.Runtime.InteropServices;

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b9162660-b530-49ea-8a34-5b62609bc4dc")]

// Get the compiler to create a strong named assembly, using the key pair in this file.
// Note that they key file gets checked into Github, as per
// http://stackoverflow.com/questions/36141302/why-is-it-recommended-to-include-the-private-key-used-for-assembly-signing-in-op
[assembly: AssemblyKeyFileAttribute("../semantictypes.strongname.snk")]
[assembly: CLSCompliant(true)]
