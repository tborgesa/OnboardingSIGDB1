namespace OnboardingSIGDB1.Domain._Base.Notification
{
    public class DomainNotification
    {
        public string Key { get; }
        public string Value { get; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
