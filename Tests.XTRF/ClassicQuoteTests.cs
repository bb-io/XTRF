using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.XTRF.Classic.Actions;
using Apps.XTRF.Classic.Models.Requests.ClassicQuote;
using Apps.XTRF.Shared.Models.Identifiers;
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

        [TestMethod]
        public async Task AddReceivableToQuote_ValidInput_ReturnsResult()
        {
            var action = new ClassicQuoteActions(InvocationContext, FileManager);
            var response = await action.AddReceivableToQuote(new QuoteIdentifier()
            {
                QuoteId = "143"
            }, new AddQuoteReceivableRequest { CalculationUnitId="1", Units=1 });

            Assert.IsNotNull(response);
        }
    }
}
