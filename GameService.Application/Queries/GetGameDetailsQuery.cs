using GameService.Application.Boundaries;
using GameService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Queries
{
    public class GetGameDetailsQuery : IRequest<GetGameDetailsOutput>
    {
        public AppId AppId { get; }

        public GetGameDetailsQuery(AppId appId)
        {
            AppId = appId;
        }
    }
}
