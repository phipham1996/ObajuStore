namespace ObajuStore.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ObajuStore.Data.ObajuStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ObajuStore.Data.ObajuStoreDbContext context)
        {
            CreateUser(context);
            BrandDefault(context);
            CreateContactDetail(context);
            CreateSystemConfig(context);
            CreatePage(context);
        }

        #region Methods

        private void CreateUser(ObajuStoreDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ObajuStoreDbContext()));

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ObajuStoreDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "dvbtham",
                    Email = "dvbtham@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = Convert.ToDateTime("15/09/1996"),
                    Gender = "Nam",
                    FullName = "Thâm David",
                    Address = "Gia Lai",
                    PhoneNumber = "01652130546"
                };

                manager.Create(user, "123123$");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("dvbtham@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }
        }

        private void BrandDefault(ObajuStoreDbContext context)
        {
            if (context.Brands.Count() == 0)
            {
                var brand = new Brand()
                {
                    Name = "Không xác định",
                    Alias = "khong-xac-dinh",
                    CreatedBy = "system",
                    Image = "",
                    Status = true
                };
                context.Brands.Add(brand);
                context.SaveChanges();
            }
        }

        private void CreateContactDetail(ObajuStoreDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new ContactDetail()
                    {
                        Name = "Shop online - ObajuStore",
                        Address = "472 Núi Thành",
                        Phone = "016 5213 0546",
                        Email = "dvbtham@gmail.com",
                        Lat = 16.034562,
                        Lng = 108.222603,
                        Website = "http://ObajuStore.com.vn",
                        Description = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch
                {
                }
            }
        }

        private void CreateSystemConfig(ObajuStoreDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "Hometitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ Obaju Store - nơi mua bán uy tín và chất lượng - ObajuStore.com"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ Obaju Store - nơi mua bán uy tín và chất lượng - ObajuStore.com"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ Obaju Store - nơi mua bán uy tín và chất lượng - ObajuStore.com"
                });
            }
        }

        private void CreatePage(ObajuStoreDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        CreatedDate = DateTime.Now,
                        MetaDescription = "Trang giới thiệu của Obaju Store",
                        MetaKeyword = "Trang giới thiệu của Obaju Store",
                        Content = @"Là trang web bán hàng online, chuyên cung cấp món hàng trong lĩnh vực thiết bị số: Điện thoại, máy tính cá nhân, máy ảnh,... và một số dịch vụ kèm theo khi mua hàng.",
                        Status = true
                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}