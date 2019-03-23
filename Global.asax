<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        if (Request.QueryString["lan"] == null || Request.QueryString["lan"] == "")
        {
            Business.UI.CultureResource.SetCulture("fa-IR", Context);
            if (Request.QueryString.Count > 0)
                Response.Redirect(Request.Url.OriginalString + "&lan=fa");
            else
                Response.Redirect(Request.Url.OriginalString + "?lan=fa");
        }
        else
        {
            switch (Request.QueryString["lan"])
            {
                case "fa": Business.UI.CultureResource.SetCulture("fa-IR", Context); break;
                //case "ar": Business.UI.CultureResource.SetCulture("ar-SA", Context); break;
                case "en": Business.UI.CultureResource.SetCulture("en-US", Context); break;
            }
            //Business.UI.CultureResource.SetCulture(Request.QueryString["lan"] == "fa" ? "fa-IR" : "en-US", Context);
        }
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
