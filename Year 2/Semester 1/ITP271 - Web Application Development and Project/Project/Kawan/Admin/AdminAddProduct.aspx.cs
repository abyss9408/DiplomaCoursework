using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AdminAddProduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KawanConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        // auto generate prodid
        GenerateProductID();
    }

    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        bool result = false;
        int prod_id;
        string prod_name;
        string database_image = "";
        string prod_desc;
        decimal unit_price;
        int stock_level;
        string category;
        string image = "";
        string query = "Insert into Products(ProductID, Product_Name, Product_Image, Product_Desc, Stock, Price, Category)" +
            "values(@ProductID, @Product_Name, @Product_Image, @Product_Desc, @Stock, @Price, @Category)";
        SqlCommand insert_Product = new SqlCommand(query, con);

        prod_id = int.Parse(lbl_ProdID.Text);
        prod_name = txt_ProdName.Text;
        prod_desc = txt_ProdDesc.Text;
        unit_price = decimal.Parse(txt_UnitPrice.Text);
        stock_level = int.Parse(txt_StockLevel.Text);
        category = DropDownListCategory.SelectedItem.ToString();


        if (FileUpload1.HasFile == true)
        {
            image = "img\\" + FileUpload1.FileName;
            database_image = "~/img/" + FileUpload1.FileName;
            result = true;
        }
        else
        {
            Response.Write("<script>alert('Please add an image');</script>");
        }
       

        if (result)
        {
            // save to database
            con.Open();
            insert_Product.Parameters.AddWithValue("@ProductID", prod_id);
            insert_Product.Parameters.AddWithValue("@Product_Name", prod_name);
            insert_Product.Parameters.AddWithValue("@Product_Image", database_image);
            insert_Product.Parameters.AddWithValue("@Product_Desc", prod_desc);
            insert_Product.Parameters.AddWithValue("@Stock", stock_level);
            insert_Product.Parameters.AddWithValue("@Price", unit_price);
            insert_Product.Parameters.AddWithValue("@Category", category);
            insert_Product.ExecuteNonQuery();
            con.Close();

            // save image to /img/
            string saveimg = Server.MapPath("..") + "\\" + image;
            lbl_Result.Text = saveimg.ToString();
            FileUpload1.SaveAs(saveimg);           
            Response.Write("<script>alert('Insert successful');</script>");

            // generate new product id
            GenerateProductID();
           
        }
        else { Response.Write("<script>alert('Insert NOT successful');</script>"); }
    }

    public void GenerateProductID()
    {
        SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(ProductID as int)), 0)+1 From Products", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        lbl_ProdID.Text = dt.Rows[0][0].ToString();
    }

    protected void btn_ViewProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AdminProducts.aspx");
    }
}