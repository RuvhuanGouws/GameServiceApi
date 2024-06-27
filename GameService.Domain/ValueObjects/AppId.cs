using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Domain.ValueObjects
{
    /// <summary>
    /// Represents an application identifier as a value object in the domain.
    /// </summary>
    public record AppId
    {
        /// <summary>
        /// Gets the value of the application identifier.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppId"/> class.
        /// </summary>
        /// <param name="value">The integer value of the application identifier.</param>
        /// <exception cref="ArgumentException">Thrown when the provided value is not a positive integer.</exception>
        public AppId(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("App ID must be a positive integer.");
            }
            Value = value;
        }

        /// <summary>
        /// Defines an implicit conversion of an <see cref="AppId"/> object to an integer.
        /// </summary>
        /// <param name="appId">The <see cref="AppId"/> object.</param>
        /// <returns>The integer value of the application identifier.</returns>
        public static implicit operator int(AppId appId) => appId.Value;

        /// <summary>
        /// Defines an implicit conversion of an integer to an <see cref="AppId"/> object.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>A new <see cref="AppId"/> object.</returns>
        public static implicit operator AppId(int value) => new AppId(value);
    }

}
