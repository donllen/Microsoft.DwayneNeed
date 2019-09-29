using System;

namespace Microsoft.DwayneNeed.Utilities
{
    /// <summary>
    ///     The CommandLineParameter attribute can be used to indicate how
    ///     properties of a type are mapped to command line parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandLineParameterAttribute : Attribute
    {
        public bool IsRequired;

        public string Name;
        public string ShortDescription;

        public CommandLineParameterAttribute(string name)
        {
            Name = name;
        }
    }
}