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
            //redis����
            var section = Configuration.GetSection("Redis:Default");
            //�����ַ���
            ConfigHelperRedis._connectionString = section.GetSection("connStr").Value;
            //ʵ��������
            ConfigHelperRedis._instanceName = section.GetSection("InstanceName").Value;
            //Ĭ�����ݿ�
            ConfigHelperRedis._db = int.Parse(section.GetSection("DefaultDB").Value ?? "0");

            //loggerFactory.AddNLog();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".AdventureWorks.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(120);//����session�Ĺ���ʱ��
            //    options.Cookie.HttpOnly = true;//���������������ͨ��js��ø�cookie��ֵ
            //});
            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddHttpContextAccessor();
            ////HttpContextAccessor Ĭ��ʵ���������˷���HttpContext
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddSingleton<ILoginRepository, LoginRepository>();
            services.AddSingleton<IHomeRepositroy, HomeRepositroy>();
            services.AddSingleton<IRoleRepositroy, RoleRepositroy>();
            services.AddSingleton<IMenuRepositroy, MenuRepositroy>();
            services.AddSingleton<IUserRepositroy, UserRepositroy>();
            ConfigurationHepler.configurations = Configuration.GetConnectionString("connStr");

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("any", builder =>
            //     {
            //         builder.AllowAnyOrigin()  //�����κ���Դ����������
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .AllowCredentials(); //ָ������cookie
            //     });
            //});

            services.AddSwaggerGen(c =>
            {
                ////����swagger��֤����
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ��Ҫ���JWT��Ȩ Token��Bearer Token",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��authorization��Ϣ��λ�ã�����ͷ�У�
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IOA.API", Version = "v1" });
                //���ȫ�ְ�ȫ����
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
            //����ȫ�ֿ���
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
