
set  solution=HexEditorWpfTest
set  config="Release"


msbuild  -restore  -t:Clean     ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.sln"

msbuild  -restore  -t:Rebuild   ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.sln"
