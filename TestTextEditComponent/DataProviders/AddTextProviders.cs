using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TestTextEditComponent.DataProviders
{
    public class AddTextProviders
    {
        public static IEnumerable AddOneLineProvider
        {
            get { yield return new TestCaseData("alkfas;mfalsfnm asdmna ;psnd"); }
        }
        
        public static IEnumerable AddTextProvider
        {
            get { yield return new TestCaseData(new List<string> {"alkfas;mfalsfnm asdmna ;psnd"}); }
        }
    }
    
}