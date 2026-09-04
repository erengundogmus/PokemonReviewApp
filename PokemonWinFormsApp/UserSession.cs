namespace PokemonWinFormsApp
{
    public static class UserSession
    {
        public static string Token { get; set; }
        public static string Username { get; set; }
        public static List<string> Permissions { get; set; } = new();
        public static bool HasPermission(string permissionName)
        {
            return Permissions != null && Permissions.Contains(permissionName);
        }
        public static void Logout()
        {
            Token = string.Empty;
            if (Permissions != null)
            {
                Permissions.Clear();
            }
        }


    }
}