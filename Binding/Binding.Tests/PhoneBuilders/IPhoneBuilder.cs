using Binding.Tests.PhoneDomain;

namespace Binding.Tests.PhoneBuilders
{
    // This is the "Builder" class
    interface IPhoneBuilder
    {
        void BuildScreen();
        void BuildBattery();
        void BuildOS();
        void BuildStylus();
        MobilePhone Phone { get; }
    }
}
