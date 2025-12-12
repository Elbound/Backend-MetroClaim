using MetroClaim.Api.DTOs.User;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Data;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;

namespace MetroClaim.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashHandler _hashHandler;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserContext _userContext;

    public UserService(IUserRepository userRepository, IAccountRepository accountRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IHashHandler hashHandler, IUserRoleRepository userRoleRepository, IUserContext userContext)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _hashHandler = hashHandler;
        _userRoleRepository = userRoleRepository;
        _userContext = userContext;
    }

    public async Task<IEnumerable<UserGetResponseDto>> GetAllUserAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersWithDetailsAsync(cancellationToken);
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
                u.UpdatedAt,
                u.UserRoles.Select(ur => ur.Role?.Name ?? "Unknown").ToList()
            ));
    }

    public async Task<UserGetResponseDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithDetailsAsync(id, cancellationToken);

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
            user.UpdatedAt,
            user.UserRoles.Select(ur => ur.Role?.Name ?? "Unknown").ToList()
        );
    }

    public async Task RegisterUserAsync(UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(requestDto.Email, cancellationToken);
        if (existingAccount is not null)
            throw new InvalidOperationException($"Email {requestDto.Email} is already registered.");

        foreach (var roleId in requestDto.RoleIds)
        {
            var roleCheck = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
            if (roleCheck is null)
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
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
            CreatedAt = now,
            UpdatedAt = now
        };

        var newAccount = new Account
        {
            Id = Guid.NewGuid(),
            UserId = newUserId,
            Email = requestDto.Email,
            Password = _hashHandler.GenerateHash(requestDto.Password),
            IsActive = true,
            IsUsed = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        var userRoles = requestDto.RoleIds.Select(roleId => new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = newUserId,
            RoleId = roleId,
            CreatedAt = now,
            UpdatedAt = now
        }).ToList();

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userRepository.CreateAsync(newUser, cancellationToken);
            await _accountRepository.CreateAsync(newAccount, cancellationToken);

            foreach (var ur in userRoles)
                await _userRoleRepository.CreateAsync(ur, cancellationToken);

        }, cancellationToken);
    }


    public async Task UpdateUserAsync(Guid id, UserUpdateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithDetailsAsync(id, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        foreach (var roleId in requestDto.RoleIds)
        {
            var roleCheck = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
            if (roleCheck is null)
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        }

        user.EmployeeId = requestDto.EmployeeId;
        user.FullName = requestDto.FullName;
        user.Salary = requestDto.Salary;
        user.DueReimbursement = requestDto.DueReimbursement;
        user.BankAccountNumber = requestDto.BankAccountNumber;
        user.ManagerId = requestDto.ManagerId;
        user.UpdatedAt = DateTime.UtcNow;

        var now = DateTime.UtcNow;
        var newRolesToInsert = requestDto.RoleIds.Select(roleId => new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            RoleId = roleId,
            CreatedAt = now,
            UpdatedAt = now
        }).ToList();

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userRepository.UpdateAsync(user);

            if (user.UserRoles is not null && user.UserRoles.Any())
            {
                foreach (var existingRole in user.UserRoles.ToList())
                    await _userRoleRepository.DeleteAsync(existingRole);
            }

            foreach (var newRole in newRolesToInsert)
                await _userRoleRepository.CreateAsync(newRole, cancellationToken);

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
    public async Task<IEnumerable<UserGetResponseDto>> GetMySubordinatesAsync(CancellationToken cancellationToken)
    {
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId == Guid.Empty) throw new UnauthorizedAccessException("User is not authenticated.");

        if (!_userContext.IsInRole("Manager"))
        {
            throw new UnauthorizedAccessException("Only managers can view their subordinates.");
        }

        var subordinates = await _userRepository.GetByManagerIdAsync(currentUserId, cancellationToken);

        return subordinates.Select(u => new UserGetResponseDto(
                u.Id,
                u.EmployeeId,
                u.FullName,
                u.Salary,
                u.DueReimbursement,
                u.BankAccountNumber!,
                u.ManagerId,
                u.CreatedAt,
                u.UpdatedAt,
                u.UserRoles.Select(ur => ur.Role?.Name ?? "Unknown").ToList()
            ));
    }
}
