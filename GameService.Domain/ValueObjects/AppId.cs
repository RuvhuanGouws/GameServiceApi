using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Domain.ValueObjects
{
    public record AppId
    {
        public int Value { get; }

        public AppId(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("App ID must be a positive integer.");
            }
            Value = value;
        }

        public static implicit operator int(AppId appId) => appId.Value;
        public static implicit operator AppId(int value) => new AppId(value);
    }
}
