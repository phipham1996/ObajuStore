namespace ObajuStore.Data.Migrations
{
    using Common;
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

        protected override void Seed(ObajuStoreDbContext context)
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
                    UserName = "dvbtham@gmail.com",
                    Email = "dvbtham@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    Gender = "Nam",
                    Image = CommonConstants.DefaultAvatar,
                    FullName = "Thâm David",
                    Address = "Gia Lai",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    PhoneNumberConfirmed = true,
                    PhoneNumber = "01652130546"
                };

                manager.Create(user, "Tham1996$");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = CommonConstants.ADMIN });
                    roleManager.Create(new IdentityRole { Name = CommonConstants.MAN });
                    roleManager.Create(new IdentityRole { Name = CommonConstants.MEM });
                }

                var adminUser = manager.FindByEmail("dvbtham@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { CommonConstants.ADMIN, CommonConstants.MAN, CommonConstants.MEM });
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
                        Name = "Shop online - Obaju Store",
                        Address = "472 Núi Thành",
                        Phone = "016 5213 0546",
                        Email = "dvbtham@gmail.com",
                        Lat = 16.034562,
                        Lng = 108.222603,
                        Website = "http://obajustore.com.vn",
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
                    ValueString = "Trang chủ Obaju Store shop - nơi mua bán uy tín và chất lượng - ObajuStore.com"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ Obaju Store shop - nơi mua bán uy tín và chất lượng - ObajuStore.com"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ Obaju Store shop - nơi mua bán uy tín và chất lượng - ObajuStore.com"
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
                        Content = @"Là trang web bán hàng online, tại đây khách hàng có thể tự tạo áo của mình theo mẫu và có thể thử mặc áo online.",
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