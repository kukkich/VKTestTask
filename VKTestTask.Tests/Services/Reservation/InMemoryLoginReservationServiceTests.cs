using VKTestTask.Services.Reservation;

namespace VKTestTask.Tests.Services.Reservation;

public class InMemoryLoginReservationServiceTests
{
    [Fact]
    public void GIVEN_Reserver_WHEN_Login_added_THEN_That_login_reserved()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Mordex");

        Assert.True(reserver.IsReserved("Mordex"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_Login_added_THEN_Another_login_not_reserved()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Mordex");

        Assert.False(reserver.IsReserved("Mirage"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_Login_added_and_then_cancelled_THEN_That_login_not_reserved()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Mordex");
        reserver.Cancel("Mordex");

        Assert.False(reserver.IsReserved("Mirage"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_Some_logins_added_and_then_one_cancelled_THEN_That_login_not_reserved()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Mordex");
        reserver.Reserve("Fait");
        reserver.Reserve("Wu Shang");
        reserver.Reserve("Orion");

        reserver.Cancel("Orion");

        Assert.False(reserver.IsReserved("Orion"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_Some_logins_added_and_then_one_cancelled_THEN_Another_login_reserved()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Mordex");
        reserver.Reserve("Fait");
        reserver.Reserve("Wu Shang");
        reserver.Reserve("Orion");

        reserver.Cancel("Fait");

        Assert.True(reserver.IsReserved("Orion"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_None_of_logins_added_and_tried_cancel_THEN_Exception_will_be_thrown()
    {
        var reserver = new InMemoryLoginReservationService();

        Assert.Throws<InvalidOperationException>(() => reserver.Cancel("Nix"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_Some_logins_added_and_tried_cancel_non_existed_login_THEN_Exception_will_be_thrown()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Artemis");
        reserver.Reserve("Cross");
        reserver.Reserve("Bodvar");

        Assert.Throws<InvalidOperationException>(() => reserver.Cancel("Nix"));
    }

    [Fact]
    public void GIVEN_Reserver_WHEN_Login_added_then_cancelled_and_tried_cancel_THEN_That_Exception_will_be_thrown()
    {
        var reserver = new InMemoryLoginReservationService();

        reserver.Reserve("Nix");
        reserver.Cancel("Nix");

        Assert.Throws<InvalidOperationException>(() => reserver.Cancel("Nix"));
    }
}