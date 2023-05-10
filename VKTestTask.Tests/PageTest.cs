using VKTestTask.Domain.Dto;

namespace VKTestTask.Tests;

public class PageTest
{
    [Fact]
    public void GIVEN_Page_WHEN_Offset_calculated_THEN_Correct_value_expected()
    {
        var page = new Page { Number = 3, Size = 10 };

        var offset = page.GetOffset();

        Assert.Equal(20, offset);
    }

    [Fact]
    public void GIVEN_Page_in_valid_state_WHEN_State_calculated_THEN_Valid_expected()
    {
        var page = new Page { Number = 3, Size = 10 };

        var isInValidState = page.IsInValidState();

        Assert.True(isInValidState);
    }

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, 1)]
    public void GIVEN_Page_with_invalid_number_WHEN_State_calculated_THEN_Invalid_expected(int number, int size)
    {
        var page = new Page { Number = number, Size = size };


        var isInValidState = page.IsInValidState();

        Assert.False(isInValidState);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(1239, 2)]
    public void GIVEN_Page_with_valid_number_WHEN_State_calculated_THEN_Valid_expected(int number, int size)
    {
        var page = new Page { Number = number, Size = size };

        var isInValidState = page.IsInValidState();

        Assert.True(isInValidState);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    [InlineData(1, -3012)]
    public void GIVEN_Page_with_invalid_size_WHEN_State_calculated_THEN_Invalid_expected(int number, int size)
    {
        var page = new Page { Number = number, Size = size };

        var isInValidState = page.IsInValidState();

        Assert.False(isInValidState);
    }

    [Fact]
    public void GIVEN_Page_with_valid_size_WHEN_State_calculated_THEN_Valid_expected()
    {
        var page = new Page { Number = 12, Size = 1 };

        var isInValidState = page.IsInValidState();

        Assert.True(isInValidState);
    }
}