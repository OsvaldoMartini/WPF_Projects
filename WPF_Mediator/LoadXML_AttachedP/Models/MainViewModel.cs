using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace LoadXML.ByAttachedP.Models
{
    public class MainViewModel : NotifyBase
    {
        public MenuModel Menu { get { return GetValue(() => Menu); } set { SetValue(() => Menu, value); } }

        public MainViewModel()
        {
            Menu = new MenuModel();
            LoadMenusFromXml(Properties.Resources.MenuItems);
        }

        private void LoadMenusFromXml(string xml)
        {
            var xmlDoc = XDocument.Parse(xml);

            // if you have more than two levels, this can be made recursive
            Menu.Items = new ObservableCollection<MenuModel>(
                            from menuItem in xmlDoc.Element("MenuItems").Elements("MenuItem")
                            select new MenuModel
                            {
                                Id = Convert.ToInt32(menuItem.Attribute("Id").Value),
                                Name = menuItem.Element("Name").Value,
                                Items = new ObservableCollection<MenuModel>(
                                    from subItem in menuItem.Elements("MenuItem")
                                    select new MenuModel
                                    {
                                        Id = Convert.ToInt32(subItem.Attribute("Id").Value),
                                        Name = subItem.Element("Name").Value,
                                        SourcePage = subItem.Element("SourcePage").Value
                                    })
                            });
        }
    }
}
