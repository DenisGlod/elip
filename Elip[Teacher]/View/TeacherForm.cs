﻿using ElipAdmin.Model;
using ElipAdmin.Model.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElipTeacher.View
{
    public partial class TeacherForm : Form
    {
        private User user;
        public TeacherForm(object user)
        {
            this.user = (User)user;
            InitializeComponent();
            InitHeaderLabel();
            InitTVGroup();
            InitDGVMyLabAndTest();
        }

        private void InitDGVMyLabAndTest()
        {
            using (var dbContext = new ElipContext())
            {
                var myDataList = dbContext.Users.Find(user.Id).DataInGroups;
                DGVMyLabAndTest.DataSource = myDataList;
                DGVMyLabAndTest.Columns["Group"].Visible = false;
                DGVMyLabAndTest.Columns["User"].Visible = false;
                DGVMyLabAndTest.Columns["UserId"].Visible = false;
                DGVMyLabAndTest.Columns["Data"].Visible = false;
            }
        }

        private void InitTVGroup()
        {
            using (var dbContext = new ElipContext())
            {
                var groups = dbContext.Groups.ToList();
                foreach (var item in groups)
                {
                    TVGroup.Nodes.Add(item.Id.ToString(), item.NumberGroup);
                }
            }
        }

        private void InitHeaderLabel()
        {
            LUserInfo.Text = new StringBuilder()
                .Append("Id: ")
                .Append(user.Id)
                .Append(" | ")
                .Append(user.LastName)
                .Append(" ")
                .Append(user.FirstName)
                .Append(" ")
                .Append(user.MiddleName)
                .Append(" | Роль: ")
                .Append(user.Role).ToString();
        }

        private void TeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            new LoginForm().Show();
        }

        private void TVGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TVGroup.SelectedNode.BackColor = SystemColors.Highlight;
            TVGroup.SelectedNode.ForeColor = Color.White;
            using (var dbContext = new ElipContext())
            {
                var group = dbContext.Groups.Find(int.Parse(TVGroup.SelectedNode.Name));
                switch (TabControl.SelectedIndex)
                {
                    case 0:
                        DGVUsers.DataSource = group.Users;
                        DGVUsers.Columns["GroupId"].Visible = false;
                        DGVUsers.Columns["Group"].Visible = false;
                        DGVUsers.Columns["DataInGroups"].Visible = false;
                        DGVUsers.Columns["Role"].Visible = false;
                        DGVUsers.Columns["Login"].Visible = false;
                        DGVUsers.Columns["Password"].Visible = false;
                        break;
                    case 1:
                        DGVDataInGroup.DataSource = group.DataInGroups;
                        DGVDataInGroup.Columns["Group"].Visible = false;
                        DGVDataInGroup.Columns["GroupId"].Visible = false;
                        DGVDataInGroup.Columns["User"].Visible = false;
                        DGVDataInGroup.Columns["Data"].Visible = false;
                        break;
                }
            }
        }

        private void BSettings_Click(object sender, System.EventArgs e)
        {
            new TeacherSettingsFrom().Show();
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (TabControl.SelectedIndex == 2)
            {
                InitDGVMyLabAndTest();
            }
            if (TVGroup.SelectedNode != null)
            {
                TVGroup_AfterSelect(TVGroup, null);
            }
        }

        private void BAddDataInGroup_Click(object sender, System.EventArgs e)
        {
            new AddDataInGroupForm(user).Show();
        }

        private void TVGroup_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (TVGroup.SelectedNode != null)
            {
                TVGroup.SelectedNode.BackColor = Color.Transparent;
                TVGroup.SelectedNode.ForeColor = SystemColors.ControlText;
            }
        }
    }
}
