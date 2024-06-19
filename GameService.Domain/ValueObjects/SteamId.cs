using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Domain.ValueObjects
{
    public record SteamId
    {
        public string Value { get; }

        public SteamId(string value)
        {
            if (!IsValidSteamId(value))
            {
                throw new ArgumentException("Invalid Steam ID format.");
            }
            Value = value;
        }

        private bool IsValidSteamId(string value)
        {
            var isNumeric = long.TryParse(value, out long _);
            return isNumeric && value.Length >= 17;
        }

        public static implicit operator string(SteamId steamId) => steamId.Value;
        public static implicit operator SteamId(string value) => new SteamId(value);
    }
}
