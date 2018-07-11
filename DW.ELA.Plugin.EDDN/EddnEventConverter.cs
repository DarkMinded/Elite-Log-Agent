﻿using System;
using System.Collections.Generic;
using System.Linq;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Events;
using Newtonsoft.Json.Linq;
using NLog;
using Utility;

namespace DW.ELA.Plugin.EDDN
{
    public class EddnEventConverter : IEventConverter<EddnEvent>
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public string UploaderID = "Unknown";

        private IDictionary<string, string> CreateHeader()
        {
            return new Dictionary<string, string>
            {
                ["uploaderID"] = UploaderID,
                ["softwareName"] = AppInfo.Name,
                ["softwareVersion"] = AppInfo.Version
            };
        }

        public IEnumerable<EddnEvent> Convert(LogEvent @event)
        {
            try
            {
                switch (@event)
                {
                    // Travel events
                    //case Docked d:
                    case FsdJump f: 
                    //case Scan s:
                    case Location l:
                        return MakeJournalEvent(@event);

                    // Market events
                    case Market e: return ConvertMarketEvent(e);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error converting message");
            }
            return Enumerable.Empty<EddnEvent>();
        }

        private IEnumerable<EddnEvent> ConvertMarketEvent(Market e)
        {
            if (e.Items == null)
                yield break;

            var commodities = e.Items
                .Where(i => i.Category != "NonMarketable")
                .Where(i => string.IsNullOrEmpty(i.Legality))
                .ToArray();

            var @event = new CommodityEvent()
            {
                Header = CreateHeader(),
                Message = new CommodityMessage()
                {
                    Timestamp = e.Timestamp,
                    MarketId = e.MarketId,
                    StationName = e.StationName,
                    SystemName = e.StarSystem,
                    Commodities = commodities.Select(ConvertCommodity).ToArray()
                }
            };
            yield return @event;
        }

        private Commodity ConvertCommodity(MarketItem arg)
        {
            return new Commodity()
            {
                BuyPrice = arg.BuyPrice,
                Demand = arg.Demand,
                DemandBracket = arg.DemandBracket,
                MeanPrice = arg.MeanPrice,
                Name = arg.Name.Replace("$","").Replace("_name;",""),
                SellPrice = arg.SellPrice,
                Stock = arg.Stock,
                StockBracket = arg.StockBracket
            };
        }

        private IEnumerable<EddnEvent> MakeJournalEvent(LogEvent e) { yield return new JournalEvent { Header = CreateHeader(), Message = Strip(e.Raw) }; }

        private JObject Strip(JObject raw)
        {
            raw = (JObject)raw.DeepClone();
            var attributesToRemove = new List<string>() {
                "CockpitBreach",
                "BoostUsed",
                "FuelLevel",
                "FuelUsed",
                "JumpDist",
                "Latitude",
                "Longitude"
            };

            foreach (var attribute in raw)
                if (attribute.Key.EndsWith("_Localised"))
                    attributesToRemove.Add(attribute.Key);

            foreach (var key in attributesToRemove)
                raw.Remove(key);

            return raw;
        }
    }
}