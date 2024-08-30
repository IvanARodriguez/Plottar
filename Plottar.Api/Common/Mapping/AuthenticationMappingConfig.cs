namespace Plottar.Api.Mapping;

using Mapster;
using Plottar.Application.Commands.Register;
using Plottar.Application.Common;
using Plottar.Application.Queries.Login;
using Plottar.Contracts.Authentication;

public class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {

    config.NewConfig<RegisterRequest, RegisterCommand>();
    config.NewConfig<LoginRequest, LoginQuery>();
    config.NewConfig<AuthenticationResult, AuthenticationResponse>()
    .Map(dest => dest, src => src.User);
  }
}
