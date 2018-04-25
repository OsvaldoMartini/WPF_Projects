using System.Collections.Generic;
using System.Linq;
using Europa_Data.Controls;

namespace Europa_Data.Model
{
    public class HierarchyOrganization
    {
        private static HierarchyOrganization self;
        private Dictionary<int, UserVM> listUser = new Dictionary<int, UserVM>();

        private HierarchyOrganization()
        {
            listUser.Add(1, new UserVM { _UserId = 1, Forename = "Joe", Surname = "Smith", ParentId = 1 });
            listUser.Add(2, new UserVM { _UserId = 2, Forename = "Rich", Surname = "Angel", ParentId = 1 });
            listUser.Add(3, new UserVM { _UserId = 3, Forename = "Mary", Surname = "Peach", ParentId = 1 });
            listUser.Add(4, new UserVM { _UserId = 4, Forename = "Mike", Surname = "Door", ParentId = 2 });
            listUser.Add(5, new UserVM { _UserId = 5, Forename = "Jimmy", Surname = "Fond", ParentId = 2 });
            listUser.Add(6, new UserVM { _UserId = 6, Forename = "Ann", Surname = "Brown", ParentId = 4 });
            listUser.Add(7, new UserVM { _UserId = 7, Forename = "George", Surname = "Farm", ParentId = 4 });
            listUser.Add(8, new UserVM { _UserId = 8, Forename = "Able", Surname = "Jump", ParentId = 4 });
            listUser.Add(9, new UserVM { _UserId = 9, Forename = "Corn", Surname = "Shaw", ParentId = 1 });
            listUser.Add(10, new UserVM { _UserId = 10, Forename = "Grey", Surname = "Peech", ParentId = 9 });
            listUser.Add(11, new UserVM { _UserId = 11, Forename = "Manny", Surname = "Travel", ParentId = 9 });
        }
        private HierarchyOrganization(UserObservableCollection<UserVM> dataItems)
        {
            int index = 1;
            foreach (var item in dataItems)
            {
                listUser.Add(index, item);
                index++;
            }
        }

        public static HierarchyOrganization Instance()
        {
            if (self == null)
                self = new HierarchyOrganization();
            return self;
        }


        public UserVM GetRoot()
        {
            return listUser[1];
        }


        public IEnumerable<UserVM> GetChildren(int parentId)
        {
            return from a in listUser
                where a.Value.ParentId == parentId
                      && a.Value._UserId !=
                      parentId
                select a.Value;
        }
    }

}






