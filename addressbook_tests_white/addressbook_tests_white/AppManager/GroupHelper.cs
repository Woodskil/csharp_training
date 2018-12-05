using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public Window OpenEditGroupDialog()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public void CloseEditGroupDialog(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenEditGroupDialog();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                list.Add(new GroupData(item.Text));
            }
            CloseEditGroupDialog(dialogue);
            return list;
        }

        public void AddGroup(GroupData newGroup)
        {
            Window dialogue = OpenEditGroupDialog();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox) dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.SetValue(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseEditGroupDialog(dialogue);
        }

        public void RemoveFroup(GroupData newGroup)
        {
            Window dialogue = OpenEditGroupDialog();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                if (item.Text == newGroup.Name)
                {
                    item.Click();
                    break;
                }
            }
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            Window delete = dialogue.ModalWindow("Delete group");
            delete.Get<Button>("uxOKAddressButton").Click();
            CloseEditGroupDialog(dialogue);
        }
    }
}
