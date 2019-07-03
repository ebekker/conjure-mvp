namespace Example.Server
{
    public static class ServerGlobals
    {
#if BLAZOR_SERVERSIDE
        public static readonly bool IsBlazorServerSide = true;
        public const string BlazorHostingMode = "SERVER-SIDE";
#else
        public static readonly bool IsBlazorServerSide = false;
        public const string BlazorHostingMode = "CLIENT-SIDE";
#endif // BLAZOR_SERVERSIDE

#if BLAZOR_PRERENDER
        public static readonly bool IsBlazorPrerender = true;
#else
        public static readonly bool IsBlazorPrerender = false;
#endif // BLAZOR_PRERENDER
    }
}