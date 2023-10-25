# 从工作目录中删除未跟踪的文件和目录。但排除了 samples 目录和 src/IdentityServer4/.vs 目录，不会删除这些目录中的文件
git clean -xdf -e samples -e src/IdentityServer4/.vs

# 运行名为 "clean_cache.ps1" 的 PowerShell 脚本
./clean_cache.ps1