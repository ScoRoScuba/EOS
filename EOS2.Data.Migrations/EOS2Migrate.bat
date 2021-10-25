rem Migrate the database

rem c:\Sites\EOS2\bin\EOS2.Data.Migrations.exe -d EOS2 -z false -c "Data Source=10.13.224.72;Initial Catalog=EOS2DB;Persist Security Info=True;User ID=eos2;Password=LetMe1nPlease"
rem c:\Sites\EOS2\bin\EOS2.Data.Migrations.exe -d Elmah -z false -c "Data Source=10.13.224.72;Initial Catalog=EOS2DB;Persist Security Info=True;User ID=eos2;Password=LetMe1nPlease"
rem c:\Sites\EOS2\bin\EOS2.Data.Migrations.exe -d Security -z false -c "Data Source=10.13.224.72;Initial Catalog=EOS2DB;Persist Security Info=True;User ID=eos2;Password=LetMe1nPlease"

rem Migrate All 
c:\Sites\EOS2\bin\EOS2.Data.Migrations.exe -d All -z true -c "Data Source=10.13.224.72;Initial Catalog=EOS2DB;Persist Security Info=True;User ID=eos2;Password=LetMe1nPlease"