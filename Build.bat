
set  solution=HexEditorWpfTest
set  config="Release"


msbuild  -restore  -t:Build     ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.sln"
