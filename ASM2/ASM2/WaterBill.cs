using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASM2
{
    public partial class WaterBill : Form
    {
        public WaterBill()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cobCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cobCustomerType.Text.Trim().ToLower();
            if (type.Equals("household customer"))
            {
                txtMember.Enabled = true;
            }
            else
            {
                txtMember.Enabled = false;
                txtMember.Text = null;
            }

        }

        private void WaterBill_Load(object sender, EventArgs e)
        {
            lstVBill.View = View.Details;
            lstVBill.GridLines = true;
            lstVBill.FullRowSelect = true;

            lstVBill.Columns.Add("Full name", 150);
            lstVBill.Columns.Add("Type", 100);
            lstVBill.Columns.Add("Member", 50);
            lstVBill.Columns.Add("Last Water number", 100);
            lstVBill.Columns.Add("Current Water number", 100);
            lstVBill.Columns.Add("Water number");
            lstVBill.Columns.Add("Money" + " VND");


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            ListViewItem item = new ListViewItem();
            item.SubItems.Add(txtFullName.Text.Trim());
            item.SubItems.Add(cobCustomerType.Text.Trim());
            item.SubItems.Add(txtMember.Text.Trim());
            item.SubItems.Add(txtLastMonthWater.Text.Trim());
            item.SubItems.Add(txtCurrentMonth.Text.Trim());
            item.SubItems.Add(txtWatercon.Text.Trim());
            item.SubItems.Add(txtMoney.Text.Trim());
            lstVBill.Items.Add(item);
            if (lstVBill.SelectedItems.Count > 0)
            {

                lstVBill.Items.Remove(lstVBill.SelectedItems[0]);

            }
            btnEdit.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string name = txtFullName.Text.Trim();
            string type = cobCustomerType.Text.Trim();
            string member = txtMember.Text.Trim();
            string Nummember = txtMember.Text.Trim();
            string lastmonthwaterindex = txtLastMonthWater.Text.Trim();
            string thismonthwaterindex = txtCurrentMonth.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter your name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (member.Equals("Household customer"))
            {
                if (string.IsNullOrEmpty(member))
                {
                    MessageBox.Show("Please enter your family member", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else if (int.Parse(Nummember) < 1)
                {
                    MessageBox.Show("Number of members must be >= 1", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                else if (!int.TryParse(lastmonthwaterindex, out _))
                {
                    MessageBox.Show("Last month water index must be interger", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else if (!int.TryParse(thismonthwaterindex, out _))
                {
                    MessageBox.Show("Current month water index must be interger", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (int.Parse(thismonthwaterindex) - int.Parse(lastmonthwaterindex) < 0 || int.Parse(lastmonthwaterindex) < 0 || int.Parse(thismonthwaterindex) < 0)
                {
                    MessageBox.Show("Please Check your water index");
                    return;
                }
                else
                {

                }
            }



            string describ = lstVBill.Text.Trim();
            ListViewItem item = new ListViewItem();
            item.SubItems.Add(name);
            item.SubItems.Add(type);
            item.SubItems.Add(member);
            item.SubItems.Add(lastmonthwaterindex);
            item.SubItems.Add(thismonthwaterindex);


            lstVBill.Items.Add(item);


        }
        public double calmoney()
        {
            double money = 0;
            string name = txtFullName.Text.Trim();
            string member = txtMember.Text.Trim();
            string Nummember = txtMember.Text.Trim();
            string lastmonthwaterindex = txtLastMonthWater.Text.Trim();
            string thismonthwaterindex = txtCurrentMonth.Text.Trim();


            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter your name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return money;
            }
            if (member.Equals("Household customer"))
            {
                if (string.IsNullOrEmpty(member))
                {
                    MessageBox.Show("Please enter your family member", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return money;
                }

                else if (int.Parse(Nummember) < 1)
                {
                    MessageBox.Show("Number of members must be >= 1", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return money;
                }


                else if (!int.TryParse(lastmonthwaterindex, out _))
                {
                    MessageBox.Show("Last month water index must be interger", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return money;
                }

                else if (!int.TryParse(thismonthwaterindex, out _))
                {
                    MessageBox.Show("Current month water index must be interger", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return money;
                }
                else if (int.Parse(thismonthwaterindex) - int.Parse(lastmonthwaterindex) < 0 || int.Parse(lastmonthwaterindex) < 0 || int.Parse(thismonthwaterindex) < 0)
                {
                    MessageBox.Show("Please Check your water index");
                    return money;
                }
                else
                {
                    double usedWater = int.Parse(thismonthwaterindex) - int.Parse(lastmonthwaterindex);
                    double memberUsedWater = usedWater / int.Parse(member);
                    if (memberUsedWater <= 10)
                    {
                        money = usedWater * 5973 * 1.1;
                        txtMoney.Text = Convert.ToString(money);
                        txtWatercon.Text = Convert.ToString(usedWater);
                        return money;
                    }
                    else if (memberUsedWater > 10 && memberUsedWater <= 20)
                    {
                        money = usedWater * 7052 * 1.1;
                        txtMoney.Text = Convert.ToString(money);
                        txtWatercon.Text = Convert.ToString(usedWater);
                        return money;
                    }
                    else if (memberUsedWater > 20 && memberUsedWater <= 30)
                    {
                        money = usedWater * 8699 * 1.1;
                        txtMoney.Text = Convert.ToString(money);
                        txtWatercon.Text = Convert.ToString(usedWater);
                        return money;
                    }
                    else
                    {
                        money = usedWater * 15929 * 1.1;
                        txtMoney.Text = Convert.ToString(money);
                        txtWatercon.Text = Convert.ToString(usedWater);
                        return money;
                    }

                }
            }
            return money;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (lstVBill.SelectedItems.Count > 0)
            {
                DialogResult dl = MessageBox.Show("Do you want to clear?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                {
                    lstVBill.Items.Remove(lstVBill.SelectedItems[0]);
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Pick one to clear", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            calmoney();
        }

        private void txtMoney_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


