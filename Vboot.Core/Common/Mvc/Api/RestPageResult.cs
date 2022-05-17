namespace Vboot.Core.Common;

public class PageResult
{
    public int total { get; set; }
    public object items { get; set; }
}

public static class RestPageResult
{
    public static PageResult Build(int Total, object Items)
    {
        return new()
        {
            total = Total,
            items = Items
        };
    }
}