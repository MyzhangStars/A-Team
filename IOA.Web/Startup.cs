using IOA.Common;
using IOA.IRepository;
using IOA.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IOA.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddSingleton<IHomeRepositroy, HomeRepositroy>();
            services.AddSingleton<ILoginRepository, LoginRepository>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            ConfigurationHepler.configurations = Configuration.GetConnectionString("connStr");

            ////��¼������
            //services.AddMvc(config => config.Filters.Add(typeof(SignFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //ֻ����û������ѡ�������£��Ž���ʹ�ù��������� ���磬����ĳ��������Ҫ�� DI �ṩ�� ILogger<T> ʵ��
            //services.AddSingleton<IMyService>((container) =>
            //{
            //    var logger = container.GetRequiredService<ILogger<MyService>>();
            //    return new MyService() { Logger = logger };
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
