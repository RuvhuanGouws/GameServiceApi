using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Domain.ValueObjects
{
    /// <summary>
    /// Represents a Steam ID as a value object in the domain.
    /// </summary>
    public record SteamId
    {
        /// <summary>
        /// Gets the value of the Steam ID.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SteamId"/> class.
        /// </summary>
        /// <param name="value">The string value of the Steam ID.</param>
        /// <exception cref="ArgumentException">Thrown when the provided value does not conform to the expected Steam ID format.</exception>
        public SteamId(string value)
        {
            if (!IsValidSteamId(value))
            {
                throw new ArgumentException("Invalid Steam ID format.");
            }
            Value = value;
        }

        /// <summary>
        /// Validates the format of the Steam ID.
        /// </summary>
        /// <param name="value">The Steam ID to validate.</param>
        /// <returns>true if the Steam ID is valid; otherwise, false.</returns>
        private bool IsValidSteamId(string value)
        {
            var isNumeric = long.TryParse(value, out long _);
            return isNumeric && value.Length >= 17;
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="SteamId"/> object to a string.
        /// </summary>
        /// <param name="steamId">The <see cref="SteamId"/> object.</param>
        /// <returns>The string value of the Steam ID.</returns>
        public static implicit operator string(SteamId steamId) => steamId.Value;

        /// <summary>
        /// Defines an implicit conversion of a string to a <see cref="SteamId"/> object.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>A new <see cref="SteamId"/> object.</returns>
        public static implicit operator SteamId(string value) => new SteamId(value);
    }
}
