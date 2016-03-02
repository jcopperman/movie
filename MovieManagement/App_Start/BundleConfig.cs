using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace MovieManagement.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region vendor Scripts

            // ---- Jquery -----
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendor/jquery/jquery-{version}.js"));


            // ---- Angular -----
            // I like to bundle my JS files recursively, so any js files that are added later are automatically added to the bundle
            bundles.Add(new ScriptBundle("~/bundles/angular")
                // Include app.js separately to ensure it is loaded first, as iwt is the main dependency
                .Include("~/Scripts/vendor/angular/angular.js")
                .IncludeDirectory("~/Scripts/vendor/angular/", "*.js", searchSubdirectories: true));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //"~/Scripts/modernizr-*"));


            // The bootstrap files were installed with NPM, generally I dont like to move these files around after installation
            // Thats why they are in a different "javascripts" folder
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/vendor/bootstrap/bootstrap.js")                
                .IncludeDirectory("~/Scripts/vendor/bootstrap/", "*.js", searchSubdirectories: true)
                .Include("~/Scripts/vendor/bootstrap/bootstrap-datepicker.js")); // Wouldnt load through directory for some stupid reason


            #endregion

            #region Custom Scripts

            bundles.Add(new ScriptBundle("~/bundles/movieApp")
               .Include("~/Scripts/modules/movieApp.js"));

            #endregion

            #region vendor Styles

            bundles.Add(new StyleBundle("~/stylesheets/bootstrap")
                .Include("~/stylesheets/_bootstrap.css")
                .Include("~/stylesheets/angular-toastr.css")
                .Include("~/stylesheets/bootstrap-datepicker3.css"));

            #endregion

            #region custom Styles

            bundles.Add(new StyleBundle("~/stylesheets/movies").Include(
                   "~/stylesheets/movies.css"));


            #endregion


            BundleTable.EnableOptimizations = false;
        }
    }
}