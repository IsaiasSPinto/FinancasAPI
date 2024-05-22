using Finances.Domain;
using Finances.Models.Transactions;

using Mapster;

namespace Finances.Services.Mappings;

public class TransactionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTransactionDto, Transaction>();
        config.NewConfig<TransactionDto, Transaction>();
        config.NewConfig<UpdateTransactionDto, Transaction>();
    }
}
