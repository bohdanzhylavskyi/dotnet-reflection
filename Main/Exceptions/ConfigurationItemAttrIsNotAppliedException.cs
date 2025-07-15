namespace dotnet_reflection.Exceptions
{
    public class ConfigurationItemAttrIsNotAppliedException : BaseException
    {
        public ConfigurationItemAttrIsNotAppliedException(string propertyName):
            base($"'{nameof(ConfigurationItemAttribute)}' is not applied to property '{propertyName}'.")
        {}
    }
}
