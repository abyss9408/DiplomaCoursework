using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Net;

/// <summary>
/// Summary description for Http
/// </summary>
public class Http
{
    public Http()
    {
        //
        // TODO: Add constructor logic here
        //        
    }

    public static byte[] Post(string url, NameValueCollection pairs)
    {
        byte[] numArray;
        using (WebClient webClient = new WebClient())
        {
            numArray = webClient.UploadValues(url, pairs);
        }
        return numArray;
    }

}