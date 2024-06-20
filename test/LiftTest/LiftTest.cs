using Lift;
namespace LiftTest;

public class ExampleTests
{
    [Test]
    public void TestUp()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[0], // 1
            new int[]{5,5,5}, // 2
            new int[0], // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        int[] result = Dinglemouse.TheLift(queues, 5);
        foreach (var value in result)
        {
            TestContext.WriteLine("value");
        }
        Assert.That(new[] { 0, 2, 5, 0 }, Is.EqualTo(result));
    }

    [Test]
    public void TestDown()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[0], // 1
            new int[]{1,1}, // 2
            new int[0], // 3
            new int[0], // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        Assert.That(new[] { 0, 2, 1, 0 }, Is.EqualTo(result));
    }

    [Test]
    public void TestUpAndUp()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[]{3}, // 1
            new int[]{4}, // 2
            new int[0], // 3
            new int[]{5}, // 4
            new int[0], // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        TestContext.WriteLine(result);
        Assert.That(new[] { 0, 1, 2, 3, 4, 5, 0 }, Is.EqualTo(result));
    }

    [Test]
    public void TestDownAndDown()
    {
        int[][] queues =
        {
            new int[0], // G
            new int[]{0}, // 1
            new int[0], // 2
            new int[0], // 3
            new int[]{2}, // 4
            new int[]{3}, // 5
            new int[0], // 6
        };
        var result = Dinglemouse.TheLift(queues, 5);
        TestContext.WriteLine(result);
        Assert.That(new[] { 0, 5, 4, 3, 2, 1, 0 }, Is.EqualTo(result));
    }
}
