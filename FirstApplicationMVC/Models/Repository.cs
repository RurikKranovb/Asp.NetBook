namespace FirstApplicationMVC.Models
{
    public static class Repository
    {
        private static readonly List<GuestResponse> _responses = new();

        public static IEnumerable<GuestResponse> Responses => _responses;

        public static void AddResponse(GuestResponse guestResponse)
        {
            _responses.Add(guestResponse);
        }
    }
}
