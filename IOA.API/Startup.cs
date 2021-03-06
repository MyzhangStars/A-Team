using IOA.Common;
using IOA.IRepository;
using IOA.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IOA.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services/*, ILoggerFactory loggerFactory*/)
        {
            //redis缓存
            var section = Configuration.GetSection("Redis:Default");
            //连接字符串
            ConfigHelperRedis._connectionString = section.GetSection("connStr").Value;
            //实例化名称
            ConfigHelperRedis._instanceName = section.GetSection("InstanceName").Value;
            //默认数据库
            ConfigHelperRedis._db = int.Parse(section.GetSection("DefaultDB").Value ?? "0");

            //loggerFactory.AddNLog();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".AdventureWorks.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(120);//设置session的过期时间
            //    options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            //});
            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddHttpContextAccessor();
            ////HttpContextAccessor 默认实现了它简化了访问HttpContext
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddSingleton<ILoginRepository, LoginRepository>();
            services.AddSingleton<IHomeRepositroy, HomeRepositroy>();
            services.AddSingleton<IRoleRepositroy, RoleRepositroy>();
            services.AddSingleton<IMenuRepositroy, MenuRepositroy>();
            services.AddSingleton<IUserRepositroy, UserRepositroy>();
            services.AddSingleton<IBusinessRepositroy, BusinessRepositroy>();
            ConfigurationHepler.configurations = Configuration.GetConnectionString("connStr");

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("any", builder =>
            //     {
            //         builder.AllowAnyOrigin()  //允许任何来源的主机访问
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .AllowCredentials(); //指定处理cookie
            //     });
            //});

            services.AddSwaggerGen(c =>
            {
                ////启用swagger验证功能
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头需要添加JWT授权 Token：Bearer Token",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放authorization信息的位置（请求头中）
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IOA.API", Version = "v1" });
                //添加全局安全条件
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                     {
                       new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"}
                        },new string[] { }
                       }
                   });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IOA.API v1"));
            }
            //设置全局跨域
            //app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseHttpsRedirection();

            //app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
