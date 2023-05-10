﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.XTRF.Webhooks.Payloads
{
    public class QuoteCreatedPayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("internal id")]
        public string InternalId { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("client legal name")]
        public string ClientLegalName { get; set; }

        [JsonProperty("client price profile")]
        public string ClientPriceProfile { get; set; }

        [JsonProperty("instructions from customer")]
        public string InstructionsFromCustomer { get; set; }

        [JsonProperty("quote manager")]
        public string QuoteManager { get; set; }

        [JsonProperty("sales person")]
        public string SalesPerson { get; set; }

        [JsonProperty("offer expiry")]
        public DateTime OfferExpiry { get; set; }

        [JsonProperty("estimated delivery date and time")]
        public DateTime EstimatedDeliveryDateAndTime { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("created on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("start date and time")]
        public DateTime StartDateAndTime { get; set; }
    }
}