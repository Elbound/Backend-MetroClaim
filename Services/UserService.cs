using MetroClaim.Api.DTOs.User;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;

namespace MetroClaim.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashHandler _hashHandler;

    public UserService(IUserRepository userRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork, IHashHandler hashHandler)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _hashHandler = hashHandler;
    }

    public async Task<IEnumerable<UserGetResponseDto>> GetAllUserAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        if (users is null)
        {
            throw new NullReferenceException("users not found");
        }

        return users.Select(u => new UserGetResponseDto(
                u.Id,
                u.EmployeeId,
                u.FullName,
                u.Salary,
                u.DueReimbursement,
                u.BankAccountNumber!,
                u.ManagerId,
                u.CreatedAt,
                u.UpdatedAt
            ));
    }

    public async Task<UserGetResponseDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            throw new NullReferenceException($"User with ID {id} not found.");
        }

        return new UserGetResponseDto(
            user.Id,
            user.EmployeeId,
            user.FullName,
            user.Salary,
            user.DueReimbursement,
            user.BankAccountNumber,
            user.ManagerId,
            user.CreatedAt,
            user.UpdatedAt
        );
    }

    public async Task RegisterUserAsync(UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(requestDto.Email, cancellationToken);
        if (existingAccount is not null)
        {
            throw new InvalidOperationException($"Email {requestDto.Email} is already registered.");
        }
        var newUserId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var newUser = new User
        {
            Id = newUserId,
            EmployeeId = requestDto.EmployeeId,
            FullName = requestDto.FullName,
            Salary = requestDto.Salary,
            DueReimbursement = requestDto.DueReimbursement,
            BankAccountNumber = requestDto.BankAccountNumber,
            ManagerId = requestDto.ManagerId,
        };

        var newAccount = new Account
        {
            Id = Guid.NewGuid(),
            UserId = newUserId,
            Email = requestDto.Email,
            Password = _hashHandler.GenerateHash(requestDto.Password),
            Otp = null,
            Expired = default,
            IsActive = true,
            IsUsed = false,
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userRepository.CreateAsync(newUser, cancellationToken);
            await _accountRepository.CreateAsync(newAccount, cancellationToken);
        }, cancellationToken);
    }

    public async Task UpdateUserAsync(Guid id, UserUpdateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            throw new NullReferenceException($"User with ID {id} not found.");
        }

        user.EmployeeId = requestDto.EmployeeId;
        user.FullName = requestDto.FullName;
        user.Salary = requestDto.Salary;
        user.DueReimbursement = requestDto.DueReimbursement;
        user.BankAccountNumber = requestDto.BankAccountNumber;
        user.ManagerId = requestDto.ManagerId;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userRepository.UpdateAsync(user);
        }, cancellationToken);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            throw new NullReferenceException($"User with ID {id} not found.");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userRepository.DeleteAsync(user);
        }, cancellationToken);
    }
}
