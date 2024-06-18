namespace ProductAdmin.Domain.InventoryContext;

using ProductAdmin.Domain.SharedKernel;

public class InventoryContextException : BusinessException
{
    public InventoryContextException(InventoryContextExceptionEnum businessExceptionEnum) : base(Detail(businessExceptionEnum).Item2)
    {
        Code = Detail(businessExceptionEnum).Item1;
    }

    private static Tuple<int, string> Detail(InventoryContextExceptionEnum businessExceptionEnum)
    {
        string message = businessExceptionEnum switch
        {
            InventoryContextExceptionEnum.ExistProduct => "Product already exists",
            InventoryContextExceptionEnum.NoExistCategory => "Category does not exist",
            InventoryContextExceptionEnum.NoExistsProduct => "Product does not exist",
            InventoryContextExceptionEnum.NoExistsProducts => "Products do not exist",
            _ => "Unknown error"
        };

        return new Tuple<int, string>((int)businessExceptionEnum, message);
    }
}

public enum InventoryContextExceptionEnum
{
    ExistProduct = 1000,
    NoExistCategory = 1001,
    NoExistsProduct = 1002,
    NoExistsProducts = 1003,
}