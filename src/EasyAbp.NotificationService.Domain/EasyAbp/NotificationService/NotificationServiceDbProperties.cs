namespace EasyAbp.NotificationService
{
    public static class NotificationServiceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "NotificationService";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "NotificationService";
    }
}
