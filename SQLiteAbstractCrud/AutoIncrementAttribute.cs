
using System.Diagnostics.CodeAnalysis;
using System;

namespace SQLiteAbstractCrud
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Property)] 
    public class AutoIncrementAttribute : Attribute
    {

    }
}