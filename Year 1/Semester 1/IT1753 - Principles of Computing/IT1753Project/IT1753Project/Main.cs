using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace IT1753Project
{
    public partial class Main : Form
    {
        // class variables
        string custName;
        string paymentMethod;
        string membership;
        int orderNo;
        int custNum;
        int tries = 0;
        int a4FlatQty = 0;
        int a4RingQty = 0;
        int a3RingQty = 0;
        int a3FolderQty = 0;
        int a4FolderQty = 0;
        bool exceptionCaught = false;
        bool isMember;
        bool nameValid;
        bool contactNoValid;
        bool memberSpecified;
        bool paymentMethodSpecified;
        bool isAnItemSelected;
        bool qtyValid;
        double a4FlatTotal;
        double a4RingTotal;
        double a3RingTotal;
        double a3FolderTotal;
        double a4FolderTotal;
        double a4FlatNetTotal;
        double a4RingNetTotal;
        double a3RingNetTotal;
        double a3FolderNetTotal;
        double a4FolderNetTotal;
        double subTotal;
        double discount;
        double a4FlatDiscount;
        double a4RingDiscount;
        double a3RingDiscount;
        double a3FolderDiscount;
        double a4FolderDiscount;
        double totalDiscountAmt;
        double gst;
        double netTotal;

        // shortened database connection
        //SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\StationeryShop.mdf;Integrated Security = True");

        // database connection for school pc
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Z:\IT1753 - Principles of Computing\Main Project\IT1753Project\IT1753Project\StationeryShop.mdf;Integrated Security=True");

        // database connection for home pc
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Projects and Assignments\IT1753 - Principles of Computing\IT1753Project\IT1753Project\StationeryShop.mdf;Integrated Security=True");

        public Main(string str_Value)
        {
            InitializeComponent();
            lblUser.Text = str_Value;
        }

        private void ValidateData()
        {
            custName = txtName.Text;
            orderNo = int.Parse(txtOrderNo.Text);

            // check customer contact number
            try
            {
                custNum = int.Parse(txtContactNo.Text);

            }
            catch (Exception)
            {
                exceptionCaught = true;
                MessageBox.Show("Error: Customer Contact Number is null or not in correct format.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Clear();
                txtContactNo.Focus();
            }

            // check quantity values
            try
            {
                a4FlatQty = int.Parse(txtA4FlatQty.Text);
                a4RingQty = int.Parse(txtA4RingQty.Text);
                a3RingQty = int.Parse(txtA3RingQty.Text);
                a3FolderQty = int.Parse(txtA3FolderQty.Text);
                a4FolderQty = int.Parse(txtA4FolderQty.Text);
            }
            catch (Exception)
            {
                exceptionCaught = true;
                MessageBox.Show("Error: One or more of your quantity values is/are null or not in correct format.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // check if user enters a 0 or a negative value into the quantiy fields
            if ((a4FlatQty <= 0 && chbA4Flat.Checked == true) || (a4RingQty <= 0 && chbA4Ring.Checked == true) || (a3RingQty <= 0 && chbA3Ring.Checked == true) || (a3FolderQty <= 0 && chbA3Folder.Checked == true) || (a4FolderQty <= 0 && chbA4Folder.Checked == true))
            {
                qtyValid = false;
                MessageBox.Show("One or more of your quantity values is/are 0 or below 0");
            }
            else
            {
                qtyValid = true;
            }

            // check if txtName is blank
            if (custName == "")
            {
                nameValid = false;
                MessageBox.Show("Please enter a customer name");
            }
            else
            {
                nameValid = true;
            }

            // check if contact number is valid
            if (txtContactNo.TextLength < 8)
            {
                contactNoValid = false;
                MessageBox.Show("Please enter a valid contact number");
                txtContactNo.Clear();
                txtContactNo.Focus();
            }
            else if (custNum < 80000000 || custNum > 99999999)
            {
                contactNoValid = false;
                MessageBox.Show("A contact number can only begin with 8 or 9");
                txtContactNo.Clear();
                txtContactNo.Focus();
            }
            else
            {
                contactNoValid = true;
            }

            // check if user selected a member choice
            if (rdbYes.Checked == false && rdbNo.Checked == false)
            {
                memberSpecified = false;
                MessageBox.Show("Please select if customer is member or non-member.");
            }

            // check if user selected a payment method
            if (rdbCash.Checked == false && rdbCheque.Checked == false && rdbCreditCard.Checked == false && rdbNets.Checked == false)
            {
                paymentMethodSpecified = false;
                MessageBox.Show("Please select a payment method.");
            }

            // check if user selected an item
            if (chbA4Flat.Checked == false && chbA4Ring.Checked == false && chbA3Ring.Checked == false && chbA3Folder.Checked == false && chbA4Folder.Checked == false)
            {
                isAnItemSelected = false;
                MessageBox.Show("Please select an item");
            }
        }

        // check user's member choice
        private void IsAMember()
        {
            if (rdbYes.Checked == true)
            {
                memberSpecified = true;
                isMember = true;
                membership = "Yes";
            }
            else if (rdbNo.Checked == true)
            {
                memberSpecified = true;
                isMember = false;
                membership = "No";
            }
        }

        private void CheckPaymentMethod()
        {
            if (rdbCash.Checked == true)
            {
                paymentMethodSpecified = true;
                paymentMethod = "Cash";
            }
            else if (rdbCheque.Checked == true)
            {
                paymentMethodSpecified = true;
                paymentMethod = "Cheque";
            }
            else if (rdbCreditCard.Checked == true)
            {
                paymentMethodSpecified = true;
                paymentMethod = "Credit Card";
            }
            else if (rdbNets.Checked == true)
            {
                paymentMethodSpecified = true;
                paymentMethod = "NETS";
            }
        }

        private void CheckIfAnItemIsSelected()
        {
            if (chbA4Flat.Checked == true || chbA4Ring.Checked == true || chbA3Ring.Checked == true || chbA3Folder.Checked == true || chbA4Folder.Checked == true)
            {
                isAnItemSelected = true;
            }
        }

        private void CalculateEachItemTotal()
        {
            if (chbA4Flat.Checked == true)
            {
                MemberDiscount();
                a4FlatTotal = a4FlatQty * 0.5;
                a4FlatDiscount = a4FlatTotal * discount;
                a4FlatNetTotal = a4FlatTotal - a4FlatDiscount;
            }

            if (chbA4Ring.Checked == true)
            {
                MemberDiscount();
                a4RingTotal = a4RingQty * 3.0;
                a4RingDiscount = a4RingTotal * discount;
                a4RingNetTotal = a4RingTotal - a4RingDiscount;
            }

            if (chbA3Ring.Checked == true)
            {
                MemberDiscount();
                a3RingTotal = a3RingQty * 5.0;
                a3RingDiscount = a3RingTotal * discount;
                a3RingNetTotal = a3RingTotal - a3RingDiscount;
            }

            if (chbA3Folder.Checked == true)
            {
                MemberDiscount();
                a3FolderTotal = a3FolderQty * 3.5;
                a3FolderDiscount = a3FolderTotal * discount;
                a3FolderNetTotal = a3FolderTotal - a3FolderDiscount;
            }

            if (chbA4Folder.Checked == true)
            {
                MemberDiscount();
                a4FolderTotal = a4FolderQty * 2.0;
                a4FolderDiscount = a4FolderTotal * discount;
                a4FolderNetTotal = a4FolderTotal - a4FolderDiscount;
            }
        }

        private void CalculateSubTotal()
        {
            subTotal = a4FlatTotal + a4RingTotal + a3RingTotal + a3FolderTotal + a4FolderTotal;
        }

        private void MemberDiscount()
        {
            if (isMember)
            {
                discount = 0.1;
            }
            else
            {
                discount = 0;
            }
        }

        private void CalculateTotalDiscountAmt()
        {
            totalDiscountAmt = subTotal * discount;
        }

        private void CalculateGST()
        {
            gst = (subTotal - totalDiscountAmt) * 0.07;
        }

        private void CalculateNetTotal()
        {
            netTotal = subTotal - totalDiscountAmt + gst;
        }

        private void DisplayOutPuts()
        {
            lblSubtotal.Text = subTotal.ToString("C");
            lblDiscount.Text = totalDiscountAmt.ToString("C");
            lblGST.Text = gst.ToString("C");
            lblNetTotal.Text = netTotal.ToString("C");
            lblPaymentType.Text = paymentMethod;
        }

        private void ClearAllFields()
        {
            // clear and reset all fields
            txtName.Clear();
            txtContactNo.Clear();
            txtA4FlatQty.Text = "0";
            txtA4RingQty.Text = "0";
            txtA3RingQty.Text = "0";
            txtA3FolderQty.Text = "0";
            txtA4FolderQty.Text = "0";
            rdbYes.Checked = false;
            rdbNo.Checked = false;
            rdbCash.Checked = false;
            rdbCheque.Checked = false;
            rdbCreditCard.Checked = false;
            rdbNets.Checked = false;
            chbA4Flat.Checked = false;
            chbA4Ring.Checked = false;
            chbA3Ring.Checked = false;
            chbA3Folder.Checked = false;
            chbA4Folder.Checked = false;
            lblSubtotal.Text = "___________";
            lblDiscount.Text = "___________";
            lblGST.Text = "___________";
            lblNetTotal.Text = "___________";
            lblPaymentType.Text = "___________";
            rtbReceipt.Clear();
        }

        private void ResetClassVariables()
        {
            a4FlatQty = 0;
            a4RingQty = 0;
            a3RingQty = 0;
            a3FolderQty = 0;
            a4FolderQty = 0;
            a4FlatTotal = 0;
            a4RingTotal = 0;
            a3RingTotal = 0;
            a3FolderTotal = 0;
            a4FolderTotal = 0;
            subTotal = 0;
            totalDiscountAmt = 0;
            gst = 0;
            netTotal = 0;
        }

        private void GenerateOrderNo()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Count(custID) from Customers", con);
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                i++;
                txtCustID.Text = "C-" + i.ToString();

                SqlCommand cmd2 = new SqlCommand("Select Count(orderNo) from Sales", con);
                int i2 = Convert.ToInt32(cmd2.ExecuteScalar());
                i2++;
                txtOrderNo.Text = i2.ToString();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void DisplayHeadings()
        {
            string headingsDisplay;

            headingsDisplay = "Order no.".PadRight(14) + "Date/Time".PadRight(23) + "Item".PadRight(17) + "Quantity".PadRight(11) + "Item Total".PadRight(11) + "subtotal".PadRight(11) + "Discount".PadRight(10) + "GST(7%)".PadRight(10) + "Net Total".PadRight(12) + "Payment Type" + '\n' + '\n';
            rtbReceipt.AppendText(headingsDisplay);

        }
        private void DisplayOrderNoAndDate()
        {
            string orderNoAndDateDisplay;

            orderNoAndDateDisplay = orderNo.ToString().PadRight(14) + dateTimePicker1.Value.ToString() + '\n' + "====================================================================================================================================" + '\n';
            rtbReceipt.AppendText(orderNoAndDateDisplay);
        }

        private void DisplayItem()
        {
            string a4FlatDisplay;
            string a4RingDiplay;
            string a3RingDisplay;
            string a3FolderDisplay;
            string a4FolderDisplay;
            string displayAllItem;

            // display receipt base on selections
            if (chbA4Flat.Checked == true)
            {
                a4FlatDisplay = " ".PadRight(37) + "A4 Flat File".PadRight(17) + a4FlatQty.ToString().PadRight(11) + a4FlatTotal.ToString("C") + '\n';
            }
            else
            {
                a4FlatDisplay = null;
            }

            if (chbA4Ring.Checked == true)
            {
                a4RingDiplay = " ".PadRight(37) + "A4 Ring File".PadRight(17) + a4RingQty.ToString().PadRight(11) + a4RingTotal.ToString("C") + '\n';
            }
            else
            {
                a4RingDiplay = null;
            }

            if (chbA3Ring.Checked == true)
            {
                a3RingDisplay = " ".PadRight(37) + "A3 Ring File".PadRight(17) + a3RingQty.ToString().PadRight(11) + a3RingTotal.ToString("C") + '\n';
            }
            else
            {
                a3RingDisplay = null;
            }

            if (chbA3Folder.Checked == true)
            {
                a3FolderDisplay = " ".PadRight(37) + "A3 Folder File".PadRight(17) + a3FolderQty.ToString().PadRight(11) + a3FolderTotal.ToString("C") + '\n';
            }
            else
            {
                a3FolderDisplay = null;
            }

            if (chbA4Folder.Checked == true)
            {
                a4FolderDisplay = " ".PadRight(37) + "A4 Folder File".PadRight(17) + a4FolderQty.ToString().PadRight(11) + a4FolderTotal.ToString("C") + '\n';
            }
            else
            {
                a4FolderDisplay = null;
            }

            displayAllItem = a4FlatDisplay + a4RingDiplay + a3RingDisplay + a3FolderDisplay + a4FolderDisplay;
            rtbReceipt.AppendText(displayAllItem);
        }

        private void DisplayRtb()
        {
            string displayTotals;
            try
            {
                DisplayHeadings();
                DisplayOrderNoAndDate();
                DisplayItem();
                displayTotals = "====================================================================================================================================" + '\n' + " ".PadRight(76) + subTotal.ToString("C").PadRight(11) + totalDiscountAmt.ToString("C").PadRight(10) + gst.ToString("C").PadRight(10) + netTotal.ToString("C").PadRight(13) + paymentMethod + '\n';
                rtbReceipt.AppendText(displayTotals);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        // currently broken as data is not stored permanently
        private void StoreData()
        {
            // variables for data to be stored
            string varID;
            int varOrderNo;
            double varA4FlatTotal;
            double varA4RingTotal;
            double varA3RingTotal;
            double varA3FolderTotal;
            double varA4FolderTotal;
            double varSubTotal;
            double varA4FlatDiscount;
            double varA4RingDiscount;
            double varA3RingDiscount;
            double varA3FolderDiscount;
            double varA4FolderDiscount;
            double varTotalDiscount;
            double varA4FlatNetTotal;
            double varA4RingNetTotal;
            double varA3RingNetTotal;
            double varA3FolderNetTotal;
            double varA4FolderNetTotal;
            double varGST;
            double varNetTotal;
            string varA4FlatQty;
            string varA4RingQty;
            string varA3RingQty;
            string varA3FolderQty;
            string varA4FolderQty;
            string varName;
            string varTel;
            string varMember;
            string varDateTime;
            string varA4Flat;
            string varA4Ring;
            string varA3Ring;
            string varA3Folder;
            string varA4Folder;
            string varPaymentMethod;

            try
            {
                // variables assignments
                varID = txtCustID.Text;
                varOrderNo = orderNo;
                varA4FolderQty = a4FolderQty.ToString();
                varA4FlatTotal = a4FlatTotal;
                varA4RingTotal = a4RingTotal;
                varA3RingTotal = a3RingTotal;
                varA3FolderTotal = a3FolderTotal;
                varA4FolderTotal = a4FolderTotal;
                varSubTotal = subTotal;
                varA4FlatDiscount = a4FlatDiscount;
                varA4RingDiscount = a4RingDiscount;
                varA3RingDiscount = a3RingDiscount;
                varA3FolderDiscount = a3FolderDiscount;
                varA4FolderDiscount = a4FolderDiscount;
                varA4FlatNetTotal = a4FlatNetTotal;
                varA4RingNetTotal = a4RingNetTotal;
                varA3RingNetTotal = a3RingNetTotal;
                varA3FolderNetTotal = a3FolderNetTotal;
                varA4FolderNetTotal = a4FolderNetTotal;
                varTotalDiscount = totalDiscountAmt;
                varGST = gst;
                varNetTotal = netTotal;
                varName = custName;
                varTel = custNum.ToString();
                varMember = membership;
                varDateTime = dateTimePicker1.Value.ToString();
                varPaymentMethod = paymentMethod;

                if (chbA4Flat.Checked == true)
                {
                    varA4Flat = chbA4Flat.Text;
                    varA4FlatQty = a4FlatQty.ToString();
                }
                else
                {
                    varA4Flat = " ";
                    varA4FlatQty = " ";
                }

                if (chbA4Ring.Checked == true)
                {
                    varA4Ring = chbA4Ring.Text;
                    varA4RingQty = a4RingQty.ToString();
                }
                else
                {
                    varA4Ring = " ";
                    varA4RingQty = " ";
                }

                if (chbA3Ring.Checked == true)
                {
                    varA3Ring = chbA3Ring.Text;
                    varA3RingQty = a3RingQty.ToString();
                }
                else
                {
                    varA3Ring = " ";
                    varA3RingQty = " ";
                }

                if (chbA3Folder.Checked == true)
                {
                    varA3Folder = chbA3Folder.Text;
                    varA3FolderQty = a3FolderQty.ToString();
                }
                else
                {
                    varA3Folder = " ";
                    varA3FolderQty = " ";
                }

                if (chbA4Folder.Checked == true)
                {
                    varA4Folder = chbA4Folder.Text;
                    varA4FolderQty = a4FolderQty.ToString();
                }
                else
                {
                    varA4Folder = " ";
                    varA4FolderQty = " ";
                }

                // open connection to sql server
                con.Open();

                // store sales data
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "INSERT INTO Sales(orderNo, dateAndTime, item1, item1Qty, item2, item2Qty, item3, item3Qty, item4, item4Qty, item5, item5Qty, subtotal, totalDiscount, GST, netTotal, paymentMethod) " +
                "values (@bno, ENCRYPTBYPASSPHRASE('key1234', @bdate), ENCRYPTBYPASSPHRASE('key1234', @bitem1), ENCRYPTBYPASSPHRASE('key1234', @bitem1Qty), ENCRYPTBYPASSPHRASE('key1234', @bitem2), ENCRYPTBYPASSPHRASE('key1234', @bitem2Qty), ENCRYPTBYPASSPHRASE('key1234', @bitem3), ENCRYPTBYPASSPHRASE('key1234', @bitem3Qty), ENCRYPTBYPASSPHRASE('key1234', @bitem4), ENCRYPTBYPASSPHRASE('key1234', @bitem4Qty), ENCRYPTBYPASSPHRASE('key1234', @bitem5), ENCRYPTBYPASSPHRASE('key1234', @bitem5Qty), @bsub, @bdiscount, @bgst, @bnet, ENCRYPTBYPASSPHRASE('key1234', @bpayment))";
                cmd2.Parameters.Add(new SqlParameter("@bno", varOrderNo));
                cmd2.Parameters.Add(new SqlParameter("@bdate", varDateTime));
                cmd2.Parameters.Add(new SqlParameter("@bitem1", varA4Flat));
                cmd2.Parameters.Add(new SqlParameter("@bitem1Qty", varA4FlatQty));
                cmd2.Parameters.Add(new SqlParameter("@bitem2", varA4Ring));
                cmd2.Parameters.Add(new SqlParameter("@bitem2Qty", varA4RingQty));
                cmd2.Parameters.Add(new SqlParameter("@bitem3", varA3Ring));
                cmd2.Parameters.Add(new SqlParameter("@bitem3Qty", varA3RingQty));
                cmd2.Parameters.Add(new SqlParameter("@bitem4", varA3Folder));
                cmd2.Parameters.Add(new SqlParameter("@bitem4Qty", varA3FolderQty));
                cmd2.Parameters.Add(new SqlParameter("@bitem5", varA4Folder));
                cmd2.Parameters.Add(new SqlParameter("@bitem5Qty", varA4FolderQty));
                cmd2.Parameters.Add(new SqlParameter("@bsub", varSubTotal));
                cmd2.Parameters.Add(new SqlParameter("@bdiscount", varTotalDiscount));
                cmd2.Parameters.Add(new SqlParameter("@bgst", varGST));
                cmd2.Parameters.Add(new SqlParameter("@bnet", varNetTotal));
                cmd2.Parameters.Add(new SqlParameter("@bpayment", varPaymentMethod));
                cmd2.ExecuteNonQuery();
                cmd2.Parameters.Clear();

                // store encrypted customer data
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Customers(custID, custName, custTel, member, orderNo) values (@aid, ENCRYPTBYPASSPHRASE('key1234',@aname), ENCRYPTBYPASSPHRASE('key1234',@atel), ENCRYPTBYPASSPHRASE('key1234',@amember), @aorder)";
                cmd.Parameters.Add(new SqlParameter("@aid", varID));
                cmd.Parameters.Add(new SqlParameter("@aname", varName));
                cmd.Parameters.Add(new SqlParameter("@atel", varTel));
                cmd.Parameters.Add(new SqlParameter("@amember", varMember));
                cmd.Parameters.Add(new SqlParameter("@aorder", varOrderNo));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                // store detailed items data
                if (chbA4Flat.Checked == true)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = con;
                    cmd3.CommandText = "INSERT INTO A4FlatFiles(orderNo, quantity, totalPrice, discount, netTotalPrice) values (@uorder, @uqty, @utotal, @udiscount, @unettotal)";
                    cmd3.Parameters.Add(new SqlParameter("@uorder", varOrderNo));
                    cmd3.Parameters.Add(new SqlParameter("@uqty", varA4FlatQty));
                    cmd3.Parameters.Add(new SqlParameter("@utotal", varA4FlatTotal));
                    cmd3.Parameters.Add(new SqlParameter("@udiscount", varA4FlatDiscount));
                    cmd3.Parameters.Add(new SqlParameter("@unettotal", varA4FlatNetTotal));
                    cmd3.ExecuteNonQuery();

                }

                if (chbA4Ring.Checked == true)
                {
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.Connection = con;
                    cmd4.CommandText = "INSERT INTO A4Ringfiles(orderNo, quantity, totalPrice, discount, netTotalPrice) values (@worder, @wqty, @wtotal, @wdiscount, @wnettotal)";
                    cmd4.Parameters.Add(new SqlParameter("@worder", varOrderNo));
                    cmd4.Parameters.Add(new SqlParameter("@wqty", varA4RingQty));
                    cmd4.Parameters.Add(new SqlParameter("@wtotal", varA4RingTotal));
                    cmd4.Parameters.Add(new SqlParameter("@wdiscount", varA4RingDiscount));
                    cmd4.Parameters.Add(new SqlParameter("@wnettotal", varA4RingNetTotal));
                    cmd4.ExecuteNonQuery();
                }

                if (chbA3Ring.Checked == true)
                {
                    SqlCommand cmd5 = new SqlCommand();
                    cmd5.Connection = con;
                    cmd5.CommandText = "INSERT INTO A3RingFiles(orderNo, quantity, totalPrice, discount, netTotalPrice) values (@xorder, @xqty, @xtotal, @xdiscount, @xnettotal)";
                    cmd5.Parameters.Add(new SqlParameter("@xorder", varOrderNo));
                    cmd5.Parameters.Add(new SqlParameter("@xqty", varA3RingQty));
                    cmd5.Parameters.Add(new SqlParameter("@xtotal", varA3RingTotal));
                    cmd5.Parameters.Add(new SqlParameter("@xdiscount", varA3RingDiscount));
                    cmd5.Parameters.Add(new SqlParameter("@xnettotal", varA3RingNetTotal));
                    cmd5.ExecuteNonQuery();

                }

                if (chbA3Folder.Checked == true)
                {
                    SqlCommand cmd6 = new SqlCommand();
                    cmd6.Connection = con;
                    cmd6.CommandText = "INSERT INTO A3FolderFiles(orderNo, quantity, totalPrice, discount, netTotalPrice) values (@yorder, @yqty, @ytotal, @ydiscount, @ynettotal)";
                    cmd6.Parameters.Add(new SqlParameter("@yorder", varOrderNo));
                    cmd6.Parameters.Add(new SqlParameter("@yqty", varA3FolderQty));
                    cmd6.Parameters.Add(new SqlParameter("@ytotal", varA3FolderTotal));
                    cmd6.Parameters.Add(new SqlParameter("@ydiscount", varA3FolderDiscount));
                    cmd6.Parameters.Add(new SqlParameter("@ynettotal", varA3FolderNetTotal));
                    cmd6.ExecuteNonQuery();

                }

                if (chbA4Folder.Checked == true)
                {
                    SqlCommand cmd7 = new SqlCommand();
                    cmd7.Connection = con;
                    cmd7.CommandText = "INSERT INTO A4FolderFiles(orderNo,  quantity, totalPrice, discount, NetTotalPrice) values (@zorder, @zqty, @ztotal, @zdiscount, @znettotal)";
                    cmd7.Parameters.Add(new SqlParameter("@zorder", varOrderNo));
                    cmd7.Parameters.Add(new SqlParameter("@zqty", varA4FolderQty));
                    cmd7.Parameters.Add(new SqlParameter("@ztotal", varA4FolderTotal));
                    cmd7.Parameters.Add(new SqlParameter("@zdiscount", varA4FolderDiscount));
                    cmd7.Parameters.Add(new SqlParameter("@znettotal", varA4FolderNetTotal));
                    cmd7.ExecuteNonQuery();

                }

                // close connection to sql server
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            string summaryOrderNoAndDate;
            string summaryItemsHeading;
            string a4FlatSummary;
            string a4RingSummary;
            string a3RingSummary;
            string a3FolderSummary;
            string a4FolderSummary;
            string summaryAllItems;
            string summaryTotals;

            // read inputs and data validation
            ValidateData();

            // checks if the customer is a member
            IsAMember();

            // checks the payment method
            CheckPaymentMethod();

            // checks if an item is selected
            CheckIfAnItemIsSelected();

            // gives discount if the customer is a member
            MemberDiscount();

            // if all info is valid, no exception caught and rich text box is empty
            if (nameValid && contactNoValid && memberSpecified && paymentMethodSpecified && qtyValid && isAnItemSelected && !exceptionCaught && rtbReceipt.Text == "")
            {
                // calculate discount amount, gst and net total
                CalculateEachItemTotal();
                CalculateSubTotal();
                CalculateTotalDiscountAmt();
                CalculateGST();
                CalculateNetTotal();

                // dialog box summary
                summaryOrderNoAndDate = "Order number: " + orderNo.ToString() + '\n' + "Date: " + dateTimePicker1.Value.ToString() + '\n' + '\n';
                summaryItemsHeading = "Item".PadRight(20) + "Quantity".PadRight(10) + "Total".PadRight(10) + '\n';

                if (chbA4Flat.Checked == true)
                {
                    a4FlatSummary = "A4 Flat File".PadRight(20) + a4FlatQty.ToString().PadRight(10) + a4FlatTotal.ToString("C") + '\n';
                }
                else
                {
                    a4FlatSummary = null;
                }

                if (chbA4Ring.Checked == true)
                {
                    a4RingSummary = "A4 Ring File".PadRight(20) + a4RingQty.ToString().PadRight(10) + a4RingTotal.ToString("C") + '\n';
                }
                else
                {
                    a4RingSummary = null;
                }

                if (chbA3Ring.Checked == true)
                {
                    a3RingSummary = "A3 Ring File".PadRight(20) + a3RingQty.ToString().PadRight(10) + a3RingTotal.ToString("C") + '\n';
                }
                else
                {
                    a3RingSummary = null;
                }

                if (chbA3Folder.Checked == true)
                {
                    a3FolderSummary = "A3 Folder File".PadRight(20) + a3FolderQty.ToString().PadRight(10) + a3FolderTotal.ToString("C") + '\n';
                }
                else
                {
                    a3FolderSummary = null;
                }

                if (chbA4Folder.Checked == true)
                {
                    a4FolderSummary = "A4 Folder File".PadRight(20) + a4FolderQty.ToString().PadRight(10) + a4FolderTotal.ToString("C") + '\n';
                }
                else
                {
                    a4FolderSummary = null;
                }

                summaryAllItems = a4FlatSummary + a4RingSummary + a3RingSummary + a3FolderSummary + a4FolderSummary;
                summaryTotals = '\n' + "Subtotal: " + subTotal.ToString("C") + '\n' + "Total Discount: " + totalDiscountAmt.ToString("C") + '\n' + "GST: " + gst.ToString("C") + '\n' + "Net Total: " + netTotal.ToString("C") + '\n' + "Payment Method:  " + paymentMethod + '\n' + '\n';

                // dialog box to confirm order if all details are valid
                DialogResult result = MessageBox.Show(summaryOrderNoAndDate + summaryItemsHeading + summaryAllItems + summaryTotals + "Confirm order?", "Summary", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // display outputs
                    DisplayOutPuts();

                    // display rich text box
                    DisplayRtb();

                    // store data
                    StoreData();

                    // generate new order no and customer id
                    GenerateOrderNo();

                    MessageBox.Show("Order successfully placed! Please press Clear to place a new order");

                    ResetClassVariables();
                }
                else
                {
                    // do nothing
                }

            }
            // check if an exception is caught
            else if (exceptionCaught)
            {
                exceptionCaught = false;
            }
            // check if the rich text box is not empty
            else if (rtbReceipt.Text != "")
            {
                MessageBox.Show("Please click clear before placing a new order");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // clear all fields
            ClearAllFields();

            // focus cursor on Name
            txtName.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // print receipt
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void Main_Load_1(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to integer stationery shop order panel!","System Message");

            GenerateOrderNo();
        }

        // enable or disable respective quantity fields based on the respectivecheckboxes
        private void chbA4Flat_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtA4FlatQty.Enabled = true;
                txtA4FlatQty.Focus();
            }
            else
            {
                txtA4FlatQty.Enabled = false;
                txtA4FlatQty.Text = "0";
            }
        }

        private void chbA4Ring_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtA4RingQty.Enabled = true;
                txtA4RingQty.Focus();
            }
            else
            {
                txtA4RingQty.Enabled = false;
                txtA4RingQty.Text = "0";
            }
        }

        private void chbA3Ring_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtA3RingQty.Enabled = true;
                txtA3RingQty.Focus();
            }
            else
            {
                txtA3RingQty.Enabled = false;
                txtA3RingQty.Text = "0";
            }
        }

        private void chbA3Folder_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtA3FolderQty.Enabled = true;
                txtA3FolderQty.Focus();
            }
            else
            {
                txtA3FolderQty.Enabled = false;
                txtA3FolderQty.Text = "0";
            }
        }

        private void chbA4Folder_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtA4FolderQty.Enabled = true;
                txtA4FolderQty.Focus();
            }
            else
            {
                txtA4FolderQty.Enabled = false;
                txtA4FolderQty.Text = "0";
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtName to only accept letters
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtContact to only accept numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtA4FlatQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtA4FlatQty to only accept numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtA4RingQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtA4RingQty to only accept numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtA3RingQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtA4RingQty to only accept numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtA3FolderQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtA4RingQty to only accept numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtA4FolderQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtA4RingQty to only accept numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbOnTop_CheckedChanged(object sender, EventArgs e)
        {
            // always on top
            if (((CheckBox)sender).Checked)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
        }

        private void salesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.salesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.stationeryShopDataSet);

        }

        private void btnListOrders_Click(object sender, EventArgs e)
        {
            // refresh the data grid
            try
            {
                DataTable dt = new DataTable();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT orderNo, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', dateAndTime)) as dateAndTime, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item1)) as item1, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item1Qty)) as item1Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2)) as item2, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2Qty)) as item2Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3)) as item3, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3Qty)) as item3Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4)) as item4, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4Qty)) as item4Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5)) as item5, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5Qty)) as item5Qty, subtotal, totalDiscount, GST, netTotal, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', paymentMethod)) as paymentMethod FROM Sales", con);

                sda.Fill(dt);
                salesDataGridView.DataSource = dt;
                salesDataGridView.Update();
                salesDataGridView.Refresh();

            }
            catch (SqlException se)
            {
                MessageBox.Show("Error: " + se.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnFindOrder_Click(object sender, EventArgs e)
        {
            // find order based on choices
            DataTable dt = new DataTable();
            try
            {
                if (rdbOrderNo.Checked == true && txtFindOrderNo.Text.Length > 0)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT orderNo, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', dateAndTime)) as dateAndTime, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item1)) as item1, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item1Qty)) as item1Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2)) as item2, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2Qty)) as item2Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3)) as item3, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3Qty)) as item3Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4)) as item4, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4Qty)) as item4Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5)) as item5, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5Qty)) as item5Qty, subtotal, totalDiscount, GST, netTotal, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', paymentMethod)) as paymentMethod FROM Sales WHERE orderNo LIKE '" + txtFindOrderNo.Text + "%'", con);
                    sda.Fill(dt);
                    salesDataGridView.DataSource = dt;
                }
                else if (rdbDateTime.Checked == true && txtFindDateTime.Text.Length > 0)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT orderNo, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', dateAndTime)) as dateAndTime, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item1)) as item1, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item1Qty)) as item1Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2)) as item2, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2Qty)) as item2Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3)) as item3, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3Qty)) as item3Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4)) as item4, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4Qty)) as item4Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5)) as item5, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5Qty)) as item5Qty, subtotal, totalDiscount, GST, netTotal, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', paymentMethod)) as paymentMethod FROM Sales WHERE CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', dateAndTime)) LIKE '%" + txtFindDateTime.Text + "%'", con);
                    sda.Fill(dt);
                    salesDataGridView.DataSource = dt;
                }
                else if (rdbItem.Checked == true && txtFindItem.Text.Length > 0)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT orderNo, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', dateAndTime)) as dateAndTime, CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item1)) as item1, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item1Qty)) as item1Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2)) as item2, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item2Qty)) as item2Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3)) as item3, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item3Qty)) as item3Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4)) as item4, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item4Qty)) as item4Qty, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5)) as item5, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', item5Qty)) as item5Qty, subtotal, totalDiscount, GST, netTotal, CONVERT(nvarchar(50), DECRYPTBYPASSPHRASE('key1234', paymentMethod)) as paymentMethod FROM Sales WHERE CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item1)) LIKE '%" + txtFindItem.Text + "%'" + "or CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item2)) LIKE '%" + txtFindItem.Text + "%'" + "or CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item3)) LIKE '%" + txtFindItem.Text + "%'" + "or CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item4)) LIKE '%" + txtFindItem.Text + "%'" + "or CONVERT(nvarchar(50),DECRYPTBYPASSPHRASE('key1234', item5)) LIKE '%" + txtFindItem.Text + "%'", con);
                    sda.Fill(dt);
                    salesDataGridView.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Please select an option and enter something in the respective text box of the the selected option to filter");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void rdbOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtFindOrderNo.Enabled = true;
                txtFindOrderNo.Focus();
            }
            else
            {
                txtFindOrderNo.Enabled = false;
                txtFindOrderNo.Clear();
            }
        }

        private void rdbDateTime_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtFindDateTime.Enabled = true;
                txtFindDateTime.Focus();
            }
            else
            {
                txtFindDateTime.Enabled = false;
                txtFindDateTime.Clear();
            }
        }

        private void rdbItem_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtFindItem.Enabled = true;
                txtFindItem.Focus();
            }
            else
            {
                txtFindItem.Enabled = false;
                txtFindItem.Clear();
            }
        }

        private void txtFindOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtFindOrderNo to accept numbers only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        // navigating between textboxes and buttons by key
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Right))
            {
                e.Handled = true;
                txtContactNo.Focus();
            }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Left))
            {
                e.Handled = true;
                txtName.Focus();
            }
        }

        private void txtFindOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Down))
            {
                e.Handled = true;
                btnFindOrder.Focus();
            }
        }

        private void txtFindDateTime_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Down))
            {
                e.Handled = true;
                btnFindOrder.Focus();
            }
        }

        private void txtFindItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Down))
            {
                e.Handled = true;
                btnFindOrder.Focus();
            }
        }

        // print receipt
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(rtbReceipt.Text, new Font("Lucida Console", 9, FontStyle.Regular), Brushes.Black, 50, 125);
        }

        // update date time picker using live date and time
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtReportUser.Text;
                string pass = txtReportPass.Text;

                SqlCommand com = new SqlCommand("SELECT username, CONVERT(varchar(50), DECRYPTBYPASSPHRASE('key1234', password)) as password FROM Logins WHERE username=@USERNAME and CONVERT(varchar(50), DECRYPTBYPASSPHRASE('key1234', password))=@PASSWORD", con);
                con.Open();
                com.Parameters.AddWithValue("@USERNAME", user);
                com.Parameters.AddWithValue("@PASSWORD", pass);
                SqlDataReader Dr = com.ExecuteReader();
                if (Dr.HasRows == true)
                {
                    btnListOrders.Enabled = true;
                    btnFindOrder.Enabled = true;
                    MessageBox.Show("Report functions unlocked","System Message");
                }
                else
                {
                    tries += 1;
                    MessageBox.Show("Authentication Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

            // closes application after 3 unsuccessful tries
            if (tries == 3)
            {
                this.Close();
            }
        }
    }
}