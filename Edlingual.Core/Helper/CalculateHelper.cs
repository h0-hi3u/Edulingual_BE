namespace Edulingual.Common.Helper;

public static class CalculateHelper
{
    public static int CalculatePaging(int pageSize, int pageIndex)
    {
        pageSize = pageSize < 1 ? 1 : pageSize;
        pageIndex = pageIndex < 1 ? 1 : pageIndex;
        return (pageIndex - 1) * pageSize;
    }
}
