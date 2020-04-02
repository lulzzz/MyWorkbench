using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// 	Extension methods for the array data type
/// </summary>
public static class ApplicationExtension
{

    ///<summary>
    ///	Check if the array is null or empty
    ///</summary>
    ///<param name = "source"></param>
    ///<returns></returns>
    public static string GetSiteUrl()
    {
        HttpRequest request = HttpContext.Current.Request;

        string url = string.Empty;

        string baseUrl = request.Url.Scheme + "://" + request.Url.Authority +
            request.ApplicationPath.TrimEnd('/') + "/";       

        if (request.IsSecureConnection)
            url = "https://";
        else
            url = "http://";

        url += request["HTTP_HOST"] + "/";

        return url;
    }


}
