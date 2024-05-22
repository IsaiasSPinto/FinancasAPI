using ErrorOr;

using Finances.Domain;
using Finances.Models.Account;
using Finances.Persistence.Contracts;
using Finances.Persistence.Contracts.Repositories;
using Finances.Services.Contracts;
using Finances.Services.Validators.Account;

using MapsterMapper;

namespace Finances.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository accountRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountDto>> GetAccountByIdAsync(int id)
    {

        var account = await _accountRepository.GetAccountByIdAsync(id);

        if (account is null)
        {
            return Error.NotFound("Account not Found !");
        }

        return _mapper.Map<AccountDto>(account);
    }

    public async Task<List<AccountDto>> GetAccountsByUserIdAsync(int userId)
    {
        return _mapper.Map<List<AccountDto>>(await _accountRepository.GetAccountsByUserIdAsync(userId));
    }

    public async Task<ErrorOr<AccountDto>> CreateAccountAsync(CreateAccountDto createAccountDto)
    {
        var validator = new CreateAccountValidator();

        var result = await validator.ValidateAsync(createAccountDto);

        if (!result.IsValid)
        {
            return result.Errors.Select(x => Error.Validation( x.ErrorCode, x.ErrorMessage)).ToList();
        }

        var account = Account.Create(createAccountDto.Name,1);

        await _accountRepository.CreateAccountAsync(account);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<AccountDto>(account);
    }

    public async Task<ErrorOr<AccountDto>> UpdateAccountAsync(UpdateAccountDto updateAccountDto)
    {
        var account = await _accountRepository.GetAccountByIdAsync(updateAccountDto.Id);

        if (account is null)
        {
            return Error.NotFound("Account not Found !");
        }

        var validator = new UpdateAccountValidator();

        var result = await validator.ValidateAsync(updateAccountDto);

        if (!result.IsValid)
        {
            return result.Errors.Select(x => Error.Validation(x.ErrorCode, x.ErrorMessage)).ToList();
        }

        account.ChangeName(updateAccountDto.Name);

        _accountRepository.UpdateAccountAsync(account);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<AccountDto>(account);
    }

    public async Task DeleteAccountAsync(int id)
    {
        await _accountRepository.DeleteAccountAsync(id);

        await _unitOfWork.CommitAsync();
    }
}
