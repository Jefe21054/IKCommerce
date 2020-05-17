using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using UIKit;

namespace KMISApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;

            global::Xamarin.Forms.Forms.Init();
            GoogleClientManager.Initialize();
            CurrentPlatform.Init();
            Xamarin.Forms.FormsMaterial.Init();
            FacebookClientManager.OnActivated();
            FacebookClientManager.Initialize(app, options);

            string dbName = "internalkmis_db.sqlite";
            string folderpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"..","Library");
            string fullpath = Path.Combine(folderpath, dbName);

            LoadApplication(new App(fullpath));

            return base.FinishedLaunching(app, options);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            GoogleClientManager.OnOpenUrl(app, url, options);
            return FacebookClientManager.OpenUrl(app, url, options);
        }
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
        }
    }
}
