﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GWvW_Overlay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        Keyboard.KeyboardListener KListener = new Keyboard.KeyboardListener();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //KListener.KeyDown += new Keyboard.RawKeyEventHandler(KListener_KeyDown);
        }

        /*void KListener_KeyDown(object sender, Keyboard.RawKeyEventArgs args)
        {
            Console.WriteLine(args.Key.ToString());
        }*/

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            KListener.Dispose();
        }
    }
}


