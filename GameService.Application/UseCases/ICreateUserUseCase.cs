using GameService.Application.Boundaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Application.UseCases
{
    public interface ICreateUserUseCase
    {
        Task Execute(CreateUserInput input);
    }
}
