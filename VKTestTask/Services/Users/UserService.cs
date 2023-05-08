using VKTestTask.Domain;
using VKTestTask.Domain.Dto;
using VKTestTask.Repositories.Users;
using VKTestTask.Services.Delay;
using VKTestTask.Services.Reservation;
using VKTestTask.Services.Time;
using VKTestTask.Services.Users.Exceptions;

namespace VKTestTask.Services.Users;

public class UserService : IUserService
{
    private readonly ILoginReservationService _loginReservationService;
    private readonly IUserRepository _userRepository;
    private readonly ITimeService _timeService;
    private readonly IDelayTimeService _delayTimeService;

    public UserService(
        ILoginReservationService loginReservationService,
        IUserRepository userRepository,
        ITimeService timeService,
        IDelayTimeService delayTimeService
        )
    {
        _loginReservationService = loginReservationService;
        _userRepository = userRepository;
        _timeService = timeService;
        _delayTimeService = delayTimeService;
    }

    public async Task<User> Get(int id)
    {
        var user = await _userRepository.TryGetById(id);

        if (user is null)
            throw new UserNotFoundException(id);

        return user;
    }

    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public Task<List<User>> GetFromPage(Page page)
    {
        return _userRepository.Take(page.Offset, page.Size);
    }

    public async Task<User> Create(UserCreationModel user)
    {
        if (_loginReservationService.IsReserved(user.Login))
        {
            throw new LoginReservedException(user.Login);
        }
        _loginReservationService.Reserve(user.Login);

        await Task.Delay(_delayTimeService.GetDelay()); // Required delay


        if (await _userRepository.IsLoginExist(user.Login))
        {
            throw new LoginAlreadyExistException(user.Login);
        }
        if (user.GetGroup() == GroupCode.Admin)
        {
            var anyAdmin = await _userRepository.IsAdminExist();
            if (anyAdmin)
            {
                throw new AdminAlreadyExistException();
            }
        }

        var newUser = new User
        {
            Login = user.Login,
            Password = user.Password,
            CreatedDate = _timeService.GetTime(),
            UserState = new UserState { Code = StateCode.Active, Description = "Created" },
            UserGroup = new UserGroup { Code = user.GetGroup(), Description = "Created" }
        };
        var createdUser = await _userRepository.Add(newUser);

        _loginReservationService.Cancel(user.Login);

        return createdUser;
    }

    public async Task<User> Remove(int id)
    {
        var userFromDb = await _userRepository.TryGetById(id);
        if (userFromDb is null)
            throw new UserNotFoundException(id);

        userFromDb.UserState.Code = StateCode.Blocked;
        userFromDb.UserState.Description = "Removed";

        var removedUser = await _userRepository.Update(userFromDb);

        return removedUser;
    }
}