using Binding.Tests.PhoneDomain;

namespace Binding.Tests.PhoneBuilders
{
    class WindowsPhoneBuilder : IPhoneBuilder
    {
        MobilePhone phone;

        public WindowsPhoneBuilder()
        {
            phone = new MobilePhone("Windows Phone");
        }

        #region IPhoneBuilder Members

        public void BuildScreen()
        {
            phone.PhoneScreen = ScreenType.ScreenType_TOUCH_CAPACITIVE;
        }

        public void BuildBattery()
        {
            phone.PhoneBattery = Battery.MAH_2000;
        }

        public void BuildOS()
        {
            phone.PhoneOS = OperatingSystem.WINDOWS_PHONE;
        }

        public void BuildStylus()
        {
            phone.PhoneStylus = Stylus.NO;
        }

        // GetResult Method which will return the actual phone
        public MobilePhone Phone
        {
            get { return phone; }
        }

        #endregion
    }
}