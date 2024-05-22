using ErrorOr;
using Finances.Domain;
using Finances.Models.Transactions;
using Finances.Persistence.Contracts;
using Finances.Persistence.Contracts.Repositories;
using Finances.Services.Contracts;
using Finances.Services.Validators;

using MapsterMapper;

namespace Finances.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionRepository repository, IUnitOfWork uow, IMapper mapper, IAccountRepository accountRepository)
    {
        _repository = repository;
        _unitOfWork = uow;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }


    public async Task<List<TransactionDto>> GetAllUserTransactionsAsync(int userId)
    {
        return _mapper.Map<List<TransactionDto>>(await _repository.GetTransactionsAsync(t => t.UserId == userId));
    }

    public async Task<TransactionDto?> GetTransactionAsync(int id)
    {
        var transaction = await _repository.GetTransactionAsync(id);

        if (transaction is null)
        {
            return null;
        }

        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<ErrorOr<int>> CreateTransactionAsync(CreateTransactionDto transaction)
    {
        CreateTransactionValidator validator = new();

        var validationResult = await validator.ValidateAsync(transaction);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.Select(e => Error.Validation(e.ErrorMessage)).ToList();
        }

        var transactionEntity = _mapper.Map<Transaction>(transaction);
        //Todo : Get the user id from the logged in user
        transactionEntity.UserId = 1;

        await _repository.CreateTransactionAsync(transactionEntity);

        await _accountRepository.UpdateAccountBalanceAsync(transactionEntity.AccountId, transactionEntity.Amount);

        await _unitOfWork.CommitAsync();

        return transactionEntity.Id;
    }

    public async Task<ErrorOr<TransactionDto>> UpdateTransactionAsync(UpdateTransactionDto transaction)
    {
        UpdateTransactionValidator validator = new();

        var validationResult = await validator.ValidateAsync(transaction);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.Select(e => Error.Validation(e.ErrorMessage)).ToList();
        }

        var transactionEntity = _mapper.Map<Transaction>(transaction);
        transactionEntity.UserId = 1;

        _repository.UpdateTransactionAsync(transactionEntity);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<TransactionDto>(transactionEntity);
    }

    public async Task<ErrorOr<bool>> DeleteTransactionAsync(int id)
    {
        await _repository.DeleteTransactionAsync(id);

        return await _unitOfWork.CommitAsync() > 0;
    }
}
