using Apps.XTRF.Smart.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTRF.Base;

namespace Tests.XTRF
{
    [TestClass]
    public class SmartQuoteActionTests : TestBase
    {
        [TestMethod]
        public async Task GetSmartQuote_ValidInput_ReturnsResult()
        {
            var action = new SmartQuoteActions(InvocationContext, FileManager);
            var response = await action.GetQuote(new Apps.XTRF.Shared.Models.Identifiers.QuoteIdentifier()
            {
                QuoteId = "K7NPUOBE2VAA3A2SDFYBZ2BK3I"
            },false);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task AddReceivableToQuote_ValidInput_ReturnsResult()
        {
            var action = new SmartQuoteActions(InvocationContext, FileManager);
            var response = await action.AddReceivableToQuote(new Apps.XTRF.Shared.Models.Identifiers.QuoteIdentifier()
            {
                QuoteId = "K7NPUOBE2VAA3A2SDFYBZ2BK3I"
            }, new Apps.XTRF.Classic.Models.Requests.ClassicQuote.AddQuoteReceivableRequest { CalculationUnitId="1", Units=1 });
            Assert.IsNotNull(response);
        }
    }
}
