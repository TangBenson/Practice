# 这是一个PowerShell脚本，用于自动化构建和管理.NET Core项目的过程

# 如果出现任何错误，脚本会立即停止执行，而不是继续执行下去
$ErrorActionPreference = "Stop";

# 创建一个名为"nuget"的目录。使用了-Force选项，如果该目录已经存在，也不会报错
New-Item -ItemType Directory -Force -Path ./nuget

# 尋找當前目錄範圍內的工具資訊清單檔，並安裝其中列出的工具
dotnet tool restore

# 接下来的一系列 pushd 和 popd 命令用于进入和退出不同的目录，然后调用各个目录下的 build.ps1 脚本
pushd ./src/Storage
Invoke-Expression "./build.ps1 $args"
popd

pushd ./src/IdentityServer4
Invoke-Expression "./build.ps1 $args"
popd

pushd ./src/EntityFramework.Storage
Invoke-Expression "./build.ps1 $args"
popd

pushd ./src/EntityFramework
Invoke-Expression "./build.ps1 $args"
popd

pushd ./src/AspNetIdentity
Invoke-Expression "./build.ps1 $args"
popd
