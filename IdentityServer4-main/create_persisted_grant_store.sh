# 用于为 IdentityServer4 项目的持久化授权数据库创建一个新的数据库迁移
# -c指定了DbContext 类的名称
# -o指定了生成的迁移文件的输出目录
# -p指定了用于创建迁移的项目文件
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb -p src/IdentityServer4/host/Host.csproj