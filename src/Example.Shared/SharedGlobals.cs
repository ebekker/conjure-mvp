namespace Example.Shared
{
    public static class SharedGlobals
    {
#if BLAZOR_SERVERSIDE
        public static readonly bool IsBlazorServerSide = true;
        public const string BlazorHostingMode = "SERVER-SIDE";
#else
        public static readonly bool IsBlazorServerSide = false;
        public const string BlazorHostingMode = "CLIENT-SIDE";
#endif // BLAZOR_SERVERSIDE
    }
}