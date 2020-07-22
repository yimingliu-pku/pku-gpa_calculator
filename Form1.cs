using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gpa_calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbType.Items.Add("分数转换为GPA");
            cmbType.Items.Add("GPA转换为分数");
            cmbType.SelectedIndex = 0;
        }

        private void btnTansfer_Click(object sender, EventArgs e)
        {
            if (txtGPAorGrade.Text != "")
            {
                if (0 == cmbType.SelectedIndex)
                {
                    string transferedgpa = "";
                    double grade = Convert.ToDouble(txtGPAorGrade.Text);
                    if (grade >= 60.0 && grade <= 100.0)
                    {
                        transferedgpa = (4.0 - 3.0 * (100 - grade) * (100 - grade) / 1600).ToString("#0.00");
                        MessageBox.Show("分数对应的GPA为" + transferedgpa);
                    }
                    else if (grade <= 60.0 && grade >= 0.0)
                    {
                        transferedgpa = "0";
                        MessageBox.Show("分数对应的GPA为" + transferedgpa);
                    }
                    else
                    {
                        MessageBox.Show("输入错误，请重新输入");
                        btnClear_Click(sender, e);
                    }

                }
                else
                {
                    string transferedgrade = "";
                    double gpa = Convert.ToDouble(txtGPAorGrade.Text);
                    if (gpa <= 4.0 && gpa >= 1.0)
                    {
                        transferedgrade = (100.0 - Math.Sqrt((4.0 - gpa) * 1600 / 3)).ToString("#0.00");
                        MessageBox.Show("GPA对应的分数为" + transferedgrade);
                    }
                    else if (0.0 == gpa)
                    {
                        transferedgrade = "不及格（小于60分）";
                        MessageBox.Show("GPA对应的分数为" + transferedgrade);
                    }
                    else
                    {
                        MessageBox.Show("输入错误，请重新输入");
                        btnClear_Click(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("你输入的参数不完整，请重新输入！");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtGPAorGrade.Text = "";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Table gpagrade = new Table();
            gpagrade.dataGridView1.Columns.Add("Grade", "成绩");
            gpagrade.dataGridView1.Columns.Add("GPA", "GPA");
            gpagrade.dataGridView1.Rows.Add("<60", 0);
            for (int i = 60; i <= 100; i++)
            {
                double gpa = Math.Round(4.0 - 3.0 * (100 - i) * (100 - i) / 1600,2);
                gpagrade.dataGridView1.Rows.Add(i,gpa);
            }
            gpagrade.Text = "成绩与GPA对应表";
            gpagrade.ShowDialog();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtNameGoal.Text = "";
            txtTempCredit.Text = "";
            txtTempGPA.Text = "";
            txtThisCredit.Text = "";
            txtGoalGPA.Text = "";
        }

        private void btnStartCaculate_Click(object sender, EventArgs e)
        {
            Table goal = new Table();
            goal.Text = txtNameGoal.Text + "想要达到的总绩点与这学期绩点的对应关系";
            if (txtNameGoal.Text!="" && txtTempCredit.Text!="" && txtTempGPA.Text != ""&& txtThisCredit.Text != "")
            {
                double origingpa = Convert.ToDouble(txtTempGPA.Text);
                double thissemcredit = Convert.ToDouble(txtThisCredit.Text);
                double origincredit = Convert.ToDouble(txtTempCredit.Text);
                string name = txtNameGoal.Text;
                goal.dataGridView1.Columns.Add("TotalGPA", name + "想要达到的总绩点");
                goal.dataGridView1.Columns.Add("ThisSemGPA", name + "这学期需要得到的绩点");
                goal.dataGridView1.Columns.Add("ThisSemGrade", name + "这学期需要得到的成绩");
                if (txtGoalGPA.Text!="")
                {
                    if (Convert.ToDouble(txtGoalGPA.Text) < 4.0 && Convert.ToDouble(txtGoalGPA.Text) > 1.0)
                    {
                        double goalgpa = Convert.ToDouble(txtGoalGPA.Text);
                        double resultgpa = Math.Round(((thissemcredit + origincredit) * goalgpa - origingpa * origincredit) / thissemcredit,2);
                        double resultgrade = Math.Round(100.0 - Math.Sqrt((4.0 - resultgpa) * 1600 / 3),2);
                        if (resultgpa > 1.0 && resultgpa < 4.0)
                        {
                            goal.dataGridView1.Rows.Add(goalgpa, resultgpa, resultgrade);
                            goal.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("你设定的目标不可能达成！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("你输入了错误的目标GPA！");
                    }
                }
                else
                {
                    for (int i = 50; i <= 200; i++)
                    {
                        double goalgpa = i * 0.02;
                        double resultgpa = Math.Round(((thissemcredit + origincredit) * goalgpa - origingpa * origincredit) / thissemcredit, 2);
                        double resultgrade = Math.Round(100.0 - Math.Sqrt((4.0 - resultgpa) * 1600 / 3), 2);
                        if (resultgpa > 1.0 && resultgpa < 4.0)
                        {
                            goal.dataGridView1.Rows.Add(goalgpa, resultgpa, resultgrade);
                        }
                    }
                    if (goal.dataGridView1.RowCount==0)
                    {
                        MessageBox.Show("你设定的目标不可能达成！");
                    }
                    else
                    {
                        goal.ShowDialog();
                    }
                }

            }
            else
            {
                MessageBox.Show("你输入的参数不完整，请重新输入！");
            }
        }
    }
}
