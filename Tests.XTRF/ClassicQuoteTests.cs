using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Classic.Actions;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class ClassicQuoteTests :TestBase
    {
        [TestMethod]
        public async Task GetQuote_ValidInput_ReturnsResult()
        {
            var action = new ClassicQuoteActions(InvocationContext, FileManager);
            var response = await action.GetQuote(new Apps.XTRF.Shared.Models.Identifiers.QuoteIdentifier()
            {
                QuoteId = "fakeId"
            });

            Assert.IsNotNull(response);
        }
    }
}
