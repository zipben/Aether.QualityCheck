using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aether.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Aether.Models.ErisClient;

namespace Aether.Extensions.Models.Tests
{
    [TestClass()]
    public class IdentifiersRootExtensionsTests
    {
        [TestMethod]
        public void AggregateCorrelatedIdentifierByIdentifierTest()
        {
            IdentifiersRoot identifiersRoot = new IdentifiersRoot
            {
                Identifiers = new List<CorrelatedIdentifierResponseModel> 
                {
                   new CorrelatedIdentifierResponseModel
                   {
                       CorrelatedIdentifiers= new Dictionary<string, List<string>>
                       {
                           {
                               "GCID", new List<string>{"gcid1", "gcid2"}
                           }
                       }
                   },
                   new CorrelatedIdentifierResponseModel
                   {
                       CorrelatedIdentifiers= new Dictionary<string, List<string>>
                       {
                           {
                               "gcid", new List<string>{"gcid3", "gcid4"}
                           }
                       }
                   }
                }
            };

          var stuff = identifiersRoot.AggregateCorrelatedIdentifierByIdentifier(Enums.IdentifierType.GCID);
        }
    }
}