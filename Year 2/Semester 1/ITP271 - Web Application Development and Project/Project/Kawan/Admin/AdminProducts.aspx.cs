using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUsername"] != null)
        {
            lblAdminUser.Text = Session["AdminUsername"].ToString();
            if (!IsPostBack)
            {
                Bind();
            }
        }
        else
        {
            Response.Redirect("/Login/AdminLogin.aspx");
        }
    }

    protected void btnLogout_Click1(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("/Login/AdminLogin.aspx");
    }

    protected void btn_AddProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddProduct.aspx");
    }

    public void UpdateData(string p_name, string p_image, string p_desc, string p_stock, string price, string p_cat, string p_id)
    {
        string updatedata = "Update Products set Product_Name=@name, Product_Image=@image, Product_Desc=@desc, Stock=@stock, Price=@price, Category=@cat Where ProductID=@pid";
        con.Open();
        SqlCommand cmd = new SqlCommand(updatedata, con);
        cmd.Parameters.AddWithValue("@name", p_name);
        cmd.Parameters.AddWithValue("@image", p_image);
        cmd.Parameters.AddWithValue("@desc", p_desc);
        cmd.Parameters.AddWithValue("@stock", p_stock);
        cmd.Parameters.AddWithValue("@price", price);
        cmd.Parameters.AddWithValue("@cat", p_cat);
        cmd.Parameters.AddWithValue("@pid", p_id);
        cmd.ExecuteNonQuery();
        con.Close();
        gv_Product.EditIndex = -1;
        Bind();
    }

    public void Bind()
    {
        gv_Product.DataSourceID = null;
        gv_Product.DataSource = sdsProducts;
        gv_Product.DataBind();
    }

    protected void gv_Product_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = e.RowIndex;
        string upload_image = "";
        string database_image = "";
        bool has_image = false;

        GridViewRow row = gv_Product.Rows[index];
        TextBox prod_name = (TextBox)row.FindControl("TextBox1");
        TextBox prod_desc = (TextBox)row.FindControl("TextBox2");
        Image prod_image = (Image)row.FindControl("Image2");
        FileUpload fu = (FileUpload)row.FindControl("FileUpload2");
        TextBox prod_stock = (TextBox)row.FindControl("TextBox3");
        TextBox prod_price = (TextBox)row.FindControl("TextBox4");
        TextBox prod_cat = (TextBox)row.FindControl("TextBox5");
        Label label1 = (Label)row.FindControl("Label1");

        try
        {
            if (fu.HasFile == true)
            {
                upload_image = "img\\" + fu.FileName;
                database_image = "~/img/" + fu.FileName;
                has_image = true;
            }
            else
            {
                database_image = prod_image.ImageUrl;
            }

            if (has_image)
            {
                // save image to ~/img/
                string saveimg = Server.MapPath("..") + "\\" + upload_image;
                fu.SaveAs(saveimg);

                UpdateData(prod_name.Text, database_image, prod_desc.Text, prod_stock.Text, prod_price.Text, prod_cat.Text, label1.Text);
            }
            else // update without changing image
            {
                UpdateData(prod_name.Text, database_image, prod_desc.Text, prod_stock.Text, prod_price.Text, prod_cat.Text, label1.Text);
            }
        }
        catch (Exception ex)
        {
            lbl_Error.Text = ex.Message.ToString();
        }
        
    }

    protected void gv_Product_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_Product.EditIndex = e.NewEditIndex;
        Bind();
    }

    protected void gv_Product_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_Product.EditIndex = -1;
        Bind();
    }

    protected void gv_Product_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label prod_id = (Label)gv_Product.Rows[e.RowIndex].FindControl("Label1");
        string query = "Delete From Products Where ProductID=@pid";
        try
        {
            con.Open();
            SqlCommand delete_prod = new SqlCommand(query, con);
            delete_prod.Parameters.AddWithValue("@pid", prod_id.Text);
            delete_prod.ExecuteNonQuery();
            con.Close();
            Bind();
            lbl_Error.Text = "Successfully deleted product";
        }
        catch (Exception)
        {
            lbl_Error.Text = "Error deleting product: Product is tied to another table";
        }
        
    }
}