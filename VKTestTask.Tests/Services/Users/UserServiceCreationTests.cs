using Moq;
using VKTestTask.Domain;
using VKTestTask.Domain.Dto;
using VKTestTask.Repositories.Users;
using VKTestTask.Services.Delay;
using VKTestTask.Services.Reservation;
using VKTestTask.Services.Time;
using VKTestTask.Services.Users;
using VKTestTask.Services.Users.Exceptions;

namespace VKTestTask.Tests.Services.Users;

public class UserServiceCreationTests
{
    public UserCreationModel NewAdmin => new()
    {
        Login = "I wanna be an admin!",
        Password = "qwerty",
        GroupCode = Enum.GetName(GroupCode.Admin)!
    };
    public UserCreationModel NewBodvar => new()
    {
        Login = "I wanna be the Bodvar",
        Password = "qwerty",
        GroupCode = Enum.GetName(GroupCode.User)!
    };

    [Fact]
    async void GIVEN_Admin_exist_WHEN_Try_add_new_admin_THEN_Exception_will_be_thrown()
    {
        GetMockServices(out var reserver, out _, out var timeSrService, out var delayService);
        var reposMock = new Mock<IUserRepository>();
        reposMock.Setup(x => x.IsAdminExist()).Returns(Task.Run(() => true));

        IUserService userService = new UserService(reserver, reposMock.Object, timeSrService, delayService);
        
        await Assert.ThrowsAsync<AdminAlreadyExistException>(() => userService.Create(NewAdmin));
    }

    [Fact]
    async void GIVEN_Admin_doesnt_exist_WHEN_Try_add_new_admin_THEN_no_exception_will_be_thrown()
    {
        GetMockServices(out var reserver, out _, out var timeSrService, out var delayService);
        var reposMock = new Mock<IUserRepository>();
        reposMock.Setup(x => x.IsAdminExist()).Returns(Task.Run(() => false));
        IUserService userService = new UserService(reserver, reposMock.Object, timeSrService, delayService);
       
        var exception = await Record.ExceptionAsync(async () => await userService.Create(NewAdmin));
        
        Assert.Null(exception);
    }

    [Fact]
    async void GIVEN_Time_service_WHEN_Try_add_user_THEN_created_date_as_in_time_service()
    {
        GetMockServices(out var reserver, out _, out _, out var delayService);
        var timeService = new Mock<ITimeService>();
        timeService.Setup(x => x.GetTime()).Returns(new DateTime(1, 2, 3, 4, 5, 6));
        var reposMock = new Mock<IUserRepository>();
        reposMock.Setup(x => x.Add(It.IsAny<User>())).Returns<User>(user => Task.Run(() => user));

        IUserService userService = new UserService(reserver, reposMock.Object, timeService.Object, delayService);

        var createdUser = await userService.Create(NewBodvar);

        Assert.Equal(new DateTime(1, 2, 3, 4, 5, 6), createdUser.CreatedDate);
    }

    [Fact]
    async void GIVEN_User_exist_WHEN_Try_add_user_with_same_login_THEN_Exception_will_be_thrown()
    {
        GetMockServices(out var reserver, out var repos, out var timeService, out var delayService);
        var reposMock = new Mock<IUserRepository>();
        reposMock.Setup(x => x.IsLoginExist(It.IsAny<string>()))
            .Returns<string>(_ => Task.Run(() => true));

        IUserService userService = new UserService(reserver, reposMock.Object, timeService, delayService);

        await Assert.ThrowsAsync<LoginAlreadyExistException>(() => userService.Create(NewBodvar));
    }

    [Fact]
    public void GIVEN_Login_reserved_WHEN_Try_add_user_with_reserved_login_THEN_Exception_will_be_thrown()
    {
        GetMockServices(out var reserver, out var repos, out var timeService, out var delayService);
        var reserverMock = new Mock<ILoginReservationService>();
        reserverMock.Setup(x => x.IsReserved(It.IsAny<string>()))
            .Returns(true);

        IUserService userService = new UserService(reserver, repos, timeService, delayService);

        Assert.ThrowsAsync<LoginReservedException>(() => userService.Create(NewBodvar));
    }

    private static void GetMockServices(
        out ILoginReservationService loginReservationService,
        out IUserRepository userRepository,
        out ITimeService timeService,
        out IDelayTimeService delayTimeService
    )
    {
        userRepository = new Mock<IUserRepository>().Object;
        loginReservationService = new Mock<ILoginReservationService>().Object;
        timeService = new Mock<ITimeService>().Object;

        var delayMock = new Mock<IDelayTimeService>();
        delayMock.Setup(x => x.GetDelay()).Returns(0);
        delayTimeService = delayMock.Object;
    }
}