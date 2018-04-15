using Binding.Tests.PhoneDomain;

namespace Binding.Tests.PhoneBuilders
{
        class AndroidPhoneBuilder : IPhoneBuilder
        {
            MobilePhone phone;

            public AndroidPhoneBuilder()
            {
                phone = new MobilePhone("Android Phone");
            }

       

            public void BuildScreen()
            {
                phone.PhoneScreen = ScreenType.ScreenType_TOUCH_RESISTIVE;
            }

            public void BuildBattery()
            {
                phone.PhoneBattery = Battery.MAH_1500;
            }

            public void BuildOS()
            {
                phone.PhoneOS = OperatingSystem.ANDROID;
            }

            public void BuildStylus()
            {
                phone.PhoneStylus = Stylus.YES;
            }
        
            // GetResult Method which will return the actual phone
            public MobilePhone Phone
            {
                get { return phone; }
            }

        }
    }
