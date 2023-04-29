using Android.Graphics.Drawables;
using BD_Bergeno_lituanistine_mokiklele.Controls;
using BD_Bergeno_lituanistine_mokiklele.Views.Dashboard;
using BD_Bergeno_lituanistine_mokiklele.Views.Startup;
using BD_Bergeno_lituanistine_mokiklele.Views.WebViewPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.Models {
    public class AppConstant {

        public async static Task AddFlyoutMenusDetails() {
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();

            var subscriberDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(NarysDashboardPage)).FirstOrDefault();
            if (subscriberDashboardInfo != null) AppShell.Current.Items.Remove(subscriberDashboardInfo);

            var teacherDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(TeacherDashboardPage)).FirstOrDefault();
            if (teacherDashboardInfo != null) AppShell.Current.Items.Remove(teacherDashboardInfo);

            var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(AdminDashboardPage)).FirstOrDefault();
            if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo);


            if (App.UserDetails.RoleID == (int)RoleDetails.Subscriber) {
                var flyoutItem = new FlyoutItem() {
                    Title = "Narys Page",
                    Route = nameof(NarysDashboardPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Naujienos",
                                    ContentTemplate = new DataTemplate(typeof(NewsPage)),
                                },                               
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Žinutės",
                                    ContentTemplate = new DataTemplate(typeof(MessagesPage)),
                                },
                                 new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Grupės",
                                    ContentTemplate = new DataTemplate(typeof(GroupsPage)),
                                },

                                new ShellContent
                                 {
                                    Icon = Icons.AboutUs,
                                    Title = "Jūsų Profilis",
                                    ContentTemplate = new DataTemplate(typeof(ProfileDashboardPage)),
                                },
                                  new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Nario Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(NarysDashboardPage)),
                                },

                            }
                };
                // default load page
                if (!AppShell.Current.Items.Contains(flyoutItem)) {
                    AppShell.Current.Items.Add(flyoutItem);
                    if (DeviceInfo.Platform == DevicePlatform.WinUI) {
                        AppShell.Current.Dispatcher.Dispatch(async () => {
                            await Shell.Current.GoToAsync($"//{nameof(NarysDashboardPage)}");
                        });
                    }
                    else {
                        await Shell.Current.GoToAsync($"//{nameof(NarysDashboardPage)}");
                    }
                }

            }

            if (App.UserDetails.RoleID == (int)RoleDetails.Author) {
                var flyoutItem = new FlyoutItem() {
                    Title = "Dashboard Page",
                    Route = nameof(TeacherDashboardPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Naujienos",
                                    ContentTemplate = new DataTemplate(typeof(NewsPage)),
                                },                                
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Žinutės",
                                    ContentTemplate = new DataTemplate(typeof(MessagesPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Grupės",
                                    ContentTemplate = new DataTemplate(typeof(GroupsPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Jūsų Profilis",
                                    ContentTemplate = new DataTemplate(typeof(ProfileDashboardPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Teacher Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(TeacherDashboardPage)),
                                },
                   }
                };

                if (!AppShell.Current.Items.Contains(flyoutItem)) {
                    AppShell.Current.Items.Add(flyoutItem);
                    if (DeviceInfo.Platform == DevicePlatform.WinUI) {
                        AppShell.Current.Dispatcher.Dispatch(async () => {
                            await Shell.Current.GoToAsync($"//{nameof(TeacherDashboardPage)}");
                        });
                    }
                    else {
                        await Shell.Current.GoToAsync($"//{nameof(TeacherDashboardPage)}");
                    }
                }
            }

            if (App.UserDetails.RoleID == (int)RoleDetails.Administrator) {
                var flyoutItem = new FlyoutItem() {
                    Title = "Dashboard Page",
                    Route = nameof(AdminDashboardPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Naujienos",
                                    ContentTemplate = new DataTemplate(typeof(NewsPage)),
                                },                                                              
                                new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Žinutės",
                                    ContentTemplate = new DataTemplate(typeof(MessagesPage)),
                                },
                                 new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Grupės",
                                    ContentTemplate = new DataTemplate(typeof(GroupsPage)),
                                },
                                 new ShellContent
                                {
                                    Icon = Icons.AboutUs,
                                    Title = "Jūsų Profilis",
                                    ContentTemplate = new DataTemplate(typeof(ProfileDashboardPage)),
                                },
                                 new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Admin Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(AdminDashboardPage)),
                                },
                   }
                };

                if (!AppShell.Current.Items.Contains(flyoutItem)) {
                    AppShell.Current.Items.Add(flyoutItem);
                    if (DeviceInfo.Platform == DevicePlatform.WinUI) {
                        AppShell.Current.Dispatcher.Dispatch(async () => {
                            await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                        });
                    }
                    else {
                        await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                    }
                }


            }
        }
    }
}
