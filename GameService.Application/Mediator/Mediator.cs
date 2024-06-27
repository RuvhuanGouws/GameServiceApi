using GameService.Application.Handlers;
using GameService.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.Mediator
{
    /// <summary>
    /// Represents a mediator to handle requests and dispatch them to their respective handlers.
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Sends a request to the appropriate handler.
        /// </summary>
        /// <typeparam name="TResponse">The response type the handler will return.</typeparam>
        /// <param name="request">The request to handle.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the handler response.</returns>
        /// <exception cref="Exception">Thrown if no handler for the request is found.</exception>
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new Exception($"Handler for {request.GetType().Name} not found");
            }

            var method = handlerType.GetMethod("Handle");
            return await (Task<TResponse>)method.Invoke(handler, new object[] { request });
        }
    }
}
