using Finances.Domain;
using Finances.Models.Account;

using Mapster;

namespace Finances.Services.Mappings;

public class AccountMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AccountDto, Account>();
    }
}
