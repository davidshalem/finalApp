using LoginBaseApp.Service;
using LoginBaseApp.Views;

namespace LoginBaseApp
{
    public partial class App : Application
    {
        Page firstpage;
        public App()
        {
            InitializeComponent();
            this.firstpage = new AppShell();
        }

    

        protected override Window CreateWindow(IActivationState? activationState)
        {
           // return new Window(new AppShell());
           return new Window(firstpage);
		}
    }
}