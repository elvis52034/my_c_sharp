using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace temp0608
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fapaDataSet1.ReadXml("receiptLotto.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //將輸入的資料儲存
            DataRow inRow = fapaDataSet1.Tables["Receipt"].NewRow();
            inRow["獎號"] = textBox3.Text;
            inRow["獎項"] = textBox4.Text;
            inRow["獎金"] = textBox2.Text;
            fapaDataSet1.Tables["Receipt"].Rows.Add(inRow);
            fapaDataSet1.WriteXml("receiptLotto.xml");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();  //系統內建音效
            //比對大獎獎號
            string s1 = textBox1.Text;
           
            fapaDataSet ds = new fapaDataSet();
            ds.ReadXml("receiptLotto.xml");
            DataColumn dc = ds.Tables["Receipt"].Columns["獎號"];
            ds.Tables["Receipt"].Constraints.Add("PK_獎號", dc, true);
            DataRow dr = ds.Tables["Receipt"].Rows.Find(s1);
         
            if (dr == null)
            {
                fin();
            }
            else
            {
                string result;
                string result2;
                result = dr["獎項"] + Environment.NewLine;
                result2 = dr["獎金"] + Environment.NewLine;
                label2.Text = (result+result2);
            }
        } //end button1
       
        private void fin()
        {
            //比對頭獎個別獎項
            string sa = "";
            string foundRows;
            int i2 = 0;
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    string s = textBox1.Text;
                    string sub = s.Substring(5 - i, i + 3);


                    string infor = string.Format("獎號 Like '%{0}'", sub);
                    foundRows = fapaDataSet1.Receipt.Select(infor)[0]
                    ["獎項"].ToString();
                    i2 = i;
                    sa = sub;
                }
            }
            catch
            {
                string infor = string.Format("獎號 Like '%{0}'", sa);
                foundRows = fapaDataSet1.Receipt.Select(infor)[0]
                ["獎項"].ToString();

                if (foundRows == "頭獎" || foundRows == "增加獎")
                {
                    int i3 = i2;
                    string[] gift = new string[] { "2百元", "1千元", "4千元", "1萬元", "4萬元" };
                    label2.Text = ("中獎了" + gift[i3]);
                }

                else if (foundRows == "特別獎" || foundRows == "特獎")
                {
                    label2.Text = ("可惜!!沒中獎");
                }

            } 
            }  
        private void button3_Click(object sender, EventArgs e)
        {
            /*
            int iRow = bindingSource1.Position;
            fapaDataSet1.Receipt.Rows.RemoveAt(iRow);
           
            fapaDataSet1.WriteXml("receiptLotto.xml");
            */
        }


        /*
         當中三碼時比對到第四碼出現錯誤,不會顯示中獎
        try
        {
            for (int i = 0; i < 5; i++)
            {
                string s = textBox1.Text;
                string sub = s.Substring(5 - i, i + 3);

                string foundRows;
                string infor = string.Format("獎號 Like '%{0}'", sub);
                foundRows = fapaDataSet1.Receipt.Select(infor)[0]["獎項"].ToString();

                if (foundRows == "頭獎" || foundRows == "增加獎")
                {

                    string[] gift = new string[] { "2百元", "1千元", "4千元", "1萬元", "4萬元", "20萬元" };
                    label2.Text = ("中獎了" + gift[i]);
                }

                else if (foundRows == "特別獎" || foundRows == "特獎")
                {
                    label2.Text = ("可惜!!沒中獎");
                }

            }

        }
        catch
        {
            label2.Text = ("is not right");
        }
    }

*/


    }//end form
} 
