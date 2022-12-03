namespace LibraryApp.Test;

public class BookTestData
{
    public static IEnumerable<object[]> Data => new List<object[]>
    {
        new object[] {"ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"},
        new object[] {"117366b8-3541-4ac5-8732-860d698e26a2"},
        new object[] {"66ff5116-bcaa-4061-85b2-6f58fbb6db25"},
        new object[] {"cd5089dd-9754-4ed2-b44c-488f533243ef"},
        new object[] {"d81e0829-55fa-4c37-b62f-f578c692af78"}
    };

    public static IEnumerable<object[]> DataFalse => new List<object[]>
    {
        new object[] {"ab2bd817-98cd-4cf3-a80a-53ea0cd9c222"},
        new object[] {"117366b8-3541-4ac5-8732-860d698e2600"},
        new object[] {"66ff5116-bcaa-4061-85b2-6f58fbb6db00"},
        new object[] {"cd5089dd-9754-4ed2-b44c-488f53324300"},
        new object[] {"d81e0829-55fa-4c37-b62f-f578c692af00"}
    };
}